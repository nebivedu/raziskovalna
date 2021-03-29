using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace raziskovalna2
{
    public partial class MainPage : ContentPage
    {
        string PartyName;
        string stfilmov;
        //int i = 0;
        int stfilma = 0;
        string id;
        string ime_filma;
        string opis;
        string url_video;
        string url_slika;
        string ocena;
        string skupinaID;
        int stfilm;
        public MainPage(string imeParty,int stfilm1)
        {
            InitializeComponent();
            
            GetMovies();
            stfilma = stfilm1;
            PartyName = imeParty;
            stfilm = Convert.ToInt32(stfilmov);
            Party.Text = "Party name: "+PartyName ;
            
            GetIdSkupine();
            


        }
        

        private async void GetMovies()
        {
            if (stfilm == 0)
            {
                
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://apistran2002.eu/tinder/countfilmi.php");
    
                var filmi = JsonConvert.DeserializeObject<List<kolkfilmov>>(response);

                stfilmov = filmi[0].num;
                stfilm = Convert.ToInt32(stfilmov);
            }
            
            
            if (stfilma < stfilm)
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("http://apistran2002.eu/tinder/vsifilmi.php");
                var filmi = JsonConvert.DeserializeObject<List<Filmi>>(response);

                id = filmi[stfilma].id;
                ime_filma = filmi[stfilma].naslov;
                opis = filmi[stfilma].opis;
                url_slika = filmi[stfilma].url_slika;
                url_video = filmi[stfilma].url_video;
                ocena = filmi[stfilma].ocena;

                //await DisplayAlert("Alert", ime_filma + id , "OK");

                // If you have a stream, such as:
                // var file = await CrossMedia.Current.PickPhotoAsync(options);
                // var originalImageStream = file.GetStream();
                //byte[] resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(originalImageStream, 500, 1000);
                Movie.Text = ime_filma;
                image.Source = url_slika;
            }

            else
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://apistran2002.eu");
                string jsonData = @"{""kljuc"" : """ + PartyName + @"""}";
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/idSkupine.php", content);

                var result = await response.Content.ReadAsStringAsync();
                var skupine = JsonConvert.DeserializeObject<List<SkupinaId>>(result);
                skupinaID = skupine[0].id;
                //await DisplayAlert("Alert", skupinaID, "OK");
                App.Current.MainPage = new Second(skupinaID, PartyName);
            }

        }

        
        private async void GetIdSkupine()
        {
            
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://apistran2002.eu");
            string jsonData = @"{""kljuc"" : """+PartyName+@"""}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/idSkupine.php", content);

            var result = await response.Content.ReadAsStringAsync();
            var skupine = JsonConvert.DeserializeObject<List<SkupinaId>>(result);
            skupinaID = skupine[0].id;
            //await DisplayAlert("Alert", skupinaID, "OK");
            
        }
        private async void Button_Clicked_like(object sender, EventArgs e)
        {
            

            
            
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://apistran2002.eu");



                //string jsonData = @"{""skupina_id"":"""+skupinaID+@"""""id"" :"""+id+@"""}";
                string jsonData = @"{""skupina_id"" : """+skupinaID+@""",""film_id"": """+id+@"""}";
                //se party id, like = 1, disslike = 0


                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/insertliked.php", content);


            //var result = await response.Content.ReadAsStringAsync();
            //var filmi = JsonConvert.DeserializeObject<List<Filmi>>(result);
            //App.Current.MainPage = new Second();
                stfilma++;
                //b++;
                GetMovies();
          
                
            
        }

        private async void Button_Clicked_disslike(object sender, EventArgs e)
        {
            
           
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://apistran2002.eu");

                //await DisplayAlert("Alert", skupinaID, "OK");
                //await DisplayAlert("Alert", id, "OK");

                string jsonData = @"{""skupina_id"" : """+skupinaID+@""",""film_id"": """+id+@"""}";
                //se party id, like = 1, disslike = 0


                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("http://apistran2002.eu/tinder/insertdisliked.php", content);
                stfilma++;
                //b++;
                GetMovies();
                 
        }

       
        
        private void Button_Clicked_info(object sender, EventArgs e)
        {
            App.Current.MainPage = new Info(opis, ocena,url_video, ime_filma, PartyName, stfilma);   
        }
        



    }
}
