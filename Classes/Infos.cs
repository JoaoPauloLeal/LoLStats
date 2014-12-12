using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoL_Stats.Classes
{
    class Infos
    {
        public static string getRegionEndpoint(string region){
            switch (region)
            {
                case "br":
                    return "br.api.pvp.net";

                case "eune":
                    return "eune.api.pvp.net";

                case "euw":
                    return "euw.api.pvp.net";

                case "kr":
                    return "kr.api.pvp.net";

                case "las":
                    return "las.api.pvp.net";

                case "lan":
                    return "lan.api.pvp.net";

                case "na":
                    return "na.api.pvp.net";

                case "oce":
                    return "oce.api.pvp.net";

                case "tr":
                    return "tr.api.pvp.net";

                case "ru":
                    return "ru.api.pvp.net";

                case "global":
                    return "global.api.pvp.net";

                default:
                    return null;

            }
        }
    }
}
