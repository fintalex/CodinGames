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
	public class Direct {
		public string Name;
		public string Cell;
	}

	public static string[,] Array;
	public static int R = 0;
	public static int C = 0;
	public static bool GoToHome = false;

	static void Main(String[] args)
	{
		string[] inputs;
		inputs = Console.ReadLine().Split(' ');
		R = int.Parse(inputs[0]); // number of rows.
		C = int.Parse(inputs[1]); // number of columns.
		int A = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.
		Console.Error.WriteLine("Rows: " + R + " Columns: " + C + " Round: " + A);
		Array = new string[C, R];

		// game loop
		while (true)
		{
			Direct dir = new Direct();
			inputs = Console.ReadLine().Split(' ');
			int Y0 = int.Parse(inputs[0]); // row where Kirk is located.
			int X0 = int.Parse(inputs[1]); // column where Kirk is located.

			ReadData();
			//Array[X0, Y0] = "@";
			WriteData();
			
			dir = GoToEmptyNotLast( X0,  Y0);

			RewriteCurCell(X0, Y0, dir);




			if (dir.Cell == "C")
			{
				GoToHome = true;
			}
			Console.WriteLine(dir.Name);
		}
	}

	private static Direct	GoToEmptyNotLast( int X0, int Y0)
	{
		List<Direct> fourDir = new List<Direct>();
		Direct up = new Direct() { Name = "UP", Cell = Array[X0, Y0 - 1] };
		Direct down = new Direct() { Name = "DOWN", Cell = Array[X0, Y0 + 1] };
		Direct left = new Direct() { Name = "LEFT", Cell = Array[X0 - 1, Y0] };
		Direct right = new Direct() { Name = "RIGHT", Cell = Array[X0 + 1, Y0] };
		fourDir.Add(up); fourDir.Add(down); fourDir.Add(left); fourDir.Add(right);

		Console.Error.WriteLine("UP: " + up.Cell + "     DOWN: " + down.Cell + "     LEFT: " + left.Cell + "     RIGHT: " + right.Cell);

		// for begining we going to empty
		Direct weGoingTo = new Direct();
		weGoingTo = fourDir.Find(d => d.Cell == "C");

		if (!GoToHome )
		{
			weGoingTo = fourDir.Find(d => d.Cell == "." || d.Cell == "C");
		}

		if (weGoingTo == null)
		{
			Console.Error.WriteLine(" TRY TO FIND EXTRA ");
			weGoingTo = fourDir.FindAll(d => d.Cell != "#" && d.Cell != ".").OrderBy(d => d.Cell).First();
		}













		if (weGoingTo != null)
		{
			return weGoingTo;
		}
		else
		{
			// if nothing find - go to empty cell
			string emptyCell = GoToEmpty(X0, Y0);

			Direct emptyDirect =  fourDir.Find(d=>d.Name == emptyCell);
			return emptyDirect;
		}

	}
	
	public static string GoToEmpty(int X0, int Y0)
	{ 
		if ( Array[X0 - 1, Y0] != "#")
		{
			return "LEFT";
		}
		if (Array[X0 + 1, Y0] != "#")
		{
			return "RIGHT";
		}

		if (Array[X0, Y0 - 1] != "#")
		{
			return "UP";
		}
		if (Array[X0, Y0 + 1] != "#")
		{
			return "DOWN";
		}
		return "DOWN";
	}

	public static void RewriteCurCell(int X0, int Y0, Direct dir) 
	{
		string nextNumber = "";
		nextNumber = GetNextNumber(dir);

		Array[X0, Y0] = nextNumber;
	}

	public static string GetNextNumber(Direct dir)
	{
		
		if (dir.Cell == ".")
		{
			return "1";
		}
		if (dir.Cell == "C")
		{
			return "1";
		}
		if (dir.Cell == "T")
		{
			return "0";
		}

		int number = 0;
		if (Int32.TryParse(dir.Cell, out number))
		{
			number++;
			return number.ToString();
		}

		else {
			Console.Error.WriteLine(" I DONT KNOW WHAT DO I HAVE TO WRITE");
			return "%";
		}

	}

	private static void WriteData()
	{
		for (int i = 0; i < R; i++)
		{
			string curRow = "";
			for (int j = 0; j < C; j++)
			{
				curRow += Array[j, i];
			}
			Console.Error.WriteLine(curRow);
		}
	}
	private static void ReadData()
	{
		for (int i = 0; i < R; i++)
		{
			String ROW = Console.ReadLine(); // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).
			for (int j = 0; j < ROW.Length; j++)
			{
				Array[j, i] = (Array[j, i] == "?" || string.IsNullOrEmpty(Array[j, i])) ? ROW[j].ToString() : Array[j, i];
			}
			//Console.Error.WriteLine("ROW: " + ROW);
		}
	}
}
