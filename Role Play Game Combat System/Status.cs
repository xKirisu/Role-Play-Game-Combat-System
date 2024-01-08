 
namespace rpgcs
{
    internal enum Status
    {
        None, 
        Fainted,        //Ignore turn and enemy selecting
        Blessing,       //Atribute luck +3
        Calamity,       //Atribute luck -3
        Prowess,        //Atribute strenght +1
        Wisdom,         //Atribute inteligence -1
        Taunt,          //Skip turn and clean status
        Swift,          //Atribute speed +2
        Delay,          //Atribute speed -2
        Distracting,    //Focus enemy to ownself
        Poisoned,       //Damage before turn and dice for remove
        Protected       //Atribute deffence and resistance +1
    }
    internal partial class Unit
    {
        internal partial void ExecuteStatus(Dice dice)
        {
            switch (Status)
            {
                case Status.Blessing:

                    break;
                case Status.Calamity:

                    break;
                case Status.Prowess:

                    break;
                case Status.Wisdom:

                    break;

                case Status.Taunt:

                    break;
                case Status.Swift:

                    break;
                case Status.Delay:

                    break;
                case Status.Poisoned:
                    sbyte dmg = (sbyte)(dice.d6() / 2);
                    Console.WriteLine($"{name} is hurt by poison for {dmg}");
                    Hurt(dmg);

                    break;
                case Status.Protected:

                    break;
            }

        }
        internal partial void ChangeStatus(Status new_status)
        {
            Atributes.luck = BaseAtributes.luck;
            Atributes.strenght = BaseAtributes.strenght;
            Atributes.intelligence = BaseAtributes.intelligence;
            Atributes.speed = BaseAtributes.speed;

            Atributes.max_mana = BaseAtributes.max_mana;
            Atributes.max_vitality = BaseAtributes.max_vitality;

            this.Status = new_status;
        }
    }
}