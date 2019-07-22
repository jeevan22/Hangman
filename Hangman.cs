using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Collections;
using System.IO;

namespace HelloWorld
{
	[Activity (Label = "Hangman")]			
	public class Hangman : Activity
	{
        /*
         * Declaring variables
         */

        public static String[] AllPlayersArray;
        public static String CurrentPlayerName;
        public static String Log;
        public static int PlayerHighScore;
        public TextView[] CharView;
        int i = 0, TotalWrongAnswers = 0, score;
        TextView ScoreDisplay, QuiestionText;
        ImageView[] BodyPartsArray;
        GridView Keyboard;
        ArrayAdapter KeyBoardAdapter;
        ArrayList AlphabetsArrayList;
        String CurrentGameWord;
        Button SubmitScore;
        LinearLayout CurrentLinearLayout;

        /*
         * Storing questions and answers in an array
         */

        String[] questions = {"CAPITAL OF INDIA",
            "POPULAR LANGUAGE RELEASED IN 1991",
            "LAST NAME OF AMAZON'S FOUNDER",
            "ANDROID'S PRIMARY LANGUAGE",
            "APPLE'S SIGNATURE DEVICE" };
        String[] answers = {"DELHI",
            "PYTHON",
            "BEZOS",
            "KOTLIN",
            "IPHONE" };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
                        SetContentView(Resource.Layout.GameLayout);

            Keyboard = FindViewById<GridView>(Resource.Id.gridView);
            MakeKeyBoard();
            KeyBoardAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, AlphabetsArrayList);
            Keyboard.Adapter = KeyBoardAdapter;
            Keyboard.ItemClick += gridView_ItemClick;

            CurrentLinearLayout = (LinearLayout) FindViewById(Resource.Id.currentWordLayout);
            AddAllCharView();

            QuiestionText = (TextView)FindViewById(Resource.Id.question);

            SubmitScore = (Button) FindViewById(Resource.Id.submit);

            SubmitScore.Click +=delegate {
                SaveHighScore();
                MainActivity.PlayerNameEditText.Text = "";
                Toast.MakeText(this, "YOUR SCORE IS: " + score, ToastLength.Long).Show();
                this.Finish();
            };

            QuiestionText.Text = questions[i];
            ScoreDisplay = (TextView)FindViewById(Resource.Id.score);

            /*
            * Calling the SetBodyPartsVisibility() function
            */

            SetBodyPartsVisibility();

        }

        private void MakeKeyBoard()
        {
            AlphabetsArrayList = new ArrayList();
            AlphabetsArrayList.Add("A");
            AlphabetsArrayList.Add("B");
            AlphabetsArrayList.Add("C");
            AlphabetsArrayList.Add("D");
            AlphabetsArrayList.Add("E");
            AlphabetsArrayList.Add("F");
            AlphabetsArrayList.Add("G");
            AlphabetsArrayList.Add("H");
            AlphabetsArrayList.Add("I");
            AlphabetsArrayList.Add("J");
            AlphabetsArrayList.Add("K");
            AlphabetsArrayList.Add("L");
            AlphabetsArrayList.Add("M");
            AlphabetsArrayList.Add("N");
            AlphabetsArrayList.Add("O");
            AlphabetsArrayList.Add("P");
            AlphabetsArrayList.Add("Q");
            AlphabetsArrayList.Add("R");
            AlphabetsArrayList.Add("S");
            AlphabetsArrayList.Add("T");
            AlphabetsArrayList.Add("U");
            AlphabetsArrayList.Add("V");
            AlphabetsArrayList.Add("W");
            AlphabetsArrayList.Add("X");
            AlphabetsArrayList.Add("Y");
            AlphabetsArrayList.Add("Z");

        }

        /*
        * This function saves the current player's highscore
        */

        public void SaveHighScore()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filename = System.IO.Path.Combine(path, "score.txt");


            using (var streamWriter = new StreamWriter(filename, true))
            {
                /*
                * Writing highscore
                */
                streamWriter.WriteLine(CurrentPlayerName + ": " + PlayerHighScore);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                AllPlayersArray = new string[5];
                AllPlayersArray = content.Split('\n');

            }
        }

        void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            String c = AlphabetsArrayList[e.Position].ToString();
            AlphabetsArrayList[e.Position] = " ";

            KeyBoardAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, AlphabetsArrayList);
            Keyboard.Adapter = KeyBoardAdapter;

            if (answers[i].Contains(c))
            {
                CurrentGameWord = CurrentGameWord + c;

                for(int n = 0; n < answers[i].Length; n++)
                {
                    if (CharView[n].Text == c)
                    {
                        CharView[n].SetTextColor(Android.Graphics.Color.White);
                    }
                }

                if (answers[i].Length == CurrentGameWord.Length)
                {
                    MakeKeyBoard();
                    KeyBoardAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, AlphabetsArrayList);
                    Keyboard.Adapter = KeyBoardAdapter;
                    Toast.MakeText(this, "GOOD!!", ToastLength.Short).Show();
                    Toast.MakeText(this, "ANSWER: " + answers[i], ToastLength.Long).Show();
                    i++;
                    score++;
                    ScoreDisplay.Text = MainActivity.PlayerName+"'s FINAL SCORE: " + score.ToString();
                    CurrentPlayerName = MainActivity.PlayerName;
                    PlayerHighScore = score;
                    QuiestionText.Text = questions[i];
                    CurrentGameWord = "";
                    CurrentLinearLayout.RemoveAllViews();
                    AddAllCharView();
                    TotalWrongAnswers = 0;
                    SetBodyPartsVisibility();
                }
            }
            else
            {
                /*
                 * Changing visibility of body part
                 */
                BodyPartsArray[TotalWrongAnswers].Visibility = ViewStates.Visible;
                TotalWrongAnswers++;
                if (TotalWrongAnswers == 6)
                {
                    Toast.MakeText(this, "WRONG ANSWER", ToastLength.Short).Show();
                    Toast.MakeText(this, "THE RIGHT ANSWER IS: "+answers[i], ToastLength.Long).Show();
                    Toast.MakeText(this, MainActivity.PlayerName + "'s"+" SCORE: " + score, ToastLength.Long).Show();
                    CurrentPlayerName = MainActivity.PlayerName;
                    MainActivity.PlayerNameEditText.Text = "";
                    PlayerHighScore = score;
                    SaveHighScore();
                    this.Finish();
                    i++;
                }
            }

        }

        private void SetBodyPartsVisibility()
        {
            BodyPartsArray = new ImageView[6];
            BodyPartsArray[0] = (ImageView) FindViewById(Resource.Id.head);
            BodyPartsArray[1] = (ImageView)FindViewById(Resource.Id.body);
            BodyPartsArray[2] = (ImageView)FindViewById(Resource.Id.left_arm);
            BodyPartsArray[3] = (ImageView)FindViewById(Resource.Id.right_arm);
            BodyPartsArray[4] = (ImageView)FindViewById(Resource.Id.left_leg);
            BodyPartsArray[5] = (ImageView)FindViewById(Resource.Id.right_leg);

            for (int p = 0; p < 6; p++)
            {
                BodyPartsArray[p].Visibility = ViewStates.Invisible;
            }

        }

        private void AddAllCharView()
        {
            CharView = new TextView[answers[i].Length];

            for (int k = 0; k < answers[i].Length; k++)
            {
                CharView[k] = new TextView(this);
                CharView[k].Text = "" + answers[i][k];
                CharView[k].SetTextColor(Android.Graphics.Color.Black);

                //Adding a new charview
                CurrentLinearLayout.AddView(CharView[k]);
            }
        }
    }
}

