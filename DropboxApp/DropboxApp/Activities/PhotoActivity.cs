
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
using Android.Support.V7.Widget;
using Android.Media;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace DropboxApp
{
	[Activity (Label = "PhotoActivity")]			
	public class PhotoActivity : Activity
	{
		private RecyclerView _RecyclerView;
		private RecyclerView.LayoutManager _LayoutManager;
		private RecyclerView.Adapter _Adapter;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.PhotoLayout1);
			_RecyclerView = FindViewById<RecyclerView> (Resource.Id.recyclerView);

			var getRequest = new GetRequest ();
			var getResponse = new GetResponse ();
			getRequest.apiKey = "7gXV0uUd54z3ksYKMAG4co595dDPMaTB";
			getRequest.accessToken = "W88EaRR8PiKZXzoDe16eqlnt96CRSj0WFeEfy9ie9N565frGPqM4OVMKVOEdo9qGrYuT4ifeS2eNqrx3P6cBG6hTB3uTFfZawyOh";
			getRequest.langCode = "en-GB";
			String url = String.Format ("http://api.powhealth.com/LabTestService.svc/api/labtests/byuser/list/{0}/{1}/{2}", getRequest.apiKey, getRequest.accessToken, getRequest.langCode);

			WebRequest request = WebRequest.Create (url);
			WebResponse response = request.GetResponse ();

			var dataStream = response.GetResponseStream ();
			StreamReader reader = new StreamReader (dataStream);
			string responseFromServer = reader.ReadToEnd ();
			reader.Close ();
			response.Close ();
			var jsonObject = JsonConvert.DeserializeObject <GetResponse> (responseFromServer);

			//_LayoutManager = new LinearLayoutManager (this);
			_LayoutManager = new GridLayoutManager (this, 2, GridLayoutManager.Vertical, false);	
			_RecyclerView.SetLayoutManager (_LayoutManager);
			_Adapter = new RecyclerAdapter (jsonObject.LabTestsUserOnlyResult, _RecyclerView);
			_RecyclerView.SetAdapter (_Adapter);
		}

	}


}

/*_Info = new List<Info> ();
_Info.Add (new Info () { Name = "tom", Image = Resource.Drawable.ic_delete });	
_Info.Add (new Info (){ Name = "dada", Image = Resource.Drawable.Icon });
_Info.Add (new Info (){ Name = "image", Image = Resource.Drawable.ic_content_cut });
_Info.Add (new Info (){ Name = "password", Image = Resource.Drawable.ic_dropbox });
_Info.Add (new Info (){ Name = "passwo", Image = Resource.Drawable.ic_dropbox });
*/