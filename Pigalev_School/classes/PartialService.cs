using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Pigalev_School
{
    public partial class Service
    {
        public float DurationInMinute
        {
            get
            {
                float time = DurationInSeconds / 60;
                return time;
            }
        }
        public double CurrentPrice
        {
            get
            {
                if(Discount != null)
                {
                    double price = Convert.ToDouble(Cost) - (Convert.ToDouble(Cost) / 100 * Convert.ToDouble(Discount));
                    return price;
                }
                else
                {
                    return Convert.ToDouble(Cost);
                }
            }
        }
        public SolidColorBrush TimeColor
        {
            get
            {
                if (Discount != null)
                {
                    SolidColorBrush IsDiscount = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                    return IsDiscount;
                }
                else
                {
                    SolidColorBrush NotDiscount = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    return NotDiscount;
                }
            }
        }
    }
}
