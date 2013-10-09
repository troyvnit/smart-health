using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VinaSale.Core.ApplicationServices.Caching
{
    public static class CacheKeys
    {
        public static string UserBasic(int userId)
        {
            return String.Format("UserBasic_{0}", userId);
        }
    }
}
