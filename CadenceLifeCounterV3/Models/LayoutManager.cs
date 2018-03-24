using CadenceLifeCounterV3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CadenceLifeCounterV3.Models
{
    public class LayoutPart
    {
        public UIElement LayoutElement { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
        public bool IsVisible { get; set; }
        public Thickness Margin { get; set; }

        //Thickness Margin { get; set; }

        public LayoutPart(UIElement layoutElement, int row, int col, int rowSpan, int colSpan, bool isVisibile, Thickness margin)
        {
            LayoutElement = layoutElement;
            Row = row;
            Column = col;
            RowSpan = rowSpan;
            ColumnSpan = colSpan;
            IsVisible = isVisibile;
            Margin = margin;
        }
    }

    class LayoutManager
    {
        List<LayoutPart> parts = new List<LayoutPart>();

        public LayoutManager() { }

        public void AddLayoutPart(LayoutPart part)
        {
            parts.Add(part);
        }

        internal void LayoutGrid()
        {
            foreach (LayoutPart part in parts)
            {
                
                FrameworkElement tile = part.LayoutElement as FrameworkElement;

                if ((tile as PlayerTile).Player == null || !(tile as PlayerTile).Player.IsActive)
                {
                    tile.Visibility = Visibility.Collapsed;
                    continue;
                }

                tile.Visibility = Visibility.Visible;                

                Grid.SetColumn(tile, part.Column);
                Grid.SetRow(tile, part.Row);
                Grid.SetColumnSpan(tile, part.ColumnSpan);
                Grid.SetRowSpan(tile, part.RowSpan);

                tile.Margin = part.Margin;
            }
        }

        public static String GetManagerKey(int numberOfPlayers, bool isLandscape)
        {
            String orientation = (isLandscape) ? "Landscape" : "Portrait";
            String count = (numberOfPlayers + 1).ToString();

            return orientation + "_" + count;
        }

        public static bool DetermineIfWindowIsLandscape()
        {
            var applicationView = ApplicationView.GetForCurrentView();
            var size = Window.Current.Bounds;

            bool isLandscape = true;

            if (applicationView.IsFullScreen)
            {
                if (applicationView.Orientation == ApplicationViewOrientation.Landscape)
                    isLandscape = true; // Fullscreen Landscape
                else
                    isLandscape = false; // Fullscreen Portrait
            }
            else
            {
                isLandscape = size.Width > size.Height;
            }

            return isLandscape;
        }

        public static LayoutManager CreateLayout(int numPlayers, bool isLandscape, UIElement layoutElement)
        {
            LayoutManager layout = new LayoutManager();

            int row, col, rowSpan = 1, colSpan = 1;

            int mod, div;
            for (int i = 0; i < MainPage.MaximumNumberOfPlayers; i++)
            {
                mod = i % 2;
                div = i / 2;
                row = (isLandscape) ? mod : div;
                col = (isLandscape) ? div : mod;

                if (numPlayers == 0 && i == 0) { rowSpan = colSpan = 2; }
                else if (numPlayers == 1)
                {
                    row = (isLandscape) ? div : mod;
                    col = (isLandscape) ? mod : div;
                    rowSpan = (isLandscape) ? 2 : 1;
                    colSpan = (isLandscape) ? 1 : 2;
                }
                else
                {
                    if (i >= numPlayers)
                    {
                        rowSpan = (isLandscape) ? 2 : 1;
                        colSpan = (isLandscape) ? 1 : 2;
                    }
                }
                layout.AddLayoutPart(new LayoutPart((layoutElement as Grid).Children.ElementAt(i), row, col, rowSpan, colSpan, (i < numPlayers + 1), new Thickness(5)));
            }
            return layout;
        }
    }
}
