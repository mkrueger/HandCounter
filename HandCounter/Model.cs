//
// Model.cs
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

namespace HandCounter
{
	public enum CountCategory
	{
		CountToFive,
		Chinese10,
		Binary31
	}

	public class Model
	{
		CountCategory currentCategory;
		public CountCategory CurrentCategory {
			get {
				return currentCategory;
			}
			set {
				currentCategory = value;
				ClampValue ();

			}
		}

		public string Description {
			get {
				switch (CurrentCategory) {
				case CountCategory.CountToFive:
					return Resources.Descriptions [0];
				case CountCategory.Chinese10:
					return Resources.Descriptions [1];
				case CountCategory.Binary31:
					return Resources.Descriptions [2];
				default:
					throw new ArgumentOutOfRangeException ();
				}
			}
		}

		public int MinValue {
			get {
				return 1;
			}
		}

		public int MaxValue {
			get {
				switch (CurrentCategory) {
				case CountCategory.CountToFive:
					return 5;
				case CountCategory.Chinese10:
					return 10;
				case CountCategory.Binary31:
					return 31;
				default:
					throw new ArgumentOutOfRangeException ();
				}
			}
		}

		int value;
		public int Value {
			get {
				return value;
			}
			set {
				if (this.value == value)
					return;
				this.value = value;
				ClampValue ();
				OnValueChanged (EventArgs.Empty);
			}
		}

		void ClampValue()
		{
			value = Math.Min (MaxValue,  Math.Max (MinValue, value));
		}

		public string CurrentImageName {
			get {
				
				return (CurrentCategory == CountCategory.Binary31 ? "b_" : "c_") + Value;
			}
		}

		public Model ()
		{
			CurrentCategory = CountCategory.Binary31;
			Value = MinValue;
		}


		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged (EventArgs e)
		{
			var handler = ValueChanged;
			if (handler != null)
				handler (this, e);
		}
	}
}

