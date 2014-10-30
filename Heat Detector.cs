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
	static void Main(String[] args)
	{
		string[] inputs;
		inputs = Console.ReadLine().Split(' ');
		int W = int.Parse(inputs[0]); // width of the building.
		int H = int.Parse(inputs[1]); // height of the building.
		int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
		inputs = Console.ReadLine().Split(' ');
		int X0 = int.Parse(inputs[0]);
		int Y0 = int.Parse(inputs[1]);

		Console.Error.WriteLine("W: " + W + " H: " + H);
		Console.Error.WriteLine("X0: " + X0 + " Y0: " + Y0);

		// there are 4 min and max value whick will be decrease our field
		int maxX = W;
		int maxY = H;
		int minX, minY = -1;


		// game loop
		while (true)
		{
			String BOMB_DIR = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
			Console.Error.WriteLine("HELP = " + BOMB_DIR);

			maxX = (BOMB_DIR == "UL" || BOMB_DIR == "DL" || BOMB_DIR == "L") ? X0 : maxX;
			maxY = (BOMB_DIR == "UL" || BOMB_DIR == "UR" || BOMB_DIR == "U") ? X0 : maxX;

			minX = (BOMB_DIR == "DR" || BOMB_DIR == "UR" || BOMB_DIR == "R") ? X0 : maxX;
			minY = (BOMB_DIR == "DR" || BOMB_DIR == "DL" || BOMB_DIR == "D") ? X0 : maxX;

			X0 = (maxX + minX) / 2;
			Y0 = (maxY + minY) / 2;
			// Write an action using Console.WriteLine()
			// To debug: Console.Error.WriteLine("Debug messages...");

			Console.WriteLine(X0 + " " + Y0); // the location of the next window Batman should jump to.
		}
	}
}