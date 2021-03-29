using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace raziskovalna2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Info : ContentPage
    {
        string opis;
        string ocena;
        string url;
        string PartyName;
        int stfilma;


        public Info(string opis1, string ocena1, string url1, string naslov1, string PartyName1, int stfilm1)
        {
            InitializeComponent();

            opis = opis1;
            ocena = ocena1;
            url = url1;
            PartyName = PartyName1;
            stfilma = stfilm1;

            label_opis.Text = opis;
            label_ocena.Text = "Film rating: " + ocena + " /5☆";
            //video.Source = url;
            
        }
        private void Button_Clicked_Home1(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage(PartyName, stfilma);
        }
        



    }
}