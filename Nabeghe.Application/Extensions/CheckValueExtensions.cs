using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Application.Extensions
{
    public static class CheckValueExtensions
    {
        public static bool CheckValue(this ICollection? collection)
        {
            if (collection != null && collection.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
