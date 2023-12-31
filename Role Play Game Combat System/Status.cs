
namespace rpgcs
{
    internal enum Status
    {
        None, Fainted
    }
    internal partial class Unit
    {
        internal partial void ExecuteStatus()
        {


        }
        internal partial void ChangeStatus(Status new_status)
        {
            this.Status = new_status;
        }
    }
}