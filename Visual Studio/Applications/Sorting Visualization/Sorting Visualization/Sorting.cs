using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualization
{
    internal static class Sorting<T> where T : IComparable<T>
    {
        public static void Inseration(Scene<T> scene)
        {
            var temp = scene.DoCreateItem();

            var data = scene.Data;

            for (int i = 1; i < data.Length; i++)
            {
                if (scene.DoCompare(data[i], data[i - 1]) < 0)
                {
                    scene.DoAssign(temp, data[i]);

                    int j = i - 1;

                    while (j >= 0 && scene.DoCompare(temp, data[j]) < 0)
                    {
                        scene.DoAssign(data[j + 1], data[j]);
                        j--;
                    }

                    scene.DoAssign(data[j + 1], temp);
                }
            }
        }
    }
}
