 
namespace rpgcs
{
    internal enum Status
    {
        None, 
        Fainted,        //Ignore turn and enemy selecting
        Blessing,       //Atribute luck +3
        Calamity,       //Atribute luck -3
        Prowess,        //Atribute strenght +2
        Wisdom,         //Atribute inteligence +2
        Swift,          //Atribute speed +4
        Delay,          //Atribute speed -4
        Distracting,    //Focus enemy to ownself
        Poisoned,       //Damage before turn and dice for remove
        Protected,      //Atribute deffence and resistance +1
        Weakness,       //Atribute deffence and resistance -1
    }
    internal partial class Unit
    {
        internal partial void ExecuteStatus(Dice dice)
        {
            switch (Status)
            {
                case Status.Poisoned:
                    sbyte dmg = (sbyte)(dice.d6() / 2);
                    Console.WriteLine($"{name} is hurt by poison for {dmg}");
                    Hurt(dmg);

                    break;
                case Status.Prowess:
                case Status.Wisdom:
                    this.Atributes.mana += (sbyte)(dice.d6() / 2);
                    Console.WriteLine($"Mana was recovered by {(sbyte)(dice.d6() / 2)}");
                    break;

            }

        }
        internal partial void ChangeStatus(Status new_status)
        {
            this.Atributes.luck = BaseAtributes.luck;
            this.Atributes.strenght = BaseAtributes.strenght;
            this.Atributes.intelligence = BaseAtributes.intelligence;
            this.Atributes.speed = BaseAtributes.speed;

            this.Atributes.max_mana = BaseAtributes.max_mana;
            this.Atributes.max_vitality = BaseAtributes.max_vitality;

            switch (new_status)
            {
                case Status.Blessing:
                    this.Atributes.luck += 3;
                    break;
                case Status.Calamity:
                    this.Atributes.luck -= 3;
                    break;
                case Status.Prowess:
                    this.Atributes.strenght += 2;
                    break;
                case Status.Wisdom:
                    this.Atributes.intelligence += 2;
                    break;
                case Status.Swift:
                    this.Atributes.speed += 4;
                    break;
                case Status.Delay:
                    this.Atributes.speed -= 4;
                    break;
                case Status.Protected:
                    this.Atributes.deffence += 1;
                    this.Atributes.resistance += 1;
                    break;
                case Status.Weakness:
                    this.Atributes.deffence -= 1;
                    this.Atributes.resistance -= 1;
                    if (this.Atributes.resistance < 0 || this.Atributes.deffence < 0)
                    {
                        this.Atributes.deffence = 0;
                        this.Atributes.resistance = 0;
                    }
                    break;
            }


            this.Status = new_status;
        }
    }
}