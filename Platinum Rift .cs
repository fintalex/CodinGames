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
	public class Zone
	{
		public int ID;
		public int Plbars;
		public int Owner;
		public List<Zone> Neighbors = new List<Zone> ();

	}
	
	public static int playerCount = 0; // the amount of players (2 to 4)
	public static int myId = 0; // my player ID (0, 1, 2 or 3)
	public static int zoneCount = 0; // the amount of zones on the map
	public static int linkCount = 0; // the amount of links between all zones

	public static List<Zone> listZone = new List<Zone>();
	//public static List<Link> listLink = new List<Link>();

	static void Main(String[] args)
	{
		string[] inputs;

		ReadCommonData();
		ReadZones();
		ReadLinks(); 
		WriteZones();


		// game loop
		while (true)
		{
			int platinum = int.Parse(Console.ReadLine()); // my available Platinum
			Console.Error.WriteLine("My plbars:" + platinum);

			for (int i = 0; i < zoneCount; i++)
			{
				inputs = Console.ReadLine().Split(' ');
				int zId = int.Parse(inputs[0]); // this zone's ID
				int ownerId = int.Parse(inputs[1]); // the player who owns this zone (-1 otherwise)
				int podsP0 = int.Parse(inputs[2]); // player 0's PODs on this zone
				int podsP1 = int.Parse(inputs[3]); // player 1's PODs on this zone
				int podsP2 = int.Parse(inputs[4]); // player 2's PODs on this zone (always 0 for a two player game)
				int podsP3 = int.Parse(inputs[5]); // player 3's PODs on this zone (always 0 for a two or three player game)
			}

			// Write an action using Console.WriteLine()
			// To debug: Console.Error.WriteLine("Debug messages...");

			Console.WriteLine("WAIT"); // first line for movement commands, second line for POD purchase (see the protocol in the statement for details)
			Console.WriteLine("1 73");
		}
	}

	private static void ReadLinks()
	{
		string[] inputs;
		for (int i = 0; i < linkCount; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int zone1 = int.Parse(inputs[0]);
			int zone2 = int.Parse(inputs[1]);
			Zone z1 = listZone.Find(z => z.ID == zone1);
			Zone z2 = listZone.Find(z => z.ID == zone2);
			z1.Neighbors.Add(z2);
			z2.Neighbors.Add(z1);

			//Link l = new Link() { Zone1 = zone1, Zone2 = zone2 };
			//listLink.Add(l);
		}
	}
	private static void ReadZones()
	{
		string[] inputs;
		for (int i = 0; i < zoneCount; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int zoneId = int.Parse(inputs[0]); // this zone's ID (between 0 and zoneCount-1)
			int platinumSource = int.Parse(inputs[1]); // the amount of Platinum this zone can provide per game turn
			Zone z = new Zone() { ID = zoneId, Plbars = platinumSource };
			listZone.Add(z);
		}
	}
	private static void WriteZones()
	{
		foreach (var z in listZone)
		{
			string neighbors = "";
			foreach (var neighbor in z.Neighbors)
			{
				neighbors += " " + neighbor.ID;
			}
			Console.Error.WriteLine(" ID:" + z.ID + "   Owner:" + z.Owner + "   PLB:" + z.Plbars + "   Neigh:" + neighbors);  
		}
	
	}
	private static void ReadCommonData()
	{
		string[] inputs;
		inputs = Console.ReadLine().Split(' ');
		playerCount = int.Parse(inputs[0]); // the amount of players (2 to 4)
		myId = int.Parse(inputs[1]); // my player ID (0, 1, 2 or 3)
		zoneCount = int.Parse(inputs[2]); // the amount of zones on the map
		linkCount = int.Parse(inputs[3]); // the amount of links between all zones

		Console.Error.WriteLine("Pl:" + playerCount + " I'm:" + myId + " ZoneCount:" + zoneCount + " LinkCount:" + linkCount);
	}
}