using LoL_Stats.Classes.League;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_Stats.Classes
{
    public class Summoner
    {
        public String name { get; set; }
        public String id { get; set; }
        public String level { get; set; }
        public String iconId { get; set; }
        public String league { get; set; }
        public String division { get; set; }
        public int leaguePoints { get; set; }
        public miniSeries series { get; set; }

        
    }
}
