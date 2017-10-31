using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TTT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Players CurrPlayer;
        public MainWindow()
        {
            InitializeComponent();
            CurrPlayer = Players.X;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var clickedBtn = (Button)sender;
            if (clickedBtn.Content == null)
            {
                if (CurrPlayer == Players.X)
                {
                    clickedBtn.Content = "X";
                    playerO.IsChecked = true;
                    playerX.IsChecked = false;
                }
                else
                {
                    clickedBtn.Content = "O";
                    playerO.IsChecked = false;
                    playerX.IsChecked = true;
                }
                NextPlayer();
                CheckVictory();
            }
        }
        public void NextPlayer()
        {
            CurrPlayer = CurrPlayer == Players.X ? Players.O : Players.X;

        }
        public void CheckVictory()
        {
            for (int i = 0; i < 8; i++)
            {
                int a = Utils.Winners[i, 0], b = Utils.Winners[i, 1], c = Utils.Winners[i, 2];        // get the indices
                                                                                                      // of the winners

                var btns = Utils.FindVisualChildren<Button>(Root).ToArray();

                Button b1 = btns[a], b2 = btns[b], b3 = btns[c];// just to make the 
                                                                // the code readable

                if (b1.Content == null || b2.Content == null || b3.Content == null)    // any of the squares blank
                    continue;                                           // try another -- no need to go further

                if (b1.Content == b2.Content && b2.Content == b3.Content)           // are they the same?
                {                                                       // if so, they WIN!
                    WinnersGrid.Visibility = Visibility.Visible;
                    WinText.Text = b1.Content + " WINS!";
                    break;  // don't bother to continue
                }
            }
        }

        private void PlayerClick(object sender, RoutedEventArgs e)
        {
            var btn = (ToggleButton)sender;
            CurrPlayer = btn.Content.ToString() == "X" ? Players.X : Players.O;
            if (CurrPlayer == Players.X)
            {
                playerX.IsChecked = true;
                playerO.IsChecked = false;
            }
            else
            {
                playerX.IsChecked = false;
                playerO.IsChecked = true;
            }
           
        }

        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            WinnersGrid.Visibility = Visibility.Collapsed;
            var btns = Utils.FindVisualChildren<Button>(Root);
            foreach (var item in btns)
            {
                if (item.Content!=null&&item.Content.ToString() != "Replay")
                    item.Content = null;
                CurrPlayer = Players.X;
            }
        }
    }
}
