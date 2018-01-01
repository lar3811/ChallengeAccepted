using Custom.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Collections.Tests
{
    public class ListTests
    {
        private MyList<int> ValueTypeList => new MyList<int>(5) { 1, 2, 3, 4, 5 };

        [Fact]
        [Trait("MyList", "General")]
        public void TestMethod1()
        {
            var list = ValueTypeList;
            Assert.Equal(list.ToString(), "1, 2, 3, 4, 5");
            Assert.Equal(5, list.Count);
            Assert.True(list.Capacity >= list.Count);

            list.RemoveAt(2);
            Assert.Equal(list.ToString(), "1, 2, 4, 5");
            Assert.Equal(4, list.Count);
            Assert.True(list.Capacity >= list.Count);

            list.Insert(2, 3);
            Assert.Equal(list.ToString(), "1, 2, 3, 4, 5");
            Assert.Equal(5, list.Count);
            Assert.True(list.Capacity >= list.Count);

            list.Add(6);
            Assert.Equal(list.ToString(), "1, 2, 3, 4, 5, 6");
            Assert.Equal(6, list.Count);
            Assert.True(list.Capacity >= list.Count);

            Assert.True(list.Remove(1));
            Assert.Equal(list.ToString(), "2, 3, 4, 5, 6");
            Assert.Equal(5, list.Count);
            Assert.True(list.Capacity >= list.Count);

            Assert.False(list.Remove(1));
            Assert.Equal(list.ToString(), "2, 3, 4, 5, 6");
            Assert.Equal(5, list.Count);
            Assert.True(list.Capacity >= list.Count);

            Assert.True(list.Contains(4));
            Assert.False(list.Contains(1));

            Assert.Equal(2, list.IndexOf(4));
            Assert.Equal(-1, list.IndexOf(1));

            Assert.Equal(6, list[4]);
            Assert.Equal(2, list[0]);

            list.Clear();
            Assert.Equal(0, list.Count);
            Assert.True(list.Capacity >= list.Count);
        }

        [Fact]
        [Trait("MyList", "General")]
        public void TestMethod2()
        {
            var list = ValueTypeList;
            Assert.Throws<IndexOutOfRangeException>(() => list[5]);
            Assert.Throws<IndexOutOfRangeException>(() => list[-1]);
            Assert.Throws<IndexOutOfRangeException>(() => list.Insert(-1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => list.Insert(6, 5));
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-1));
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(5));
        }
    }
}
