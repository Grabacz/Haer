using DG.Common;
using System.Collections.Generic;

namespace DG.Haer.Api.Infrastructure.Core
{
    public class SearchSet
    {
        public int SelectedPage { get; set; } = 1;
        public ContactsFilter Filter { get; set; }

        public SearchSet()
        {
            Filter = new ContactsFilter();
        }

        public SearchSet(int selectedPage, ContactsFilter filter)
        {
            SelectedPage = selectedPage;
            Filter = filter;
        }
    }
}
