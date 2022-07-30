using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MathQuest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        public void OnNewGameClicked(object sender, EventArgs args)
        {
            popupSelectLevelView.IsVisible = true;
        }

        private async void OnCancelClicked(object sender, EventArgs args)
        {
            popupSelectLevelView.IsVisible = false;
            await Navigation.PushModalAsync(new LandingPage());
        }


        private async void OnStartGame(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            string buttonName = button.Text;
            await Navigation.PushModalAsync(new MainPage(buttonName));
        }
    }
}