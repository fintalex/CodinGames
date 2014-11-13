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
class PlatinumRift
{
	public class Zone
	{
		public int ID;
		public int Plbars;
		public int Owner;
		public List<Zone> Neighbors = new List<Zone>();
		public List<Player> Players = new List<Player>();
		public int EnemysPODs;
		public int? Step;
	}

	public class Player
	{
		public int ID;
		public List<Zone> Zones = new List<Zone>();
		public int PODs;
	}

	public static int playerCount = 0; // the amount of players (2 to 4)
	public static int myId = 0; // my player ID (0, 1, 2 or 3)
	public static int zoneCount = 0; // the amount of zones on the map
	public static int linkCount = 0; // the amount of links between all zones
	public static int myPlbar = 0; // my current count of platinum

	public static List<Zone> listZone = new List<Zone>();

	static void Main(String[] args)
	{
		//Console.Error.WriteLine("=================================================");
		ReadCommonData();
		ReadZones();
		ReadLinks();
		//WriteZones();


		// game loop
		while (true)
		{
			//WriteZones();
			ReadPodsForZones();

			//free going to free zone
			string move = GetRowForMove();
			Console.WriteLine(move);






			//Zone zoneFrom = listZone.Find(z => z.ID == 135);
			//Zone zoneTo = listZone.Find(z => z.ID == 127);
			//int stepTo = StepToZone(zoneFrom,zoneTo);


			string buy = GetRowForBuy();

			//if (lis)
			//Console.Error.WriteLine("               String for buing:" + buy);
			Console.WriteLine(buy);
		}
	}

	private static int StepToZone(Zone zoneFrom)//, Zone zoneTo)
	{
		List<Zone> allZones = new List<Zone>();
		Zone ourZoneForMove = new Zone();
		int nextStep = 0;
		zoneFrom.Step = nextStep;
		allZones.Add(zoneFrom);

		List<Zone> stepedZones = new List<Zone>();
		stepedZones = BuildStepedZones(allZones, nextStep);//, zoneTo.ID);

		if (stepedZones.Count > 0)
		{
			foreach (Zone zone in stepedZones)
			{
				Console.Error.WriteLine("To " + zone.ID + " - " + zone.Step + " steps");
			}

			Zone lastZone = stepedZones.Find(z => z.Owner != myId);//ID == zoneTo.ID);
			Zone firstZone = stepedZones.Find(z => z.ID == zoneFrom.ID);

			//Console.Error.WriteLine(" TRY TO FIND WAY ");
			if (lastZone != null && firstZone != null)
			{
				ourZoneForMove = GetNextZone(firstZone, lastZone);
			}
			//Console.Error.WriteLine(" GO HERE " + OURFUCINGZONE.ID + " FROM " + zoneFrom.ID);
			if (ourZoneForMove != null)
				return ourZoneForMove.ID;
			else
				return 0;
		}
		else
			return 0;




	}

	private static Zone GetNextZone(Zone zoneFrom, Zone zoneTo)
	{
		//Console.Error.WriteLine("Zfrom:" + zoneFrom.ID + " Zto:" + zoneTo.ID);
		Zone nearZone = zoneTo.Neighbors.Find(n => n.Step == zoneTo.Step - 1);
		if (nearZone != null && nearZone.ID != zoneFrom.ID)
		{
			Console.Error.WriteLine(" NEAR - " + nearZone.ID);
			return GetNextZone(zoneFrom, nearZone);
		}
		else
		{
			return zoneTo;
		}
	}

	private static List<Zone> BuildStepedZones(List<Zone> allZones, int nextStep)//, int zoneToID)
	{
		List<Zone> zoneArround = allZones.FindAll(z => z.Step == nextStep - 1);

		//foreach (var item in allZones)
		//{
		//	Console.Error.WriteLine(" Zone: " + item.ID + " St: " + item.Step + "nextStep: " + nextStep);
		//}




		//bool addedNewStep = false;

		if (nextStep == 0 || zoneArround.Count > 0)
		{
			foreach (Zone zone in zoneArround)
			{
				foreach (Zone neib in zone.Neighbors.FindAll(n => n.Step == null))
				{
					neib.Step = nextStep;

					Console.Error.WriteLine(" ADDED Zone: " + neib.ID + " St: " + neib.Step);
					allZones.Add(neib);
					//addedNewStep = true; //  show me that we add any one NEIGHBOR

					if (neib.Owner != myId) //ID == zoneToID)
					{
						return allZones;
					}
				}
			}
		}
		else
		{
			return allZones;
		}

		nextStep++;
		return BuildStepedZones(allZones, nextStep);//, zoneToID);
		

	}

	private static string GetRowForMove()
	{
		string move = "";
		List<Zone> allMyZonesPODs = listZone.FindAll(z => z.Owner == myId && z.Players.Find(p => p.ID == myId) != null);
		foreach (var myZone in allMyZonesPODs)
		{
			List<Zone> freeZones = myZone.Neighbors.FindAll(n => n.Owner != myId).OrderByDescending(n => n.Plbars).ToList();
			if (freeZones.Count > 0)
			{
				foreach (var item in freeZones)
				{
					move += 1 + " " + myZone.ID + " " + item.ID + " ";// myZone.Players.Find(p => p.ID == myId).PODs + " " + myZone.ID + " " + item.ID + " ";
				}

			}
			else
			{
				//Zone enemyZone = listZone.Find(z => z.Owner != myId);
				myZone.Step = 0;
				Console.Error.WriteLine(" ======================   FIND EXTRA WAY FOR ZONE:" +myZone.ID + "        AND NOW STEP : " + myZone.Step);
				
				int stepToEnemy = StepToZone(myZone);//, enemyZone);

				foreach (var item in listZone)
				{
					
				}

				listZone.ForEach(z => z.Step = null);

				myZone.Step = 0;

				Console.Error.WriteLine( "================= Iam: " + myZone.ID + " stepToEnemy: " + stepToEnemy);
				move += myZone.Players.Find(p => p.ID == myId).PODs + " " + myZone.ID + " " + stepToEnemy+ " "; // myZone.Neighbors[0].ID + " ";
				Console.Error.WriteLine(" MOVE = " + move);
			}
		}


		if (allMyZonesPODs.Count == 0)
		{
			return "WAIT"; // first line for movement commands, second line for POD purchase (see the protocol in the statement for details)				
		}
		else
		{
			return move;
		}
	}

	private static string GetRowForBuy()
	{
		int myAvvailiablePODs = myPlbar / 20;
		string buy = "";
		List<Zone> orderedZone = new List<Zone>();


		// find about (5-10 best zone(my zone) and keep it )
		orderedZone = listZone.FindAll(zx => zx.Owner == myId).OrderByDescending(z => z.Plbars).ToList();
		if (orderedZone.Count > 0)
		{
			int maxZoneForReflect = orderedZone.Count > 7 ? 7 : orderedZone.Count;
			for (int i = 0; i < maxZoneForReflect; i++)
			{
				if (orderedZone[i].Neighbors.FindAll(n => n.Players.FindAll(p => p.ID != myId).Count > 0).Count > 0)
				{
					List<Zone> enemyNeighbors = orderedZone[i].Neighbors.FindAll(n => n.Players.FindAll(p => p.ID != myId).Count > 0);
					int countPODsForHelp = 0;
					enemyNeighbors.ForEach(neib => countPODsForHelp += neib.EnemysPODs);

					buy += (countPODsForHelp + 1) + " " + orderedZone[i].ID + " ";
					//Console.Error.WriteLine("REFLECT ATACK FOR " + orderedZone[i].ID + " zone; ========  enemyPODs: " + countPODsForHelp);
				}
			}
		}




		// for begining get all empty zone
		if (playerCount > 2)
		{
			// change strategy
			orderedZone = listZone.FindAll(zx => zx.Owner == -1).OrderByDescending(z => z.Plbars).ToList();
			if (orderedZone.Count > 0)
			{
				for (int i = 0; i < orderedZone.Count; i++)
				{
					// here I want to get 2 best neighbors
					orderedZone[i].Neighbors = orderedZone[i].Neighbors.OrderByDescending(z => z.Plbars).ToList();

					buy += "1 " + orderedZone[i].Neighbors[0].ID + " ";
					if (orderedZone[i].Neighbors.Count>1) 
						buy += "1 " + orderedZone[i].Neighbors[1].ID + " ";
				}
			}
		}
		else
		{
			orderedZone = listZone.FindAll(zx => zx.Owner == -1).OrderByDescending(z => z.Plbars).ToList();
			if (orderedZone.Count > 0)
			{
				for (int i = 0; i < orderedZone.Count; i++)
				{
					buy += "2 " + orderedZone[i].ID + " ";
				}
			}
		}



		// if all empty zone is busy -  buy PODs for my current pods
		if (orderedZone.Count == 0)
		{
			//Console.Error.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!   ALL EMPTY ZONE IS FINISHED,   myAvvailiablePODs=" + myAvvailiablePODs);
			// get all my zones
			orderedZone = listZone.FindAll(zx => zx.Owner == myId).OrderByDescending(z => z.Plbars).ToList();


			foreach (Zone myzone in orderedZone)
			{

				if (myzone.Neighbors.FindAll(n => n.Players.FindAll(p => p.ID != myId).Count > 0).Count > 0)
				{
					List<Zone> enemyNeighbors = myzone.Neighbors.FindAll(n => n.Players.FindAll(p => p.ID != myId).Count > 0);
					int countPODsForHelp = 0;
					enemyNeighbors.ForEach(neib => countPODsForHelp += neib.EnemysPODs);

					buy += (countPODsForHelp + 1) + " " + myzone.ID + " ";
					//buy += "2 " + myzone.ID + " ";
					//Console.Error.WriteLine("HELP FOR " + myzone.ID + " zone;  enemyPODs:" + countPODsForHelp);
				}
			}
			//Console.Error.WriteLine("=================================================");
		}



		return buy;
	}

	private static void ReadPodsForZones()
	{
		string[] inputs;
		myPlbar = int.Parse(Console.ReadLine()); // my available Platinum
		//Console.Error.WriteLine("My plbars:" + myPlbar);

		for (int i = 0; i < zoneCount; i++)
		{
			inputs = Console.ReadLine().Split(' ');
			int zId = int.Parse(inputs[0]); // this zone's ID
			Zone zone = listZone.Find(z => z.ID == zId);

			int ownerId = int.Parse(inputs[1]); // the player who owns this zone (-1 otherwise)
			int podsP0 = int.Parse(inputs[2]); // player 0's PODs on this zone
			int podsP1 = int.Parse(inputs[3]); // player 1's PODs on this zone
			int podsP2 = int.Parse(inputs[4]); // player 2's PODs on this zone (always 0 for a two player game)
			int podsP3 = int.Parse(inputs[5]); // player 3's PODs on this zone (always 0 for a two or three player game)

			zone.Owner = ownerId;
			zone.Players.Clear();
			if (podsP0 != 0) { zone.Players.Add(new Player() { ID = 0, PODs = podsP0 }); }
			if (podsP1 != 0) { zone.Players.Add(new Player() { ID = 1, PODs = podsP1 }); }
			if (podsP2 != 0) { zone.Players.Add(new Player() { ID = 2, PODs = podsP2 }); }
			if (podsP3 != 0) { zone.Players.Add(new Player() { ID = 3, PODs = podsP3 }); }
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
			string neighbors = "", players = "";
			foreach (var neighbor in z.Neighbors)
			{
				neighbors += " " + neighbor.ID;
			}
			foreach (var player in z.Players)
			{
				players += "   PL:" + player.ID + "-" + player.PODs;
			}
			//Console.Error.WriteLine(" ID:" + z.ID + "   Owner:" + z.Owner + "   PLB:" + z.Plbars + "   Neigh:" + neighbors + " " + players);
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

		//Console.Error.WriteLine("Pl:" + playerCount + " I'm:" + myId + " ZoneCount:" + zoneCount + " LinkCount:" + linkCount);
	}
}
