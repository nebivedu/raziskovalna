using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
namespace raziskovalna2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Party : ContentPage
    {
        

        int stfilma = 0;
        public Party()
        {
            InitializeComponent();
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(partyNameCreate.Text))
            {
                await DisplayAlert("Alert", "Party name not valid", "OK");
            }
            else
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://apistran2002.eu");

                string jsonData = @"{""kljuc"" : """ + partyNameCreate.Text + @"""}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                //spremeni api naslov na takega ku dela(naredit ga se rabis)
                HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/insertskupina.php", content);
                var result = await response.Content.ReadAsStringAsync();
                //var filmi = JsonConvert.DeserializeObject<List<Filmi>>(result);

                App.Current.MainPage = new MainPage(partyNameCreate.Text, stfilma);
            }
            
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://apistran2002.eu");
            string name = partyNameJoin.Text;
            string jsonData = @"{""kljuc"" : """+name+@"""}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //spremeni api naslov na takega ku dela(naredit ga se rabis)
            HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/skupinaapi.php", content);
            var result = await response.Content.ReadAsStringAsync();
            string a = result;
            //await DisplayAlert("Alert", a, "OK");
            //var filmi = JsonConvert.DeserializeObject<List<Filmi>>(result);


            if (string.IsNullOrWhiteSpace(a))
            {
                
                await DisplayAlert("Alert", "Party does not exist", "OK");

            }
            else
            {
                App.Current.MainPage = new MainPage(partyNameJoin.Text, stfilma);
            }
                
            
            
        }
    }
}