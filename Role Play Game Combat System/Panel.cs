using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
                if(member.Status == Status.Fainted)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0:G} \t fainted", member.name);
                    Console.ResetColor();
                }
                else
                {
                    if (member.Atributes.vitality <= member.Atributes.max_vitality / 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    string status = member.Status == Status.None ? "" : member.Status.ToString();
                    Console.WriteLine($"{member.name} \t\t HP: {member.Atributes.vitality} / {member.Atributes.max_vitality}  MP: {member.Atributes.mana} / {member.Atributes.max_mana}  {status}");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("");

            //Enemies panel draw
            foreach (Enemy enemy in enemies)
            {
                if (enemy.Status == Status.Fainted) continue;

                string status = enemy.Status == Status.None ? "" : enemy.Status.ToString();

                if (enemy.Atributes.vitality <= enemy.Atributes.max_vitality / 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                
                Console.WriteLine($"{enemy.name}"
                //Space corrector in longer names
                + (enemy.name.Length < 7 ? "\t" : "")
                + $"\t HP: {enemy.Atributes.vitality} / {enemy.Atributes.max_vitality}  {status}");

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

        //Check Win Loose Status
        public enum CombatStatus
        {
            Continued, Won, Loosed
        }

        public static CombatStatus WinLoose = CombatStatus.Continued;

        public static CombatStatus ChechWinLoose(Character[] party, Enemy[] enemies)
        {
            CombatStatus status;

            ///Check loose
            status = CombatStatus.Loosed;
            foreach (Character character in party)
            {
                if (character.Status != Status.Fainted)
                {
                    status = CombatStatus.Continued;
                    break;
                }

            }
            ///Check win
            if(status == CombatStatus.Continued) {
                status = CombatStatus.Won;
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.Status != Status.Fainted)
                    {
                        status = CombatStatus.Continued;
                        break;
                    }
                }
            }
            return status;
        }

    }

}

