namespace DG.Haer.Domain
{
    public class Contact : Entity
    {
        public string Name { get; set; }
        public byte Experience { get; set; }
        public decimal Salary { get; set; }
        public ContactType ContactType { get; set; }

        public Contact()
        {

        }
    }
}
