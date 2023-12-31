
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

        internal Statistic(sbyte strength, sbyte intelligence, sbyte vitality, sbyte maxVitality, sbyte mana, sbyte maxMana, sbyte deffence, sbyte resistance, sbyte speed, sbyte luck)
        {
            this.strength = strength;
            this.intelligence = intelligence;
            this.vitality = vitality;
            this.max_vitality = maxVitality;
            this.mana = mana;
            this.max_mana = maxMana;
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
        // Ooutsiders
        protected Statistic Atrubutes;
        protected Magic Spellbook;
        protected Status Status = Status.None;
        

        /// <summary>
        /// Core of setting unit
        /// </summary>
        Unit(Statistic atributes, Magic spellbook)
        {
            Atrubutes = atributes;
            Spellbook = spellbook;


        }
        protected virtual void TakeAnAction() { }

        /// <summary>
        /// Hurt and die
        /// </summary>
        protected void Hurt(sbyte damage)
        {
            Atrubutes.vitality -= damage;

            if(Atrubutes.vitality <= 0 ) 
            {

            }
        }
        protected void Die()
        {
            ChangeStatus(Status.Fainted);
        }

        /// <summary>
        /// Status usage function
        /// </summary>
        internal partial void ExecuteStatus();
        internal partial void ChangeStatus(Status new_status);
    }


}
