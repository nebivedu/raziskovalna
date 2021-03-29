using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using RefreshView = Xamarin.Forms.RefreshView;
using System.Windows.Input;

namespace raziskovalna2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Second : ContentPage
    {
        string skupincaID;
        string PartyName;
        //string ime_filma;
        //string haha;
        
        int i = 0;
        public Second(string skupinaID,string imeskup)
        {
            InitializeComponent();
            skupincaID = skupinaID;
            PartyName = imeskup;
            Party.Text = "Party name: " + PartyName;
            Topfilmi();
            

        }
        
        private async void Topfilmi()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://apistran2002.eu");
            string jsonData = @"{""skupina_id"" : """ + skupincaID + @"""}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/hexte-test.php", content);

            var result = await response.Content.ReadAsStringAsync();
            var skupine = JsonConvert.DeserializeObject<List<Filmi>>(result);
            //ime_filma = skupine[0].naslov;
            //await DisplayAlert("Alert", ime_filma, "OK");
            TopFilm1.Text = skupine[0].naslov;
            TopFilm2.Text = skupine[1].naslov;
            TopFilm3.Text = skupine[2].naslov;

        }
        private void Button_Clicked_Home(object sender, EventArgs e)
        {
            App.Current.MainPage = new Party();
        }
        private void Button_Clicked_Refresh(object sender, EventArgs e)
        {
            Topfilmi();

        }
        
    }

}