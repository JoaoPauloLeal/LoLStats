using LoL_Stats.Classes.gameStats;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_Stats.Classes
{
    public class statsgames
    {
        public int summonerId { get; set; }
        public IList<games> games { get; set; }
    }
}
