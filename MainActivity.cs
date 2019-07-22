using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
namespace HelloWorld
{
	[Activity (Label = "Hangman", MainLauncher = true)]
	public class MainActivity : Activity
	{
        Button ViewHighScore;
        public static EditText PlayerNameEditText;
        public static String PlayerName;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainMenu);
            
			var buttonNewGame = FindViewById<Button> (Resource.Id.Button_NewGame);
			buttonNewGame.Click += (sender, e) => {
				Intent newGame= new Intent(this,typeof(Hangman));
                PlayerNameEditText = (EditText)FindViewById(Resource.Id.player);
                PlayerName = (PlayerNameEditText.Text).ToString();

                StartActivity(newGame);
			};

            ViewHighScore = (Button)FindViewById(Resource.Id.Button_HighScores);

            ViewHighScore.Click += delegate {
                Intent newGame = new Intent(this, typeof(ProfileActivity));
                StartActivity(newGame);
            };

        }
	}
}


