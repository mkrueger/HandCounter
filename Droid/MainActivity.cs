using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Collections.Generic;
using Android.Content.Res;
using Java.IO;

namespace HandCounter.Droid
{
	[Activity (Label = "HandCounter.Droid", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{
		Model model = new Model();
		Dictionary<string, Bitmap> images = new Dictionary<string, Bitmap>();

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.NoTitle);

			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var spinner = FindViewById<Spinner> (Resource.Id.spinner1);
			var adapter = ArrayAdapter.CreateFromResource (BaseContext, Resource.Array.category_array, Android.Resource.Layout.SimpleSpinnerItem);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;

			var button = FindViewById<ImageButton> (Resource.Id.imageButton);
			button.Click += delegate {
				model.RandomValue ();
			};
			model.ValueChanged += Model_ValueChanged;

			FindViewById<Button> (Resource.Id.button1).Click += delegate {
				model.Value++;
			};
			FindViewById<Button> (Resource.Id.button2).Click += delegate {
				model.Value--;
			};

			UpdateViewFromModel ();

		}

		void Spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var spinner = FindViewById<Spinner> (Resource.Id.spinner1);
			model.CurrentCategory =(CountCategory)spinner.SelectedItemPosition;
			UpdateViewFromModel ();
		}

		void Model_ValueChanged (object sender, EventArgs e)
		{
			UpdateViewFromModel ();
		}

		void EditText_AfterTextChanged (object sender, Android.Text.AfterTextChangedEventArgs e)
		{
			try {
				var editText = FindViewById<EditText> (Resource.Id.editText);
				model.Value = int.Parse (editText.Text);
			} catch {
				return;
			}
			UpdateViewFromModel ();
		}

		void UpdateViewFromModel ()
		{
			Bitmap img;
			if (!images.TryGetValue (model.CurrentImageName, out img)) {
				images [model.CurrentImageName] = img = LoadBitmapFromAsset (BaseContext, "Images/" + model.CurrentImageName +".png");
			}
			var button = FindViewById<ImageButton> (Resource.Id.imageButton);
			if (img != null)
				button.SetImageBitmap (img);

			FindViewById<TextView> (Resource.Id.textViewDescription).Text = model.Description;
			var spinner = FindViewById<Spinner> (Resource.Id.spinner1);
			spinner.ItemSelected -= Spinner_ItemSelected; 
			spinner.SetSelection ((int)model.CurrentCategory);
			spinner.ItemSelected += Spinner_ItemSelected; 

			//Stepper.MinimumValue = model.MinValue;
			//Stepper.MaximumValue = model.MaxValue;
			//Stepper.Value = model.Value;
			var editText = FindViewById<EditText> (Resource.Id.editText);
			editText.AfterTextChanged -= EditText_AfterTextChanged; 
			editText.Text = model.Value.ToString ();
			editText.AfterTextChanged += EditText_AfterTextChanged; 
		}

		public static Bitmap LoadBitmapFromAsset(Context context, string filePath) 
		{
			try {
				using (var stream = context.Assets.Open(filePath))
					return BitmapFactory.DecodeStream(stream);
			} catch (Exception) {
				return null;
			}
		}

		int ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int) ((pixelValue)/Resources.DisplayMetrics.Density);
			return dp;
		}
	}
}


