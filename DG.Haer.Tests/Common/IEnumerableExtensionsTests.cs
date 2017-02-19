using DG.Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DG.Haer.Tests.Common
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class IEnumerableExtensionsTests
    {
        List<Item> _items;

        [SetUp]
        public void SetUp()
        {
            _items = new List<Item>()
            {
                new Item { Id = 1, Name = "Item1" },
                new Item { Id = 2, Name = "Item1" },
                new Item { Id = 3, Name = "Item2" },
                new Item { Id = 4, Name = "Item2" },
                new Item { Id = 5, Name = "Item3" },
            };
        }

        [Test]
        public void ConditionalWhereWorksProperlyForTrueCondition()
        {
            var result = _items.ConditionalWhere(() => true, x => x.Name == "Item2");

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(_items.ToArray()[2], result.ToArray()[0]);
            Assert.AreEqual(_items.ToArray()[3], result.ToArray()[1]);
        }

        [Test]
        public void ConditionalWhereWorksProperlyForFalseCondition()
        {
            var result = _items.ConditionalWhere(() => false, x => x.Name == "Item2");

            Assert.AreEqual(5, result.Count());
        }

        [Test]
        public void GetCountWorksProperly()
        {
            // Arange
            int testCount1;
            int testCount2;

            _items.GetCount(out testCount1);
            _items.RemoveAt(0);
            _items.GetCount(out testCount2);

            Assert.AreEqual(5, testCount1);
            Assert.AreEqual(4, testCount2);
        }
    }
}
