﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Java.Util.ResourceBundle;
using YeatyAppMobile.Droid.CustomRenderers;
using YeatyAppMobile.CustomControls;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace YeatyAppMobile.Droid.CustomRenderers
{
    public class XEntryRenderer : EntryRenderer
    {
        public XEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }





    }
}