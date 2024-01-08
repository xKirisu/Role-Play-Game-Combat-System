using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal class Panel
    {
        public static byte Turn = 1;
        public static void Draw(Character[] party, Enemy[] enemies, List<Unit> queue, Unit actual)
        {
            Console.WriteLine($"Turn: {Turn}");

            //Party panel draw
            foreach (Character member in party)
            {
                switch (member.Status)
                {
                    // Fainted
                    case Status.Fainted:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0:G} \t fainted", member.name);
                        Console.ResetColor();
                        break;

                    // Statuses
                    case Status.Prowess:
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.ResetColor();
                        break;

                    // None status
                    case Status.None:
                        if (member.Atributes.vitality <= member.Atributes.max_vitality / 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        Console.WriteLine($"{member.name} \t\t HP: {member.Atributes.vitality} / {member.Atributes.max_vitality}  MP: {member.Atributes.mana} / {member.Atributes.max_mana} ");
                        Console.ResetColor();
                        break;

                    default:
                        if (member.Atributes.vitality <= member.Atributes.max_vitality / 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        Console.Write($"{member.name} \t\t HP: {member.Atributes.vitality} / {member.Atributes.max_vitality}  MP: {member.Atributes.mana} / {member.Atributes.max_mana} ");
                        Console.ResetColor();
                        Console.Write($"{member.Status.ToString()}");
                        break;
                }
                
            }

            Console.WriteLine("");

            //Enemies panel draw
            foreach (Enemy enemy in enemies)
            {
                if (enemy.Status == Status.Fainted) continue;

                if (enemy.Atributes.vitality <= enemy.Atributes.max_vitality / 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                
                Console.WriteLine($"{enemy.name}"
                //Space corrector in longer names
                + (enemy.name.Length < 7 ? "\t" : "")
                + $"\t HP: {enemy.Atributes.vitality} / {enemy.Atributes.max_vitality}");

                Console.ResetColor();
                
            }

            Console.WriteLine("");

            //Queue panel draw
            Console.WriteLine("Queue:");
            foreach (Unit unit in queue)
            {
                if (unit.Status == Status.Fainted) continue;

                if(unit.name == actual.name)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{unit.name} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"{unit.name} ");
                }
            }
            Console.Write('\n');
        }
    }

}

