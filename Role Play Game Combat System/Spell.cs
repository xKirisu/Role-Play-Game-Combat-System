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

        bool is_critable;

        Spell(string name, float factor, sbyte cost, DamageType damage_type, bool is_critable)
        {
            this.name = name;
            this.factor = factor;
            this.cost = cost;

            this.type = damage_type;

            this.is_set_status = false;
            this.set_status = Status.None;

            this.is_critable = is_critable;
        }
        Spell(string name, float factor, sbyte cost, DamageType damage_type, Status status, bool is_critable)
        {
            this.name = name;
            this.factor = factor;
            this.cost = cost;

            this.type = damage_type;

            this.is_set_status = true;
            this.set_status = status;

            this.is_critable = is_critable;
        }
    }
    internal class Magic
    {
        Spell[] spellslot = new Spell[4];

        internal Spell[] Factory()
        {
            return new Spell[]
            {
                new Spell()
            };
        }
        internal Magic(Spell spell1, Spell spell2, Spell spell3) 
        {
            spellslot[0] = Factory()[0];
            spellslot[1] = spell1;
            spellslot[2] = spell2;
            spellslot[3] = spell3;
        }

        internal void Cast(Unit unit, Unit target, Spell spell)
        {

        }
    }
}
