using Android.App;
using Android.Widget;
using Android.OS;
using Java.Lang;
using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;

namespace Demo
{
	[Activity(Label = "Demo", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private bool Running { get; set; }
		private DateTime StartTime { get; set; }
		private TimeSpan Span = TimeSpan.Zero;
		System.Timers.Timer timer = new System.Timers.Timer(1000);
		Button button;
		TextView label;
		string PauseDisplay { get; set; }
		string StartDisplay { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			PauseDisplay = GetString(Resource.String.pause);
			StartDisplay = GetString(Resource.String.start);

			button = FindViewById<Button>(Resource.Id.myButton);
			label = FindViewById<TextView>(Resource.Id.label);

			timer.Elapsed += Timer_Elapsed; 

			button.Click += StartTimeHandler;

		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		void StartTimeHandler(object sender, EventArgs e)
		{
			if (Running)
			{
				timer.Stop();
				Running = false;
				button.Text = StartDisplay;

			}
			else {
				timer.Start();
				Running = true;
				StartTime = DateTime.Now - Span;
				button.Text = PauseDisplay;
			}
		}

		void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Span = DateTime.Now.Subtract(StartTime);
			var hours = Span.Hours;
			var minutes = Span.Minutes;
			var seconds = Span.Seconds;
			var milliseconds = Span.Milliseconds;

			RunOnUiThread(() =>
			{
				label.Text = Span.ToString(@"dd\:hh\:mm\:ss");
			});
		}
	}
}

