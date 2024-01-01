using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgcs
{
    internal enum DamageType
    {
        Phisical, Magical, Other
    }
    internal struct Spell
    {
        internal string name;

        internal float factor;
        internal DamageType type;

        internal sbyte cost;

        bool is_set_status;
        Status set_status;

        internal bool is_critable;
        internal bool is_offensive;

        Spell(string name, float factor, sbyte cost, DamageType damage_type, bool is_critable, bool is_offensive)
        {
            this.name = name;
            this.factor = factor;
            this.cost = cost;

            this.type = damage_type;

            this.is_set_status = false;
            this.set_status = Status.None;

            this.is_critable = is_critable;
        }
        Spell(string name, float factor, sbyte cost, DamageType damage_type, Status status, bool is_critable, bool is_offensive)
        {
            this.name = name;
            this.factor = factor;
            this.cost = cost;

            this.type = damage_type;

            this.is_set_status = true;
            this.set_status = status;

            this.is_critable = is_critable;
            this.is_offensive = is_offensive;
        }
    }
    internal class Magic
    {
        
        Spell[] spellslot = new Spell[4];

        internal Dictionary<string, Spell> Factory()
        {
            return new Dictionary<string, Spell>
            {
                { "Atack", new Spell() }
            };
        }


        internal Magic(string name_1, string name_2, string name_3) 
        {
            spellslot[0] = Factory()["Atack"];
            spellslot[1] = Factory()[name_1];
            spellslot[2] = Factory()[name_2];
            spellslot[3] = Factory()[name_3];
        }


        internal void Cast(Unit unit, Unit target, Spell spell, sbyte dice6, sbyte dice20)
        {

            sbyte value = 0;
            float prep_value = 0;
            
            switch(spell.type)
            {
                 case DamageType.Phisical:
                    if (dice20 == 1) { Console.WriteLine("Atack was miss"); break; }
                    prep_value = (float)
                    (unit.Atributes.strenght * spell.factor * (1 - target.Atributes.deffence * 0.1) + dice6 - target.Atributes.deffence > 0 ? dice6 - target.Atributes.deffence : 0);
                    break;

                 case DamageType.Magical:
                    if (dice20 == 1) { Console.WriteLine("Spell doesn't work"); break; }
                    prep_value = (float)
                    (unit.Atributes.intelligence * spell.factor * (1 - target.Atributes.resistance * 0.1) + dice6 - target.Atributes.resistance > 0 ? dice6 - target.Atributes.resistance : 0);
                    break;

                case DamageType.Other:
                    if (dice20 == 1) { Console.WriteLine("Something went wrong"); break; }
                    prep_value = (float)
                    (unit.Atributes.luck * spell.factor * 1 + dice6);
                    break;
                }

            if (spell.is_critable && dice20 + unit.Atributes.luck > 20)
            {
                prep_value *= 1.4f;
            }

            value = (sbyte)(prep_value + 0.5);

            Console.WriteLine($"{unit.name} used {spell.name} on {target.name}");

            if (spell.is_offensive)
            {
                target.Hurt(value);
            }
            else
            {
                target.Heal(value);
            }

            
            if (spell.is_critable)
            {
                Console.WriteLine("That was critial");
            }
        }
    }
}
