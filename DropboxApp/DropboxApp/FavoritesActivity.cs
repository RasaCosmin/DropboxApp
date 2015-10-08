
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DropboxApp
{
	[Activity (Label = "FavoritesActivity")]			
	public class FavoritesActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			TextView textview = new TextView (this);
			textview.Text = "This is the My Favorites tab";
			SetContentView (textview);
		}
	}
}

