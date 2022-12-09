using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigalev_School
{
    public partial class ClientService
    {
        public string RemainingTime
        {
            get
            {
                TimeSpan dateTime = StartTime - DateTime.Now;
                return "" + dateTime.ToString("hh") + " часов " + dateTime.ToString("mm") + " минут";
            }
        }
    }
}
