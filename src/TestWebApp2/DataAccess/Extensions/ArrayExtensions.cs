using System;
using System.Collections.Generic;
using System.Text;

namespace TestWebApp2.DataAccess.Extensions
{
    public static class ArrayExtensions
    {
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength == 0) return;
            ArrayTraverse walker = new ArrayTraverse(array);
            do action(array, walker.Position);
            while (walker.Step());
        }
    }
}
