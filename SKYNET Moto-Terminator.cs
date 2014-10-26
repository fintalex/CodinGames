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
    public class Iteration
    {
        public int Pos;
        public int Speed;
        public string Action;
    }
    public static List<Iteration> alliwedActions = new List<Iteration>(); // list for all allowed actions

    static void Main(string[] args)
    {
        int R = int.Parse(Console.ReadLine()); // the length of the road before the gap.
        int G = int.Parse(Console.ReadLine()); // the length of the gap.
        int L = int.Parse(Console.ReadLine()); // the length of the landing platform.
        Console.Error.WriteLine(R + " " + G + " " + L);

        Iteration iter = new Iteration() { Pos = R - 1, Speed = G + 1, Action = "JUMP" };
        alliwedActions.Add(iter);
        FillList(iter); // fill all list allowed actions
        //foreach (Iteration item in alliwedActions)
        //{
        //    Console.Error.WriteLine("Pos: " + item.Pos + " Speed: " + item.Speed + " Act: " + item.Action);
        //}
        Console.Error.WriteLine("Count allowed row: " + alliwedActions.Count);

       
        // game loop
        bool alreadyJumped = false;
        while (true)
        {
            int S = int.Parse(Console.ReadLine()); // the motorbike's speed.
            int X = int.Parse(Console.ReadLine()); // the position on the road of the motorbike.
            Console.Error.WriteLine(S + " " + X); // A single line containing one of 4 keywords: SPEED, SLOW, JUMP, WAIT.

            // find first allowed action
            Iteration curAllowedIter = alliwedActions.Find(i => i.Pos == X && i.Speed == S); 


            if (curAllowedIter != null)
            {
                Console.WriteLine(curAllowedIter.Action);
            }
            else
            {
                Console.WriteLine("SLOW");
            }

            
        }
    }

    /// <summary>
    /// fill all allowed actions
    /// </summary>
    /// <param name="iter">iteration before jump</param>
    public static void FillList(Iteration iter)
    {
        if (iter.Pos - iter.Speed >= 0 && (iter.Speed != 0 && iter.Pos!=0 ))
        {

            int prevPos = iter.Pos - iter.Speed;
            Iteration iterWait = new Iteration() { Speed = iter.Speed, Pos = prevPos, Action = "WAIT" };
            alliwedActions.Add(iterWait);
            FillList(iterWait);

            if (iter.Speed > 0)
            {
                Iteration iterSpeed = new Iteration() { Speed = iter.Speed - 1, Pos = prevPos, Action = "SPEED" };
                alliwedActions.Add(iterSpeed);
                FillList(iterSpeed);
            }

            Iteration iterSlow = new Iteration() { Speed = iter.Speed+1, Pos = prevPos, Action = "SLOW" };
            alliwedActions.Add(iterSlow);
            FillList(iterSlow);

        }
    }

   
}