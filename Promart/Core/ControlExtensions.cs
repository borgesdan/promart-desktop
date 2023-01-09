using System;
using System.Windows.Media;

namespace Promart.Core
{
    public static class ControlExtensions
    {
        static public void ForEachVisual(this Visual control, Action<Visual> action)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(control, i);
                
                action(childVisual);

                ForEachVisual(childVisual, action);
            }
        }
    }
}