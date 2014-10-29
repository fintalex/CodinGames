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
	public class Pass
	{
		public int Y;
		public bool IsFired;
		public int PosToFire;
	}

	public class Mount
	{
		public int X;
		public int High;
	}

	public static List<Mount> allMount = new List<Mount>();
	public static List<Pass> allPass = new List<Pass>();

	static void Main(String[] args)
	{
		string[] inputs;

		// game loop
		while (true)
		{
			inputs = Console.ReadLine().Split(' ');
			int SX = int.Parse(inputs[0]);
			int SY = int.Parse(inputs[1]);
			Console.Error.WriteLine(" SX: " + SX + " SY: " + SY);

			
			for (int i = 0; i < 8; i++)
			{
				int MH = int.Parse(Console.ReadLine()); // represents the height of one mountain, from 9 to 0. Mountain heights are provided from left to right.
				Console.Error.WriteLine(i+"th : " + MH);

				Mount findedMount = allMount.Find(m => m.X == i);
				if (findedMount != null) // if found
				{
					findedMount.High = MH;
				}
				else
				{
					allMount.Add(new Mount() { X = i, High = MH });
				}
			}

			foreach (var item in allMount)
			{
				Console.Error.WriteLine("");
			}

			string action = CheckFire(SX, SY);


			Console.WriteLine(action); // either:  FIRE (ship is firing its phase cannons) or HOLD (ship is not firing).
		}
	}

	public static string CheckFire(int sx, int sy)
	{
		 // for begining find pass in allPass
		Pass passForCurY = allPass.Find(p => p.Y == sy);
		if (passForCurY == null) { 
			Pass p = new Pass() { IsFired = false, Y = sy };
			allPass.Add(p); //add pass
		}

		Pass pass = allPass.Find(p => p.Y == sy && !p.IsFired);


		Mount maxMount = new Mount ();
		// if found pass, where I did fired
		if (pass != null)
		{

			foreach (Mount mount in allMount)
			{
				if (sx % 2 == 1) // odd number  <-----
				{
					if (mount.High > maxMount.High)
						maxMount = mount;
				}
				else // even number  ------>
				{
					if (mount.High >= maxMount.High)
						maxMount = mount;
				}
			}

			if (maxMount.X == sx)
			{
				pass.IsFired = true;
				return "FIRE";
			}
			else
			{
				return "HOLD";
			}
			
		}
		else
		{
			
			return "HOLD";
		}

		
	}
}