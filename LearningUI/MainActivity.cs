using Android.App;
using Android.Widget;
using Android.OS;
using LearningUI.Model;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Android.Graphics;

namespace LearningUI
{
    [Activity(Label = "LearningUI")]
    public class MainActivity : Activity
    {
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            listView = FindViewById<ListView>(Resource.Id.myListView);
            listView.Adapter = new ColorAdapter(this, GetColorItemsList());
        }

        private List<ColorItem> GetColorItemsList()
        {
            List<ColorItem> colorItemsList = new List<ColorItem>();
            var colorInstance = new Color();
            var colorNamesList = colorInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static)
                                      .Where(x => x.PropertyType == typeof(Color))
                                      .Select(x => x.Name).ToList();
            
            foreach (var colorName in colorNamesList)
            {
                var colorProperty = (Color) colorInstance.GetType().GetProperty(colorName).GetValue(null);
                var colorItem = new ColorItem()
                {
                    Color = colorProperty,
                    ColorName = colorName,
                    Code = "R: " + colorProperty.R.ToString() + " G: " + colorProperty.G.ToString() + " B: " + colorProperty.B.ToString()
                };
                colorItemsList.Add(colorItem);
            }

            return colorItemsList;
        }
    }
}

