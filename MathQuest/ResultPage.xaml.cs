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
    public partial class ResultPage : ContentPage
    {
        public ResultPage()
        {
            InitializeComponent();
        }
        public ResultPage(int _easy, int _inter, int _hard, int _expert, int _correct, int _incorrect, int _timedOut, string _gameTime)
        {
            InitializeComponent();

            lblCorrect.Text = _correct.ToString();
            lblIncorrect.Text = _incorrect.ToString();
            lblTimedOut.Text = _timedOut.ToString();
            lblEasy.Text = _easy.ToString();
            lblInter.Text = _inter.ToString();
            lblHard.Text = _hard.ToString();
            lblExpert.Text = _expert.ToString();
            lblGameTime.Text = _gameTime;
        }

        void OnYesClick(object sender, EventArgs args)
        {
            popupSelectLevelView.IsVisible = true;
        }

        async void OnNoClick(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new LandingPage());
        }

        private void OnButtonQuit(object sender, EventArgs args)
        {
            popupQuitView.IsVisible = true;
        }

        private void onNoSelection(object sender, EventArgs args)
        {
            popupQuitView.IsVisible = false;
            popupSelectLevelView.IsVisible = true;

        }

        private async void OnStartGame(Object sender, EventArgs args)
        {
            Button button = (Button)sender;
            string buttonName = button.Text;

            popupSelectLevelView.IsVisible = false;
            await Navigation.PushModalAsync(new MainPage(buttonName));
        }
    }
}