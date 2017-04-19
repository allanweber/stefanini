using System;

namespace CustomerContracts.Core
{
    public static class Utilities
    {
        public static int ToInt(this string value)
        {
            int outInt = 0;
            if (int.TryParse(value, out outInt))
                return outInt;

            return 0;
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime outDateTime = DateTime.MinValue;
            if (DateTime.TryParse(value, out outDateTime))
                return outDateTime;

            return DateTime.MinValue;
        }
    }
}
