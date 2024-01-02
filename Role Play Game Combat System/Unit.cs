using System.Reflection.Metadata.Ecma335;

namespace rpgcs
{
    /// <summary>
    /// Statistics
    /// 
    /// </summary>
    internal struct Statistic
    {
        internal sbyte strenght;
        internal sbyte intelligence;

        internal sbyte vitality;
        internal sbyte max_vitality;

        internal sbyte mana;
        internal sbyte max_mana;

        internal sbyte deffence;
        internal sbyte resistance;

        internal sbyte speed;
        internal sbyte luck;
        private object strength;

        internal Statistic(sbyte strength, sbyte intelligence, sbyte vitality, sbyte mana, sbyte deffence, sbyte resistance, sbyte speed, sbyte luck)
        {
            this.strength = strength;
            this.intelligence = intelligence;
            this.vitality = vitality;
            this.max_vitality = vitality;
            this.mana = mana;
            this.max_mana = mana;
            this.deffence = deffence;
            this.resistance = resistance;
            this.speed = speed;
            this.luck = luck;
        }

    }
    /// <summary>
    /// 
    /// </summary>
    internal partial class Unit
    {
        public string name;
        // Ooutsiders
        internal Statistic Atributes;
        protected Magic Spellbook;
        internal Status Status = Status.None;
        

        /// <summary>
        /// Core of setting unit
        /// </summary>
        protected Unit(string name, Statistic atributes, Magic spellbook)
        {
            this.name = name;
            Atributes = atributes;
            Spellbook = spellbook;


        }
        public virtual void TakeAnAction(List<Unit> queue, Dice dice)
        {
            Console.WriteLine($"{name} have an action");
            ExecuteStatus(dice);
        }

        /// <summary>
        /// Hurt and die
        /// </summary>
        internal void Hurt(sbyte damage)
        {
            Atributes.vitality -= damage;
            Console.WriteLine($"{name} was hurt for {damage}");

            if (Atributes.vitality <= 0 ) 
            {
                Die();
            }
        }
        internal void Heal(sbyte value)
        {
            Atributes.vitality += value;
            Console.WriteLine($"{name} was heal for {value}");

            if (Atributes.vitality > Atributes.max_vitality)
            {
                Atributes.vitality = Atributes.max_vitality;
            }
        }
        protected void Die()
        {
            ChangeStatus(Status.Fainted);
            Console.WriteLine($"{name} was fainted");
        }

        /// <summary>
        /// Status usage function
        /// </summary>
        internal partial void ExecuteStatus(Dice dice);
        internal partial void ChangeStatus(Status new_status);
    }


}
