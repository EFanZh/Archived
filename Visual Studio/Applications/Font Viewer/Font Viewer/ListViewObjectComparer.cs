using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FontViewer
{
    public class ListViewObjectComparer : IComparer
    {
        private readonly List<Tuple<string, bool>> sortDescriptions = new List<Tuple<string, bool>>();

        public int Compare(object x, object y)
        {
            foreach (var sortDescription in sortDescriptions)
            {
                PropertyInfo propertyInfo = x.GetType().GetProperty(sortDescription.Item1);

                object value1 = propertyInfo.GetValue(x);
                object value2 = propertyInfo.GetValue(y);

                IComparable v1Comparable = value1 as IComparable;

                if (v1Comparable == null)
                {
                    if (value1 is FontWeight)
                    {
                        v1Comparable = ((FontWeight)value1).ToOpenTypeWeight();
                        value2 = ((FontWeight)value2).ToOpenTypeWeight();
                    }
                    else
                    {
                        v1Comparable = value1.ToString();
                        value2 = value2.ToString();
                    }
                }

                int result = v1Comparable.CompareTo(value2);

                if (result != 0)
                {
                    return sortDescription.Item2 ? result : -result;
                }
            }

            return 0;
        }

        internal void SortBy(GridViewColumn gridViewColumn)
        {
            string property = ((Binding)gridViewColumn.DisplayMemberBinding).Path.Path;

            if (sortDescriptions.Count > 0 && sortDescriptions[0].Item1 == property)
            {
                sortDescriptions[0] = Tuple.Create(sortDescriptions[0].Item1, !sortDescriptions[0].Item2);
            }
            else
            {
                sortDescriptions.Remove(sortDescriptions.FirstOrDefault(t => t.Item1 == property));
                sortDescriptions.Insert(0, Tuple.Create(property, true));
            }
        }
    }
}
