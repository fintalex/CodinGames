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
class Solution
{
	public class Symbol
	{
		public string Latter;
		public string[,] Represent;

	}

	public static List<Symbol> listSymbols = new List<Symbol>();
	static void Main(String[] args)
	{
		int L = int.Parse(Console.ReadLine());
		int H = int.Parse(Console.ReadLine());

		// fill aur LIST
		string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";
		for (int i = 0; i < alfabet.Length; i++)
		{
			listSymbols.Add(new Symbol() { Latter = alfabet[i].ToString(), Represent = new string[L,H] } );
		}


		string ourLine = Console.ReadLine();
		Console.Error.WriteLine(ourLine);

		// for each row
		for (int h = 0; h < H; h++)
		{


			int indexForSymbol = 0;
			int curPosInArray = 0;
			String ROW = Console.ReadLine();
			for (int j = 0; j < ROW.Length; j++)
			{
				if (curPosInArray < L)
				{
					listSymbols[indexForSymbol].Represent[curPosInArray, h] = ROW[j].ToString();
					curPosInArray++;
				}
				else
				{
					curPosInArray = 0;
					indexForSymbol++;

					listSymbols[indexForSymbol].Represent[curPosInArray, h] = ROW[j].ToString();
					curPosInArray++;
				}
			}
			//Console.Error.WriteLine(ROW);
		}

		// Write an action using Console.WriteLine()
		// To debug: Console.Error.WriteLine("Debug messages...");

		Console.Error.WriteLine(" Введите букву или слово");

		string exit = Console.ReadLine();
		while (exit != "0")
		{
			PrintLine(exit, L, H);
			exit = Console.ReadLine();
		}



		//Console.WriteLine("answer");
	}

	public static void PrintLine(string ourLine, int L, int H)
	{ 
		List<Symbol> listLetterForPrint = new List<Symbol> ();

		// find all list of letter
		for (int i = 0; i < ourLine.Length; i++)
		{
			Symbol curSymbol = listSymbols.Find(s=>s.Latter == ourLine[i].ToString());

			if (curSymbol != null)
			{
				listLetterForPrint.Add(curSymbol);
			}
			else
			{
				// add ?
				listLetterForPrint.Add(listSymbols[listSymbols.Count-1]);
			}
		}

		for (int i = 0; i < H; i++)
		{
			string curRow = "";
			foreach (Symbol curSymb in listLetterForPrint)
			{
				for (int j = 0; j < L; j++)
				{
					curRow += curSymb.Represent[j, i].ToString();
				}
				//curRow += " ";
			}
			Console.WriteLine(curRow);
		}
	}
}