using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CadenceLifeCounterV3.Models
{
    public abstract class HistoryEvent
    {
        public String PrimaryText { get; set; }
        public String SecondaryText { get; set; }
        public IconElement Glyph { get; set; }
        public ColorSwatchModel Color { get; set; }

        public override string ToString()
        {
            return PrimaryText; 
        }
    }

    class ResetGameEvent : HistoryEvent
    {
        public ResetGameEvent(int startingLifeValue)
        {
            PrimaryText = "The game has been reset.";
            SecondaryText = $"Starting life is {startingLifeValue}.";
            Glyph = new SymbolIcon(Symbol.Rotate);
        }
    }
    class LifeEvent : HistoryEvent
    {
        public LifeEvent(String name, ColorSwatchModel color, int oldLifeValue, int newLifeValue)
        {
            int changeInLife = Math.Abs(newLifeValue - oldLifeValue);
            PrimaryText = name + ((newLifeValue - oldLifeValue > 0)?" gained ":" lost ") + changeInLife + " life.";
            SecondaryText = $"Went from {oldLifeValue} to {newLifeValue}";

            if (newLifeValue - oldLifeValue > 0)
            {
                Glyph = new SymbolIcon(Symbol.Up);
            }
            else
            {
                Glyph = new FontIcon() { Glyph = "\uE1FD" };
            }


            Color = color;
        }
    }
    class DiceEvent : HistoryEvent
    {
        public DiceEvent(String name, int dieValue)
        {
            PrimaryText = $"{name} rolled a {dieValue}";
            //Glyph = new SymbolIcon(Symbol.Placeholder);

            var SegoeUISymbol = new Windows.UI.Xaml.Media.FontFamily("Segoe UI Symbol");
            String face = "";

            if (dieValue == 1) { face = "\u2680"; }
            else if (dieValue == 2) { face = "\u2681"; }
            else if (dieValue == 3) { face = "\u2682"; }
            else if (dieValue == 4) { face = "\u2683"; }
            else if (dieValue == 5) { face = "\u2684"; }
            else if (dieValue == 6) { face = "\u2685"; }

            Glyph = new FontIcon() { Glyph = face, FontFamily = SegoeUISymbol, FontSize = 34 };
        }
    }
    class CoinEvent : HistoryEvent
    {
        public CoinEvent()
        {
            PrimaryText = "Coin";
            Glyph = new SymbolIcon(Symbol.ReShare);

        }
    }
    class NumberPlayersChangedEvent : HistoryEvent
    {
        public NumberPlayersChangedEvent(int numberOfPlayers)
        {
            PrimaryText = $"There are now {numberOfPlayers} players.";
            PrimaryText = (numberOfPlayers > 1) ? $"There are now {numberOfPlayers} players." : $"There is now {numberOfPlayers} player.";
            Glyph = new SymbolIcon(Symbol.People);

        }
    }




    public class EventManager
    {
        public ObservableCollection<HistoryEvent> LoggedEvents { get; set; }

        public EventManager()
        {
            LoggedEvents = new ObservableCollection<HistoryEvent>();
        }

        public void LogEvent(HistoryEvent e)
        {
            LoggedEvents.Insert(0, e);

            Debug.WriteLine(e.ToString());
        }

        public void PrintEvents()
        {
            foreach (HistoryEvent e in LoggedEvents)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void ClearHistory()
        {
            LoggedEvents.Clear();
        }
    }
}
