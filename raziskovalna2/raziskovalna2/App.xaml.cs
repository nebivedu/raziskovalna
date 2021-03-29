using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace raziskovalna2
{
    public partial class App : Application
    {
        //string lol;
        public App()
        {
            InitializeComponent();

            MainPage = new Party();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
