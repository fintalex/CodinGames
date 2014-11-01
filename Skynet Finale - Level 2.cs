using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
	public static int numberOfNodes = 0;
	public static int numberOfLinks = 0;
	public static int numberOfExits = 0;
	public static int positionSkynet = 0;
	public static int[] positionExits;
	public static int[] positionSimpleNode;
	public static List<Link> listAllLinks = new List<Link>();
	public static List<Link> listExitsLinks = new List<Link>();
	public static List<Link> listImportantLinks = new List<Link>();

	public class Link
	{
		public int from;
		public int to;
		public bool isAvailable;
		public bool alreadyCut;
		public int priority;

		public Link()
		{
			from = 0;
			to = 0;
			isAvailable = true;
		}
	}

	static void Main(string[] args)
	{
		string firstLine = Console.ReadLine();
		string[] firstLineArray = firstLine.Split(' ');
		numberOfNodes = Convert.ToInt32(firstLineArray[0]);
		numberOfLinks = Convert.ToInt32(firstLineArray[1]);
		numberOfExits = Convert.ToInt32(firstLineArray[2]);
		positionExits = new int[numberOfExits];
		positionSimpleNode = new int[numberOfNodes - numberOfExits];
		PrintAllNumbers();

		GetListOfLinks();
		//PrintAllLinks();

		GetAllPosition();
		//PrintAllPositions();

		
		

		GetAllSimpleNodes();
		PrintAllSimpleNodes();





		
		while (true)
		{
			positionSkynet = Convert.ToInt32(Console.ReadLine());




			GetExitsLinks();
			PrintAllExitsLinks();

			GetImportantLink();
			PrintImportantLinks();




			Console.Error.WriteLine("==========");
			//PrintAllLinks();
			
			// here main logic

			//получаем линк от выхода до скайнет
			string shortestLink = "";
			shortestLink = GetShortestLink(shortestLink);

			if (shortestLink == "")
			{
				shortestLink = GetFirstNearestLinkForExit(shortestLink);
			}


			Console.WriteLine(shortestLink);
		}

	}

	private static void PrintImportantLinks()
	{
		Console.Error.WriteLine(" PRINT IMPORTANT LINK ");
		foreach (Link impLink in listImportantLinks)
		{
			Console.Error.WriteLine("      " + impLink.to + " - " + impLink.from);
		}
	}

	private static void GetImportantLink()
	{
		listImportantLinks.Clear();
		for (int i = 0; i < positionSimpleNode.Length; i++)
		{
			// find all whick have two exited uncuted link
			int curPosInCircle = positionSimpleNode[i];
			List<Link> linksWith2exits =
				listExitsLinks.FindAll(le =>
					(le.from == curPosInCircle || le.to == curPosInCircle) &&
					!le.alreadyCut);

			// now check other link
			if (linksWith2exits.Count > 1)
			{
				List<Link> otherLinks = listAllLinks.FindAll(l =>
					(l.to == curPosInCircle || l.from == curPosInCircle) &&
					linksWith2exits.Find(le => le == l) == null);

				if (otherLinks.Count > 0)
				{
					foreach (Link other in otherLinks)
					{
						int curNode = 0;
						curNode = other.from == positionSimpleNode[i] ? other.to : other.from;
						// NOW WE HAVE TO CHECK  - ARE THERE ANY exit links for this node
						if (listExitsLinks.Find(l => l.from == curNode || l.to == curNode) != null || curNode == positionSkynet)
						{
							linksWith2exits[0].priority = (curNode == positionSkynet) ? 1 : 2;
							if (!listImportantLinks.Contains(linksWith2exits[0]))
							{
								listImportantLinks.Add(linksWith2exits[0]);
							}
						}
					}
				}
			}
		}

		listImportantLinks = listImportantLinks.OrderBy(l => l.priority).ToList();
	}

	

	private static string GetFirstNearestLinkForExit(string shortestLink)
	{
		if (listExitsLinks.Count > 0)
		{
			Link firstLink = new Link();
			firstLink = listAllLinks.Find(l => l == listExitsLinks[0]);
			firstLink.alreadyCut = true;
			shortestLink = firstLink.from + " " + firstLink.to;
			return shortestLink;
		}
		//for (int i = 0; i < positionExits.Length; i++)
		//{
		//	Link firstLink = new Link();
		//	firstLink = listAllLinks.Find(l => (l.from == positionExits[i] || l.to == positionExits[i]));
		//	firstLink.alreadyCut = true;
		//	shortestLink = firstLink.from + " " + firstLink.to;
		//	return shortestLink;
		//}


		return shortestLink;
	}

	private static string GetShortestLink(string shortestLink)
	{
		Link shortLink = new Link();
		// look at each EXIT and 
		for (int i = 0; i < positionExits.Length; i++)
		{
			int curExit = positionExits[i];
			// for begining find all NEAREST LINK
			shortLink = listAllLinks.Find(l => ((l.from == curExit && l.to == positionSkynet) || (l.to == curExit && l.from == positionSkynet)) && !l.alreadyCut);

			if (shortLink != null)
			{
				// cut
				shortLink.alreadyCut = true;
				shortestLink = shortLink.from + " " + shortLink.to;

				if (shortLink != null)
					Console.Error.WriteLine("shortLink : " + shortLink.from + " - : " + shortLink.to);
				return shortestLink;
			}
		}


		foreach (Link impLink in listImportantLinks)
		{
			// just get first important link
			// ????????????????????????????????????????????????? BUT Here I have to check hat exactly link

			shortLink = listExitsLinks.Find(l =>
				(l.to == impLink.to && l.from == impLink.from) ||
				(l.to == impLink.from && l.from == impLink.to)
				);
			//shortLink = impLink;
			shortLink.alreadyCut = true;
			shortestLink = shortLink.from + " " + shortLink.to;
			Console.Error.WriteLine(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! " + shortestLink);
			return shortestLink;
		}



		//but if we did not find any nearest we MUST TO CUT EXTRA  node which have a two exits		
		for (int i = 0; i < positionSimpleNode.Length; i++)
		{
			List<Link> linksWith2exits =
				listExitsLinks.FindAll(le =>
					(le.from == positionSimpleNode[i] || le.to == positionSimpleNode[i]) &&
					!le.alreadyCut);
			if (linksWith2exits.Count > 1)
			{
				shortLink = linksWith2exits[0];
				shortLink.alreadyCut = true;
				shortestLink = shortLink.from + " " + shortLink.to;
				Console.Error.WriteLine(" !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ");
				return shortestLink;
			}
		}

	

		return shortestLink;
	}

	private static void PrintAllExitsLinks()
	{
		Console.Error.WriteLine(" LIST OF ALL EXITS LINK");
		foreach (Link el in listExitsLinks)
		{
			Console.Error.WriteLine(el.to +  " - " + el.from );
		}

	}
	private static void PrintAllPositions()
	{
		for (int i = 0; i < positionExits.Length; i++)
		{
			Console.Error.WriteLine(" positionExits : " + positionExits[i]);
		}
	}
	private static void PrintAllNumbers()
	{
		Console.Error.WriteLine(" numberOfNodes : " + numberOfNodes);
		Console.Error.WriteLine(" numberOfLinks : " + numberOfLinks);
		Console.Error.WriteLine(" numberOfExits : " + numberOfExits);
	}
	private static void PrintAllLinks()
	{
		foreach (var item in listAllLinks)
		{
			Console.Error.WriteLine(" lnk : " + item.from + " - " + item.to + " cut " + item.alreadyCut);
		}
	}
	private static void GetAllPosition()
	{
		for (int i = 0; i < numberOfExits; i++)
		{
			string pos = Console.ReadLine();
			positionExits[i] = Convert.ToInt32(pos);
		}
	}
	private static void GetListOfLinks()
	{
		for (int i = 0; i < numberOfLinks; i++)
		{
			string nextLink = Console.ReadLine();
			string[] arrayNextLink = nextLink.Split(' ');

			int nextLinkFrom = Convert.ToInt32(arrayNextLink[0]);
			int nextLinkTo = Convert.ToInt32(arrayNextLink[1]);

			Link link = new Link()
			{
				from = nextLinkFrom,
				to = nextLinkTo
			};

			listAllLinks.Add(link);

			
		}
	}
	private static void PrintAllSimpleNodes()
	{
		string r = "";
		for (int i = 0; i < positionSimpleNode.Length; i++)
		{
			r += " " + positionSimpleNode[i];

		}
		Console.Error.WriteLine(r);
	}
	private static void GetAllSimpleNodes()
	{
		int k = 0;
		for (int i = 0; i < numberOfNodes; i++)
		{
			if (!positionExits.Contains(i))
			{
				positionSimpleNode[k] = i;
				k++;
			}
		}
	}
	private static void GetExitsLinks()
	{
		// get first link
		listExitsLinks.Clear();
		foreach (Link l in listAllLinks)
		{
			for (int i = 0; i < numberOfExits; i++)
			{
				if ( (positionExits[i] == l.from || positionExits[i] == l.to) && !l.alreadyCut )
					listExitsLinks.Add(l);
			}
		}
	}
}