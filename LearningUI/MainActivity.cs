using Android.App;
using Android.Widget;
using Android.OS;
using LearningUI.Model;
using System.Collections.Generic;

namespace LearningUI
{
    [Activity(Label = "LearningUI", MainLauncher = true)]
    public class MainActivity : Activity
    {
        List<ColorItem> colorItems = new List<ColorItem>();
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            listView = FindViewById<ListView>(Resource.Id.myListView);

            List<ColorItem> colorList = new List<ColorItem>()
            {
                new ColorItem() { Color = Android.Graphics.Color.DarkRed, ColorName = "Dark Red", Code = "8B0000" },
                new ColorItem() { Color = Android.Graphics.Color.SlateBlue, ColorName = "Slate Blue", Code = "6A5ACD" },
                new ColorItem() { Color = Android.Graphics.Color.ForestGreen, ColorName = "Forest Green", Code = "228B22" }
            };

            colorItems.AddRange(colorList);

            listView.Adapter = new ColorAdapter(this, colorItems);
        }
    }
}

