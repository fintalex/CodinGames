﻿using System;
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
		public List<Zone> Neighbors = new List<Zone> ();
		public List<Player> Players = new List<Player>();
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

	public static List<Zone> listZone = new List<Zone>();

	static void Main(String[] args)
	{
		string[] inputs;

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
			string move = "";
			List<Zone> allMyZones = listZone.FindAll(z => z.Owner == myId);
			foreach (var myZone in allMyZones)
			{
				Console.Error.WriteLine("222222222222222222222222222222222");
				// here I have to move my PODs to free zone
				Zone freeZone = myZone.Neighbors.Find(n => n.Owner != myId);
				if (freeZone != null)
				{
					move += myZone.Players[myId].PODs + " " + myZone.ID + " " + freeZone.ID + " ";
				}
			}




			if (allMyZones.Count == 0)
			{
				Console.WriteLine("WAIT"); // first line for movement commands, second line for POD purchase (see the protocol in the statement for details)				
			}
			else 
			{
				Console.WriteLine(move);
			}

			Console.WriteLine("1 73");
		}
	}

	private static void ReadPodsForZones()
	{
		string[] inputs;
		int platinum = int.Parse(Console.ReadLine()); // my available Platinum
		Console.Error.WriteLine("My plbars:" + platinum);

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
			Console.Error.WriteLine(" ID:" + z.ID + "   Owner:" + z.Owner + "   PLB:" + z.Plbars + "   Neigh:" + neighbors + " " + players);  
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