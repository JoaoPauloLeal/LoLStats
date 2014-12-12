using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.UI;

namespace LoL_Stats.Classes
{
    public class ItemsBox
    {
        public BitmapImage champion { get; set; }
        public BitmapImage spell1 { get; set; }
        public BitmapImage spell2 { get; set; }
        public BitmapImage item0 { get; set; }
        public BitmapImage item1 { get; set; }
        public BitmapImage item2 { get; set; }
        public BitmapImage item3 { get; set; }
        public BitmapImage item4 { get; set; }
        public BitmapImage item5 { get; set; }
        public BitmapImage item6 { get; set; }
        public string win { get; set; }
        public string subType { get; set; }
        public int kill { get; set; }
        public int death { get; set; }
        public int assist { get; set; }
        public int gold { get; set; }

    }
}
