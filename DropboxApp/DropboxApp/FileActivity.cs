
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
	[Activity (Label = "FileActivity")]			
	public class FileActivity : Activity
	{
		
		private ImageButton moreButton;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);



			SetContentView (Resource.Layout.ListLayout);


			InfoAdapter adapter = new InfoAdapter (this, buildList ());
			ListView listview = FindViewById<ListView> (Resource.Id.listView);
			listview.Adapter = adapter;
			moreButton = FindViewById<ImageButton> (Resource.Id.moreButton);
			moreButton.Click += new EventHandler (OnOverflowClick);


		}

		public List<Info> buildList ()
		{
			return new List<Info> () {
				new Info () { Name = "Alexandru Vlad" },
				new Info () { Name = "Razvan Cristian" },
				new Info () { Name = "Cosmin Ciprian" },
				new Info () { Name = "Claudiu" },
				new Info () { Name = "Stefan" },
				new Info () { Name = "Adrian" },
				new Info () { Name = "Paul" }
			};
		}

		public void OnOverflowClick (object sender, EventArgs args)
		{
			PopupMenu popupMenu = new PopupMenu (this, this.moreButton);
			popupMenu.MenuInflater.Inflate (Resource.Menu.mainmenu, popupMenu.Menu);
			popupMenu.Show ();
		}




	}
}

