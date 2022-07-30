using System;
using Xamarin.Forms;

namespace MathQuest
{
    class MathQuest
    {
        public MathQuest()
        {
        }

        // Methods for MathQuest

        // Auto-properties

        public System.Timers.Timer Timer { get; set; }

        public System.Timers.Timer TotalTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime TotalStartTime { get; set; }

        public double ShotClock { get; set; }
        public int Chances { get; set; }

        public int Easy { get; set; }

        public int Inter { get; set; }

        public int Hard { get; set; }

        public int Expert { get; set; }

        public int Correct { get; set; }

        public int Incorrect { get; set; }

        public int TimedOut { get; set; }

        public int MultipliedValue { get; set; }

        public string GameTime { get; set; }

        public Button CancelButtonSelected { get; set; } = new Button();

        public ContentView PopupLoginView { get; set; } = new ContentView();

        public ContentView PopupSelectLevelView { get; set; } = new ContentView();

        public ContentView PopupQuitView { get; set; } = new ContentView();

        public Label LevelHeadingLabel { get; set; } = new Label();

        public Label ChancesLabel { get; set; } = new Label();

        public Label ErrorLabel { get; set; } = new Label();
        public Label ResultLabel { get; set; } = new Label();

        public Label TimerLabel { get; set; } = new Label();

        public Label LV_PopupTitleLabel { get; set; } = new Label();

        public Label LV_PopupBodyLabel { get; set; } = new Label();

        public Label TotalTimeLabel { get; set; } = new Label();

        // Method to select the difficulty level
        public void SelectedDifficulty(string _level)
        {
            Random numberOne = new Random();
            Random numberTwo = new Random();

            if (_level == "Easy")
            {
                Chances++;
                Easy++;
                ShotClock = 10;
                // Initiate the countdown timer
                StartTime = DateTime.UtcNow;
                Timer.Start();
                int firstNum = numberOne.Next(0, 5);
                int secNum = numberTwo.Next(0, 5);
                MultipliedValue = firstNum * secNum;
                LevelHeadingLabel.Text = "Current level: Easy";
                LevelHeadingLabel.TextColor = Color.LightGreen;
                LevelHeadingLabel.FontAttributes = FontAttributes.Italic;
                LevelHeadingLabel.FontAttributes = FontAttributes.Bold;
                ChancesLabel.Text = "Attempts remaining: " + (10 - Chances).ToString();
                ResultLabel.Text = "What is " + firstNum + " multiplied by " + secNum + "?";
                ResultLabel.FontAttributes = FontAttributes.Bold;
                ResultLabel.TextColor = Color.Blue;
            }

            if (_level == "Intermediate")
            {
                Chances++;
                Inter++;
                ShotClock = 15;
                // Initiate the countdown timer
                StartTime = DateTime.UtcNow;
                Timer.Start();
                int firstNum = numberOne.Next(0, 12);
                int secNum = numberTwo.Next(0, 12);
                MultipliedValue = firstNum * secNum;
                LevelHeadingLabel.Text = "Current level: Intermediate";
                LevelHeadingLabel.TextColor = Color.Orange;
                LevelHeadingLabel.FontAttributes = FontAttributes.Italic;
                LevelHeadingLabel.FontAttributes = FontAttributes.Bold;
                ChancesLabel.Text = "Chances remaining: " + (10 - Chances).ToString();
                ResultLabel.Text = "What is " + firstNum + " multiplied by " + secNum + "?";
                ResultLabel.FontAttributes = FontAttributes.Bold;
                ResultLabel.TextColor = Color.Blue;
            }

            if (_level == "Hard")
            {
                Chances++;
                Hard++;
                ShotClock = 25;
                // Initiate the countdown timer
                StartTime = DateTime.UtcNow;
                Timer.Start();
                int firstNum = numberOne.Next(0, 99);
                int secNum = numberTwo.Next(0, 99);
                MultipliedValue = firstNum * secNum;
                LevelHeadingLabel.Text = "Current level: Hard";
                LevelHeadingLabel.TextColor = Color.IndianRed;
                LevelHeadingLabel.FontAttributes = FontAttributes.Italic;
                LevelHeadingLabel.FontAttributes = FontAttributes.Bold;
                ChancesLabel.Text = "Chances remaining: " + (10 - Chances).ToString();
                ResultLabel.Text = "What is " + firstNum + " multiplied by " + secNum + "?";
                ResultLabel.FontAttributes = FontAttributes.Bold;
                ResultLabel.TextColor = Color.Blue;
            }

            if (_level == "Expert")
            {
                Chances++;
                Expert++;
                ShotClock = 30;
                // Initiate the countdown timer
                StartTime = DateTime.UtcNow;
                Timer.Start();
                int firstNum = numberOne.Next(0, 199);
                int secNum = numberTwo.Next(0, 199);
                MultipliedValue = firstNum * secNum;
                LevelHeadingLabel.Text = "Current level: Expert";
                LevelHeadingLabel.TextColor = Color.DarkMagenta;
                LevelHeadingLabel.FontAttributes = FontAttributes.Italic;
                LevelHeadingLabel.FontAttributes = FontAttributes.Bold;
                ChancesLabel.Text = "Chances remaining: " + (10 - Chances).ToString();
                ResultLabel.Text = "What is " + firstNum + " multiplied by " + secNum + "?";
                ResultLabel.FontAttributes = FontAttributes.Bold;
                ResultLabel.TextColor = Color.Blue;
            }
        }

        public void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var elapsedSinceBtnStartPressed = (DateTime.UtcNow - StartTime).TotalSeconds;
                var remaining = ShotClock - elapsedSinceBtnStartPressed;

                if (remaining > 5.00)
                {
                    TimerLabel.TextColor = Color.Green;
                    TimerLabel.Text = Math.Round(remaining, 2).ToString() + " seconds remaining...";
                }
                else if (remaining <= 5.00 && remaining > 0.00)
                {
                    TimerLabel.Text = "Hurry! " + Math.Round(remaining, 2).ToString() + " seconds remaining!";
                    TimerLabel.TextColor = Color.Red;
                }
                else
                {
                    TimerLabel.Text = "0:00";
                    Timer.Stop();
                    ErrorLabel.Text = "";
                    PopupLoginView.IsVisible = true;
                    LV_PopupTitleLabel.Text = "Oops! Time's Up!";
                    LV_PopupTitleLabel.TextColor = Color.Red;
                    LV_PopupTitleLabel.FontAttributes = FontAttributes.Bold;
                    LV_PopupBodyLabel.Text = "Your time has run out." + "\n" + "\n" +
                        "The correct answer is " + MultipliedValue;
                    LV_PopupBodyLabel.TextColor = Color.Black;
                }
            });
        }

        public void TotalTimeElapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var totalTimeElapsed = (DateTime.UtcNow - TotalStartTime).TotalSeconds;
                GameTime = Math.Round(totalTimeElapsed, 2).ToString();
                TotalTimeLabel.Text = "Total game time: " + Math.Round(totalTimeElapsed, 2).ToString();
            });
        }
    }
}

