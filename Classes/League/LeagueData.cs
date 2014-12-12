using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_Stats.Classes.League
{
    public class LeagueData
    {
        public string name { get; set; }
        public string tier { get; set; }
        public string queue { get; set; }
        public JArray entries { get; set; }

        public miniSeries getSeries()
        {
            JObject league = (JObject)entries[0];
            JObject leagueSeries = (JObject)league["miniSeries"];
            miniSeries series = null;
            if (leagueSeries != null)
            {
                series = JsonConvert.DeserializeObject<miniSeries>(leagueSeries.ToString());
            }

            if (series == null)
            {
                return null;
            }
            else
            {
                return series;
            }

        }

        public string getDivision()
        {
            JObject league = (JObject)entries[0];
            Entries division = JsonConvert.DeserializeObject<Entries>(league.ToString());

            return division.division;
        }

        public int getLeaguePoints()
        {
            JObject league = (JObject)entries[0];
            Entries division = JsonConvert.DeserializeObject<Entries>(league.ToString());

            return division.leaguePoints;
        }

    }
}
