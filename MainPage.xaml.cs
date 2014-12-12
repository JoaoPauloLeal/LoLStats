using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LoL_Stats.Resources;
using Newtonsoft.Json.Linq;
using LoL_Stats.Classes;
using LoL_Stats.Pages;

namespace LoL_Stats
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string key = ""; //put your key here
        Summoner s = new Summoner();
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            String[] servers = {"BR", "EUNE", "EUW", "KR", "LAN", "LAS", "NA", "OCE", "RU", "TR"};
            server.ItemsSource = servers;


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (summoner.Text == String.Empty)
            {
                MessageBox.Show("Preencha o Nome!");
            }
            else
            {
                string regionEndpoint = Infos.getRegionEndpoint(((String)server.SelectedItem).ToLower());
                
                WebClient jsonClient = new WebClient();
                jsonClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(jsonClient_DownloadStringCompleted);

                jsonClient.DownloadStringAsync(new Uri(@Uri.EscapeUriString(("http://" + regionEndpoint + "/api/lol/" + server.SelectedItem + "/v1.4/summoner/by-name/" + summoner.Text + "?api_key=" + key).ToLower())));

            }
            
        }

        private void jsonClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
             try
            {
                string json = e.Result;
                JObject jobject = JObject.Parse(json);

                string name = summoner.Text;
                while (name.IndexOf(" ") >= 0)
                {
                    name = name.Replace(" ", "");
                }
                JObject jobjectsummoner = (JObject)jobject[name.ToLower()];

                s.name = (String)jobjectsummoner["name"];
                s.id = (String)jobjectsummoner["id"];
                s.level = (String)jobjectsummoner["summonerLevel"];
                s.iconId = (String)jobjectsummoner["profileIconId"];


                NavigationService.Navigate(new Uri("/Pages/Stats.xaml?server="+server.SelectedItem, UriKind.Relative));
            }
            catch
            {
                MessageBox.Show("Summoner not found");
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Stats page = e.Content as Stats;
            if(page != null){
                page.summoner = s;
            }                   
        }

    }
}