using Custom.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Collections.Tests
{
    public class EnumeratorTests
    {
        private MyList<int> ValueTypeList => new MyList<int>(5) { 1, 2, 3, 4, 5 };

        [Fact]
        [Trait("MyList", "Enumerator")]
        public void TestMethod1()
        {
            var list = ValueTypeList;
            var builder = new StringBuilder();
            foreach (var val in list)
            {
                builder.Append(val);
                builder.Append(", ");
            }
            Assert.Equal(builder.ToString(0, builder.Length - 2), list.ToString());
        }

        [Fact]
        [Trait("MyList", "Enumerator")]
        public void TestMethod2()
        {
            var list = ValueTypeList;

            var enum1 = list.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() => enum1.Current);
            Assert.True(enum1.MoveNext());
            Assert.Equal(enum1.Current, 1);

            var enum2 = list.GetEnumerator();
            enum2.MoveNext();
            enum2.MoveNext();
            Assert.Equal(enum2.Current, 2);

            list.RemoveAt(0);
            Assert.Throws<InvalidOperationException>(() => enum1.Current);
            Assert.Throws<InvalidOperationException>(() => enum2.Current);

            var enum3 = list.GetEnumerator();
            enum3.MoveNext();
            Assert.Equal(enum3.Current, 2);
        }

        [Fact]
        [Trait("MyList", "Enumerator")]
        public void TestMethod3()
        {
            var list = new MyList<object>();
            var enumerator = list.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() => enumerator.Current);
            Assert.False(enumerator.MoveNext());
            Assert.Throws<InvalidOperationException>(() => enumerator.Current);
        }
    }
}
