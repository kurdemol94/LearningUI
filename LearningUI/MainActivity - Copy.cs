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
        SearchView searchView;
        List<ColorItem> colorItemsList = new List<ColorItem>();
        ColorAdapter colorAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            GetColorItemsList();
            colorAdapter = new ColorAdapter(this, colorItemsList);
            listView = FindViewById<ListView>(Resource.Id.myListView);
            listView.Adapter = colorAdapter;

            searchView = FindViewById<SearchView>(Resource.Id.searchView);
            searchView.QueryTextSubmit += SearchView_QueryTextSubmit;
        }

        private void SearchView_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            GetColorItemsList(e.Query);
            colorAdapter.NotifyDataSetChanged();
        }

        private void GetColorItemsList(string query = "")
        {
            colorItemsList.Clear();
            var colorInstance = new Color();
            var colorNamesList = colorInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static)
                                      .Where(x => x.PropertyType == typeof(Color))
                                      .Select(x => x.Name);

            if (query != string.Empty)
                colorNamesList = colorNamesList.Where(x => x.ToLower().Contains(query.ToLower()));

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
        }
    }
}

