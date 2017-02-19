using DG.Haer.Domain;
using System.Collections;

namespace DG.Haer.Tests
{
    public class ContactsComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return Compare((Contact)x, (Contact)y);
        }

        public int Compare(Contact x, Contact y)
        {
            if (x.Id == y.Id && x.Name == y.Name && x.Experience == y.Experience && x.Salary == y.Salary && x.ContactType == y.ContactType)
                return 0;
            return x.Id > y.Id ? 1 : -1;
        }
    }
}
