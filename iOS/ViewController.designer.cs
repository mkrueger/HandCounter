// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace HandCounter.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISegmentedControl CategoryChooser { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel DescriptionLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField NumberField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIStepper Stepper { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CategoryChooser != null) {
				CategoryChooser.Dispose ();
				CategoryChooser = null;
			}
			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}
			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}
			if (NumberField != null) {
				NumberField.Dispose ();
				NumberField = null;
			}
			if (Stepper != null) {
				Stepper.Dispose ();
				Stepper = null;
			}
		}
	}
}
