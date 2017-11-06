using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace TTT
{
    public enum Players
    {
        X,
        O
    }
    public static class Utils
    {
        // Winners contains all the array locations of
        // the winning combination -- if they are all 
        // either X or O (and not blank)
        public static int[,] Winners = new int[,]
        {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
        };
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) // Pulls all of the Grids in the menu and puts them in an IEnumerable collection
            where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
