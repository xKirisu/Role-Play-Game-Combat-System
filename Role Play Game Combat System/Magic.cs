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

        internal Spell(string name, float factor, sbyte cost, DamageType damage_type, bool is_critable, bool is_offensive)
        {
            this.name = name;
            this.factor = factor;
            this.cost = cost;

            this.type = damage_type;

            this.is_set_status = false;
            this.set_status = Status.None;

            this.is_critable = is_critable;
        }
        internal Spell(string name, float factor, sbyte cost, DamageType damage_type, Status status, bool is_critable, bool is_offensive)
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

        internal Dictionary<string, Spell> spellslot = new Dictionary<string, Spell>();

        internal Dictionary<string, Spell> Factory()
        {
            return new Dictionary<string, Spell>
            {
                                                // name                 factor  cost     damagetype      change status critable offensive
                { "Atack",              new Spell("Atack",              1f,     0, DamageType.Phisical,                true, true) },
                { "Fire Ball",          new Spell("Fire ball",          1.4f,   2, DamageType.Magical,                 false, true) },
                { "Lightning",          new Spell("Lightning",          1.8f,   6, DamageType.Magical, Status.Taunt,   true, true) },
                { "Sorrow",             new Spell("Sorrow",             0.5f,   2, DamageType.Magical, Status.Calamity,false, true) },
                { "Heal",               new Spell("Heal",               1f,     4, DamageType.Magical,                 true, false) },
                { "Blessing",           new Spell("Blessing",           0.5f,   6, DamageType.Magical, Status.Blessing,false, false) },
                { "Divine Chastiment",  new Spell("Divine Chastiment",  1.8f,   8, DamageType.Magical, Status.Taunt,   true, true ) },
                { "Condamnation",       new Spell("Condamnation",       1.4f,   6, DamageType.Phisical,                true, true) },
                { "Protection",         new Spell("Protection",         0f,     4, DamageType.Phisical,Status.Protected,false, false) },
                { "Distraction",        new Spell("Distraction",        0.5f,   8, DamageType.Phisical,Status.Distracting,true, true) },
                { "Punishment",         new Spell("Punishment",         1.8f,   6, DamageType.Other,                     true, true) },
                { "Decapitation",       new Spell("Decapitation",       2f,     8, DamageType.Other,                     true, true) },
                { "Perish",             new Spell("Perish",             2f,     8, DamageType.Magical,               true, true) },
                { "Ice Shard",          new Spell("Ice Shard",          1.8f,   8, DamageType.Magical,               true, true) },
                { "Hail",               new Spell("Hail",               0.5f,   4, DamageType.Magical, Status.Delay, false, true) },
                { "Black Tome",         new Spell("Black Tome",         0.0f,   4, DamageType.Magical, Status.Wisdom, false, false) },
                { "Unshod Shadow",      new Spell("Unshod Shadow",      0f,     4, DamageType.Phisical, Status.Swift, false, false) },
                { "Deathly Reap",       new Spell("Deathly Reap",       2f,     8, DamageType.Magical,               true, true) },
                { "Virgin Fate",        new Spell("Virgin Fate",        0.5f,   4, DamageType.Magical, Status.Prowess, false, false) },
                { "Swat",               new Spell("Swat",               1.4f,   4, DamageType.Phisical, Status.Taunt, true, true) },
                { "Atack Order",        new Spell("Atack Order",        0.0f,   4, DamageType.Phisical, Status.Prowess, false, false) },
                { "Regenerate Mucus",   new Spell("Regenerate Mucus",   1.4f,   4, DamageType.Magical,                  true, false) },
                { "Toxic Ooze",         new Spell("Toxic Ooze",         1.4f,   4, DamageType.Magical, Status.Poisoned, true, true) },
                { "Toxic Dagger",       new Spell("Toxic Dagger",       1.8f,   6, DamageType.Phisical, Status.Poisoned, true, true) },
                { "Trick",              new Spell("Trick",              1.8f,   8, DamageType.Other,    Status.Taunt, false, true) },

                { "NULL",               new Spell("NULL", 0f, 0, DamageType.Other, Status.None, false, false) },

            };
        }


        internal Magic(string name_1, string name_2, string name_3) 
        {
            spellslot.Add("Atack", Factory()["Atack"]);
            if(name_1 != "Atack") spellslot.Add(name_1, Factory()[name_1]);
            if (name_2 != "Atack") spellslot.Add(name_2, Factory()[name_2]);
            if (name_3 != "Atack") spellslot.Add(name_3, Factory()[name_3]);

        }


        internal static void Cast(Unit unit, Unit target, Spell spell, byte dice6, byte dice20)
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
