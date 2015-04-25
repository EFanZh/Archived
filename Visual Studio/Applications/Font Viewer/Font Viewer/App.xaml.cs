using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FontViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Dictionary<Type, Type> TypeWindowDictionary = new Dictionary<Type, Type>()
        {
            { typeof(FontFamily), typeof(FontFamilyWindow) },
            { typeof(FamilyTypeface), typeof(FamilyTypefaceWindow) },
            { typeof(Typeface), typeof(TypefaceWindow) },
        };

        private static ConditionalWeakTable<ListView, ListViewObjectComparer> listViewComparers = new ConditionalWeakTable<ListView, ListViewObjectComparer>();

        private void ObjectButton_Click(object sender, RoutedEventArgs e)
        {
            ShowObjectWinow(((Button)sender).Content);
        }

        private void ObjectListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowObjectWinow(((ListBoxItem)sender).Content);
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = (GridViewColumnHeader)sender;
            ListCollectionView view = GetListCollectionViewFromGridViewColumnHeader(header);

            using (view.DeferRefresh())
            {
                ListViewObjectComparer comparer = (ListViewObjectComparer)view.CustomSort ?? new ListViewObjectComparer();

                comparer.SortBy(header.Column);

                view.CustomSort = comparer;
            }
        }

        private static void ShowObjectWinow(object obj)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() =>
            {
                Type objectType = obj.GetType();
                ConstructorInfo constructorInfo = TypeWindowDictionary[objectType].GetConstructor(new[] { objectType });
                Window window = (Window)constructorInfo.Invoke(new[] { obj });

                window.Show();
            }));
        }

        private static ListCollectionView GetListCollectionViewFromGridViewColumnHeader(GridViewColumnHeader header)
        {
            DependencyObject p = VisualTreeHelper.GetParent(header);

            while (!(p is ListView))
            {
                p = VisualTreeHelper.GetParent(p);
            }

            return (ListCollectionView)CollectionViewSource.GetDefaultView(((ListView)p).ItemsSource);
        }
    }
}
