using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void ObjectButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            object obj = button.Content;

            if (obj == null)
            {
                return;
            }

            Type objectType = obj.GetType();
            ConstructorInfo constructorInfo = TypeWindowDictionary[objectType].GetConstructor(new[] { objectType });
            Window window = (Window)constructorInfo.Invoke(new[] { obj });

            window.Show();
        }

        private void ObjectListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            object currentItem = listBox.Items.CurrentItem;

            if (currentItem == null)
            {
                return;
            }

            Type objectType = currentItem.GetType();
            ConstructorInfo constructorInfo = TypeWindowDictionary[objectType].GetConstructor(new[] { objectType });
            Window window = (Window)constructorInfo.Invoke(new[] { currentItem });

            window.Show();
        }
    }
}
