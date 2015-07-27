//
// ViewController.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2015 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
		
using UIKit;
using System.Collections.Generic;

namespace HandCounter.iOS
{
	public partial class ViewController : UIViewController
	{
		Model model = new Model();

		public ViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif
			NumberField.Text = "1";
			Stepper.ValueChanged += delegate {
				model.Value = ((int)Stepper.Value);
			};
			NumberField.ValueChanged += (sender, e) => model.Value = int.Parse (NumberField.Text);
			CategoryChooser.ValueChanged += CategoryChooser_ValueChanged;

			ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			ImageView.UserInteractionEnabled = true;
			model.ValueChanged += (sender, e) => UpdateViewFromModel ();

			UpdateViewFromModel ();
		}

		Dictionary<string, UIImage> images = new Dictionary<string, UIImage>();

		public override void TouchesEnded (Foundation.NSSet touches, UIEvent evt)
		{
			var arr = touches.ToArray<UITouch> ();
			if (arr[0].View == ImageView) {
				model.RandomValue ();
			}

			base.TouchesEnded (touches, evt);
		}

		void CategoryChooser_ValueChanged (object sender, EventArgs e)
		{
			model.CurrentCategory = (CountCategory)
				(int)CategoryChooser.SelectedSegment;
			UpdateViewFromModel ();
		}

		void UpdateViewFromModel ()
		{
			UIImage img;
			if (!images.TryGetValue (model.CurrentImageName, out img)) {
				images [model.CurrentImageName] = img = UIImage.FromBundle ("Images/" + model.CurrentImageName);
			}
			ImageView.Image = img;
			DescriptionLabel.Text = model.Description;
			NumberField.Text = model.Value.ToString ();
			Stepper.MinimumValue = model.MinValue;
			Stepper.MaximumValue = model.MaxValue;
			Stepper.Value = model.Value;
			CategoryChooser.SelectedSegment = (int)model.CurrentCategory;
		}
	}
}
