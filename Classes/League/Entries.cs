using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_Stats.Classes.League
{
    public class Entries
    {
        public string playerOrTeamId { get; set; }
        public string playerOrTeamName { get; set; }
        public string division { get; set; }
        public int leaguePoints { get; set; }
        public int wins { get; set; }
        public bool isHotStreak { get; set; }
        public bool isVeteran { get; set; }
        public bool isFreshBlood { get; set; }
        public bool isInactive { get; set; }
        public JObject series { get; set; }

    }
}
