﻿using rpgcs;
using System.IO;
using static rpgcs.Panel;

namespace program
{
    class Program
    {
        static void Main()
        {

            // Setting obect
            Dice dice = new Dice();

            Character[] party = Character.Fabric();
            Enemy[] bestiary = Enemy.Fabric();

            Enemy[] enemies =
            {
                new Enemy(bestiary[dice.random(bestiary.Length)]),
                new Enemy(bestiary[dice.random(bestiary.Length)]),
                new Enemy(bestiary[dice.random(bestiary.Length)])
            };
            Enemy.EnemiesNameCorrector(bestiary, enemies);

            // Setting Queue
            List<Unit> Queue = new List<Unit>();

            for (int i = 0; i < (party.Length + enemies.Length); i++)
            {
                if (i < party.Length)   Queue.Add(party[i]);
                else                    Queue.Add(enemies[i - party.Length]);
            }

            // Core loop
            while (true)
            {
                // Setting queue
                Queue = Queue.OrderBy(unit => unit.Atributes.speed + dice.d6()/2).ToList();
                Queue.Reverse();

                // Turn
                foreach (Unit unit in Queue)
                {
                    //checkwin
                    Panel.WinLoose = Panel.ChechWinLoose(party, enemies);
                    if(Panel.WinLoose != CombatStatus.Continued)
                    {
                        break;
                    }


                    Panel.Draw(party, enemies, Queue, unit);

                    if (unit.Status != Status.Fainted)
                        unit.TakeAnAction(Queue, dice);

                    Console.Clear();
                    
                }
                if (Panel.WinLoose != CombatStatus.Continued)
                {
                    break;
                }
                Panel.Turn++;
            }
            if(Panel.WinLoose == CombatStatus.Won)
            {
                Console.Clear();
                Panel.Draw(party, enemies, Queue, new Unit());
                Console.WriteLine("Congratulation. You won the battle!");
            }
            else if(Panel.WinLoose == CombatStatus.Loosed)
            {
                Console.Clear();
                Panel.Draw(party, enemies, Queue, new Unit());
                Console.WriteLine("You loose the battle");
            }
            
        }
    }

}

