using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.UI
{
    internal class HelperClass
    {
        //Checks if user entered passed date
        public static bool IsEarlierDate(DateTime date)
        {
            if (DateTime.Compare(date, DateTime.Now) >= 0)
                return false;
            return true;
        }
    }
}
