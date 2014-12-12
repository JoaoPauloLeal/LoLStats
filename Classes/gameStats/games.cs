using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_Stats.Classes.gameStats
{
    public class games
    {
        public int championId{get;set;}
        public long createDate { get; set; }
        public IList<fellowPlayers> fellowPlayers{ get; set; }
        public int gameId { get; set; }
        public string gameMode{ get; set; }
        public string gameType{ get; set; }
        public bool invalid { get; set; }
        public int ipEarned { get; set; }
        public int level { get; set; }
        public int mapId { get; set; }
        public int spell1 { get; set; }
        public int spell2 { get; set; }
        public stats stats { get; set; }
        public string subType { get; set; }
        public int teamId { get; set; }

        public string getSubType()
        {
            switch (this.subType)
            {
                case "NONE":
                    return "Custom ";
                case "NORMAL":
                    return "Normal 5x5";
                case "NORMAL_3x3":
                    return "Normal 3x3";
                case "ODIN_UNRANKED":
                    return "Dominion";
                case "ARAM_UNRANKED_5x5":
                    return "ARAM";
                case "BOT":
                    return "BOT";
                case "BOT_3x3":
                    return "BOT 3x3";
                case "RANKED_SOLO_5x5":
                    return "Ranked Solo 5x5";
                case "RANKED_TEAM_3x3":
                    return "Ranked Team 3x3";
                case "RANKED_TEAM_5x5":
                    return "Ranked Team 5x5";
                case "ONEFORALL_5x5":
                    return "One for All";
                case "FIRSTBLOOD_1x1":
                    return "First Blood 1x1";
                case "FIRSTBLOOD_2x2":
                    return "First Blood 2x2";
                case "SR_6x6":
                    return "Hexakill Summoner's Rift";
                case "CAP_5x5":
                    return "Team Builder";
                case "URF":
                    return "URF";
                case "URF_BOT":
                    return "URF (BOT)";
                case "NIGHTMARE_BOT":
                    return "Doom Bots";
                case "ASCENSION":
                    return "Ascension";
                case "HEXAKILL":
                    return "Hexakill";
                case "KING_PORO":
                    return "King Poro";

            }
            return null;
        }

        public string getWin(){
            if (this.stats.win == true)
            {
                return "Victory";
            }
            else
            {
                return "Defeat";
            }
        }
    }
}
