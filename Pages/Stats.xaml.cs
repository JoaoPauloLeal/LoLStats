using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LoL_Stats.Classes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using LoL_Stats.Classes.League;
using System.IO;
using LoL_Stats.Classes.gameStats;

namespace LoL_Stats.Pages
{
    public partial class Stats : PhoneApplicationPage
    {

        public Summoner summoner { get; set; }
        private const string key = ""; //put your key here
        string server;
        string regionEndpoint;
        public Stats()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            base.OnNavigatedTo(e);

            StatsTitle.Text = summoner.name;
            txtLevel.Text = "Level: " + summoner.level;

            string msg;
            if (NavigationContext.QueryString.TryGetValue("server", out msg))
            {
                server = msg;
                regionEndpoint = Infos.getRegionEndpoint(server.ToLower());
            }

            //get the league and division - update the image of these things

                WebClient getRank = new WebClient();
                getRank.DownloadStringCompleted += new DownloadStringCompletedEventHandler(getRank_DownloadStringCompleted);         
                getRank.DownloadStringAsync(new Uri(@Uri.EscapeUriString(("https://" + regionEndpoint + "/api/lol/" + server + "/v2.5/league/by-summoner/" + summoner.id + "/entry?api_key=" + key).ToLower())));
                BitmapImage bm = new BitmapImage(new Uri(@"/Images/profileicon/" + summoner.iconId + ".png", UriKind.RelativeOrAbsolute));
                profileIcon.Source = bm;
            
            //get the games' stats
            WebClient getStats = new WebClient();
            getStats.DownloadStringCompleted += new DownloadStringCompletedEventHandler(getStats_DownloadStringCompleted);
            getStats.DownloadStringAsync(new Uri(@Uri.EscapeUriString(("http://" + regionEndpoint + "/api/lol/" + server + "/v1.3/game/by-summoner/" + summoner.id + "/recent?api_key=" + key).ToLower())));

        }

        private void getRank_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if(e.Error != null){
                txtRank.Text = "UNRANKED";
                BitmapImage cm = new BitmapImage(new Uri(@"/Images/ranked/unknown.png", UriKind.RelativeOrAbsolute));
                rank.Source = cm;
            }
            else 
            {
                string json = e.Result;
                JObject jobject = JObject.Parse(json);
                JArray jobjectLeague = (JArray)jobject[summoner.id];
                JObject stats = (JObject)jobjectLeague[0];

                LeagueData data = JsonConvert.DeserializeObject<LeagueData>(stats.ToString());
                summoner.league = data.tier;
                summoner.division = data.getDivision();
                summoner.leaguePoints = data.getLeaguePoints();
                summoner.series = data.getSeries();

                txtRank.Text = summoner.league + " " + summoner.division + " (" + summoner.leaguePoints + " LP)";

                try
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/ranked/" + summoner.league + "_" + summoner.division + ".png", UriKind.RelativeOrAbsolute));
                    rank.Source = cm;
                }
                catch
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/ranked/unknown.png", UriKind.RelativeOrAbsolute));
                    rank.Source = cm;
                }
            }
            if(summoner.series != null)
                series();            
        }

        private void getStats_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string json = e.Result;
            JObject jobject = JObject.Parse(json);

            statsgames statsgames = JsonConvert.DeserializeObject<statsgames>(json);

            List<ItemsBox> win = new List<ItemsBox>();;
            foreach (games x in statsgames.games)
            {
                BitmapImage champ = new BitmapImage(new Uri(@"/Images/champion/"+x.championId+".png", UriKind.Relative));
                BitmapImage spell1 = new BitmapImage(new Uri(@"/Images/spells/" + x.spell1 + ".png", UriKind.Relative));
                BitmapImage spell2 = new BitmapImage(new Uri(@"/Images/spells/" + x.spell2 + ".png", UriKind.Relative));
                BitmapImage item0 = new BitmapImage(new Uri(getUriItem(x.stats.item0), UriKind.Relative));
                BitmapImage item1 = new BitmapImage(new Uri(getUriItem(x.stats.item1), UriKind.Relative));
                BitmapImage item2 = new BitmapImage(new Uri(getUriItem(x.stats.item2), UriKind.Relative));
                BitmapImage item3 = new BitmapImage(new Uri(getUriItem(x.stats.item3), UriKind.Relative));
                BitmapImage item4 = new BitmapImage(new Uri(getUriItem(x.stats.item4), UriKind.Relative));
                BitmapImage item5 = new BitmapImage(new Uri(getUriItem(x.stats.item5), UriKind.Relative));
                BitmapImage item6 = new BitmapImage(new Uri(getUriItem(x.stats.item6), UriKind.Relative));
                string txtWin = x.getWin();
                string subType = x.getSubType();

                win.Add(new ItemsBox { champion = champ, 
                                       spell1 = spell1, 
                                       spell2 = spell2,
                                       item0 = item0,
                                       item1 = item1,
                                       item2 = item2,
                                       item3 = item3,
                                       item4 = item4,
                                       item5 = item5,
                                       item6 = item6,
                                       win = txtWin,
                                       subType = subType,
                                       kill = x.stats.championsKilled,
                                       death = x.stats.numDeaths,
                                       assist = x.stats.assists,
                                       gold = x.stats.goldEarned});
                
            }
            lstStats.ItemsSource = win;

        }

        private string getUriItem(int item)
        {
            if (item != 0)
            {
                return @"/Images/items/" + item + ".png";
            }
            else
            {
                return @"/Images/items/null.png";
            }
        }

        private void series()  //TODO improve this method
        {
            //serie 0
            if (summoner.series.progress[0] == 'W')
            {
                BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/win.png", UriKind.RelativeOrAbsolute));
                serie0.Source = cm;
            }
            else
            {
                if (summoner.series.progress[0] == 'L')
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/lose.png", UriKind.RelativeOrAbsolute));
                    serie0.Source = cm;
                }
                else
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/null.png", UriKind.RelativeOrAbsolute));
                    serie0.Source = cm;
                }
            }
            //serie1
            if (summoner.series.progress[1] == 'W')
            {
                BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/win.png", UriKind.RelativeOrAbsolute));
                serie1.Source = cm;
            }
            else
            {
                if (summoner.series.progress[1] == 'L')
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/lose.png", UriKind.RelativeOrAbsolute));
                    serie1.Source = cm;
                }
                else
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/null.png", UriKind.RelativeOrAbsolute));
                    serie1.Source = cm;
                }
            }
            //serie2
            if (summoner.series.progress[2] == 'W')
            {
                BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/win.png", UriKind.RelativeOrAbsolute));
                serie2.Source = cm;
            }
            else
            {
                if (summoner.series.progress[2] == 'L')
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/lose.png", UriKind.RelativeOrAbsolute));
                    serie2.Source = cm;
                }
                else
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/null.png", UriKind.RelativeOrAbsolute));
                    serie2.Source = cm;
                }
            }
            if (summoner.series.progress.Length == 5)
            {
                //serie3
                if (summoner.series.progress[3] == 'W')
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/win.png", UriKind.RelativeOrAbsolute));
                    serie3.Source = cm;
                }
                else
                {
                    if (summoner.series.progress[3] == 'L')
                    {
                        BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/lose.png", UriKind.RelativeOrAbsolute));
                        serie3.Source = cm;
                    }
                    else
                    {
                        BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/null.png", UriKind.RelativeOrAbsolute));
                        serie3.Source = cm;
                    }
                }
                //serie4
                if (summoner.series.progress[4] == 'W')
                {
                    BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/win.png", UriKind.RelativeOrAbsolute));
                    serie4.Source = cm;
                }
                else
                {
                    if (summoner.series.progress[4] == 'L')
                    {
                        BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/lose.png", UriKind.RelativeOrAbsolute));
                        serie4.Source = cm;
                    }
                    else
                    {
                        BitmapImage cm = new BitmapImage(new Uri(@"/Images/series/null.png", UriKind.RelativeOrAbsolute));
                        serie4.Source = cm;
                    }
                }
            }
        }
    }
}