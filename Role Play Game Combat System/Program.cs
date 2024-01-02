using rpgcs;
using System.IO;

namespace program
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("HelloWorld!");

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
                Queue = Queue.OrderBy(unit => unit.Atributes.speed + dice.d6()).ToList();
                Queue.Reverse();

                // Turn
                foreach (Unit unit in Queue)
                {
                    Panel.Draw(party, enemies, Queue);

                    unit.TakeAnAction(Queue, dice);

                    Console.Clear();
                    
                }
                Panel.Turn++;
                // Check win

            }
        }
    }

}

