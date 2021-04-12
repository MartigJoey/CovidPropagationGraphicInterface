using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPropagationGraphicInterface
{
    public static class ListExtensions
    {
        public static int CountFromZero<T>(this List<T> list)
        {
            return list.Count - 1;
        }
    }
}
