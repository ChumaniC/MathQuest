using System;
using System.Timers;
using Xamarin.Forms;

namespace MathQuest
{
    public partial class MainPage : ContentPage
    {
        private int userAnswer;

        MathQuest mathQuest = new MathQuest();

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(string selectedButton)
        {
            InitializeComponent();

            // Initialize properties

            mathQuest.Chances = 0;
            mathQuest.Easy = 0;
            mathQuest.Inter = 0;
            mathQuest.Hard = 0;
            mathQuest.Expert = 0;
            mathQuest.Correct = 0;
            mathQuest.Incorrect = 0;
            mathQuest.TimedOut = 0;

            // Initialize UI elements
            // Labels

            mathQuest.LevelHeadingLabel = lblLevelHeading;
            mathQuest.ChancesLabel = lblChances;
            mathQuest.ErrorLabel = lblError;
            mathQuest.TimerLabel = lblTimer;
            mathQuest.TotalTimeLabel = lblTotalTime;
            mathQuest.ResultLabel = lblResult;
            mathQuest.LV_PopupTitleLabel = LV_lblPopUpTitle;
            mathQuest.LV_PopupBodyLabel = LV_lblPopUpBody;

            // Content Views

            mathQuest.PopupLoginView = popupLoginView;
            mathQuest.PopupSelectLevelView = popupSelectLevelView;
            mathQuest.PopupQuitView = popupQuitView;

            // Initialize current game timer

            mathQuest.Timer = new Timer();
            mathQuest.Timer.Interval = 10;
            mathQuest.Timer.Elapsed += mathQuest.OnTimedEvent;

            // Initialize total game timer

            mathQuest.TotalTime = new Timer();
            mathQuest.TotalTime.Interval = 1;
            mathQuest.TotalTime.Elapsed += mathQuest.TotalTimeElapsed;

            // Initiate the total game timer

            mathQuest.TotalStartTime = DateTime.UtcNow;
            mathQuest.TotalTime.Start();

            // Initialize method to start game 

            mathQuest.SelectedDifficulty(selectedButton);
        }

        private async void OnCancel(object sender, EventArgs args)
        {
            mathQuest.PopupQuitView.IsVisible = false;
            await Navigation.PushModalAsync(new LandingPage());
        }

        private void OnSubmit(object sender, EventArgs args)
        {
            if (Int32.TryParse(txtUserAns.Text, out userAnswer))
            {
                if (mathQuest.MultipliedValue == userAnswer)
                {
                    mathQuest.Timer.Stop();
                    mathQuest.Correct++;
                    mathQuest.ErrorLabel.Text = "";
                    mathQuest.PopupLoginView.IsVisible = true;
                    mathQuest.LV_PopupTitleLabel.Text = "Congratulations!";
                    mathQuest.LV_PopupTitleLabel.TextColor = Color.LightGreen;
                    mathQuest.LV_PopupTitleLabel.FontAttributes = FontAttributes.Bold;
                    mathQuest.LV_PopupBodyLabel.Text = "You have correctly guessed the answer of " + mathQuest.MultipliedValue;
                    mathQuest.LV_PopupBodyLabel.TextColor = Color.Black;
                }
                else
                {
                    mathQuest.Timer.Stop();
                    mathQuest.Incorrect++;
                    mathQuest.ErrorLabel.Text = "";
                    mathQuest.PopupLoginView.IsVisible = true;
                    mathQuest.LV_PopupTitleLabel.Text = "Sorry!";
                    mathQuest.LV_PopupTitleLabel.TextColor = Color.Red;
                    mathQuest.LV_PopupTitleLabel.FontAttributes = FontAttributes.Bold;
                    mathQuest.LV_PopupBodyLabel.Text = "You have incorrectly guessed the answer." + "\n" + "\n" +
                        "The correct answer is " + mathQuest.MultipliedValue;
                    mathQuest.LV_PopupBodyLabel.TextColor = Color.Black;
                }
            }
            else
            {
                mathQuest.ErrorLabel.Text = "The value provided is invalid. Enter a number";
            }
        }

        private async void OnButtonContinue(object sender, EventArgs args)
        {
            if (mathQuest.Chances < 10)
            {
                mathQuest.PopupLoginView.IsVisible = false;
                mathQuest.PopupSelectLevelView.IsVisible = true;
                lblPopUpChances.Text = "Chances remaining: " + (10 - mathQuest.Chances).ToString();
            }
            else
            {
                mathQuest.TotalTime.Stop();
                if (mathQuest.Correct + mathQuest.Incorrect == 10)
                {
                    mathQuest.TimedOut = 0;
                    await Navigation.PushModalAsync(new ResultPage(mathQuest.Easy, mathQuest.Inter, mathQuest.Hard, mathQuest.Expert, mathQuest.Correct, mathQuest.Incorrect, mathQuest.TimedOut, mathQuest.GameTime));
                }
                else if (mathQuest.Correct + mathQuest.Incorrect < 10)
                {
                    mathQuest.TimedOut = Math.Abs(mathQuest.Correct - mathQuest.Incorrect);
                    await Navigation.PushModalAsync(new ResultPage(mathQuest.Easy, mathQuest.Inter, mathQuest.Hard, mathQuest.Expert, mathQuest.Correct, mathQuest.Incorrect, mathQuest.TimedOut, mathQuest.GameTime));
                }

            }
        }

        private void OnButtonQuit(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            mathQuest.CancelButtonSelected = button;
            mathQuest.PopupLoginView.IsVisible = false;
            mathQuest.PopupQuitView.IsVisible = true;
        }

        private void onNoSelection(object sender, EventArgs args)
        {
            if (mathQuest.CancelButtonSelected == btn_Cancel)
            {
                mathQuest.PopupQuitView.IsVisible = false;
                mathQuest.PopupLoginView.IsVisible = true;
            }
            else if (mathQuest.CancelButtonSelected == btnQuit)
            {
                mathQuest.PopupQuitView.IsVisible = false;
            }
        }

        private void OnStartGame(Object sender, EventArgs args)
        {
            Button button = (Button)sender;
            string buttonName = button.Text;

            mathQuest.SelectedDifficulty(buttonName);
            mathQuest.PopupSelectLevelView.IsVisible = false;
        }
    }
}
