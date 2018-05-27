
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using LearningUI.Model;

namespace LearningUI
{
	public class HomeFragment : Fragment
	{
        ListView listView;
        List<ColorItem> colorItemsList = new List<ColorItem>();
        ColorAdapter colorAdapter;
        SearchView colorSearchView;


        public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);            
        }

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.HomeLayout,container,false);

            GetColorItemsList();
            colorAdapter = new ColorAdapter(this.Activity, colorItemsList);

            listView = view.FindViewById<ListView>(Resource.Id.myListView);
            listView.Adapter = colorAdapter;

            colorSearchView = view.FindViewById<SearchView>(Resource.Id.colorSearchView);
            colorSearchView.QueryTextSubmit += ColorSearchView_QueryTextSubmit;

            return view;
		}

        private void ColorSearchView_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
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
                var colorProperty = (Color)colorInstance.GetType().GetProperty(colorName).GetValue(null);
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

