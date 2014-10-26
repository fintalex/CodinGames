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
    public class House
    {
        public int x;
        public int y;
    }

    public static long minLength = 0;
    public static List<House> listAllHouse = new List<House>();
    static void Main(String[] args)
    {
        SetAllHourse();

        listAllHouse = listAllHouse.OrderBy(h => h.x).ToList(); //ordered by x
        minLength += Math.Abs(listAllHouse.Last().x - listAllHouse.First().x);  //length beetwin EAST and WEST


        listAllHouse = listAllHouse.OrderBy(h => h.y).ToList(); // ordered by y
        long main = listAllHouse[listAllHouse.Count / 2].y; // get y pos for main

        for (int i = 0; i < listAllHouse.Count; i++)
        {
            minLength += Math.Abs(listAllHouse[i].y - main); // get length beetwin current house and main cable
        }

        Console.WriteLine(minLength.ToString());
    }

    private static void SetAllHourse()
    {
        string[] inputs;
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {

            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);

            House house = new House() { x = X, y = Y };
            listAllHouse.Add(house);
            Console.Error.WriteLine( i + "House  x:" + house.x + " y:" + house.y);
        }
        Console.Error.WriteLine("count=" + N);
    }
}