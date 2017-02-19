using DG.Haer.Domain;
using System.Collections.Generic;
using System.Data.Entity;

namespace DG.Haer.Data
{
    public class AppDbDataSeeder : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            GetContacts().ForEach(x => context.Contacts.Add(x));

            context.Commit();
        }

        public static List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact
                {
                    Name = "Dariusz Grabacz",
                    ContactType = ContactType.Programmer,
                    Experience = 1,
                    Salary = 2625
                },
                new Contact
                {
                    Name = "Mateusz Nowak",
                    ContactType = ContactType.Programmer,
                    Experience = 2,
                    Salary = 2750
                },
                new Contact
                {
                    Name = "Jarek Mach",
                    ContactType = ContactType.Tester,
                    Experience = 1,
                    Salary = 2600
                },
                new Contact
                {
                    Name = "Dominika Kam",
                    ContactType = ContactType.Programmer,
                    Experience = 3,
                    Salary = 5375
                },
                new Contact
                {
                    Name = "Bronisław Komor",
                    ContactType = ContactType.Programmer,
                    Experience = 1,
                    Salary = 2625
                },
                new Contact
                {
                    Name = "Donald Wysocki",
                    ContactType = ContactType.Tester,
                    Experience = 1,
                    Salary = 2600
                },
                new Contact
                {
                    Name = "Jacek Pytel",
                    ContactType = ContactType.Tester,
                    Experience = 2,
                    Salary = 3575
                },
                new Contact
                {
                    Name = "Dominik Kowalski",
                    ContactType = ContactType.Tester,
                    Experience = 5,
                    Salary = 4500
                },
                new Contact
                {
                    Name = "Marcin Kam",
                    ContactType = ContactType.Tester,
                    Experience = 3,
                    Salary = 3675
                },
                new Contact
                {
                    Name = "Mariusz Kowalski",
                    ContactType = ContactType.Programmer,
                    Experience = 8,
                    Salary = 6500
                },
                new Contact
                {
                    Name = "Michał Gondzik",
                    ContactType = ContactType.Tester,
                    Experience = 1,
                    Salary = 2600
                },
                new Contact
                {
                    Name = "Adam Małysz",
                    ContactType = ContactType.Tester,
                    Experience = 4,
                    Salary = 3775
                },
            };
        }
    }
}
