package md51836869730c090bd10fafe7a9f9a6a9e;


public class PopupActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("DropboxApp.PopupActivity, DropboxApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PopupActivity.class, __md_methods);
	}


	public PopupActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PopupActivity.class)
			mono.android.TypeManager.Activate ("DropboxApp.PopupActivity, DropboxApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
