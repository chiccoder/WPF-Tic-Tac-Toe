using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TTT
{
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
                    SetPlayerOActive();
                }
                else
                {
                    clickedBtn.Content = "O";
                    SetPlayerXActive();
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
            var btns = Utils.FindVisualChildren<Button>(Root).ToArray();
            for (int i = 0; i < 8; i++)
            {
                int a = Utils.Winners[i, 0], b = Utils.Winners[i, 1], c = Utils.Winners[i, 2];
                Button b1 = btns[a], b2 = btns[b], b3 = btns[c];
                if (b1.Content == null || b2.Content == null || b3.Content == null)
                    continue;
                if (b1.Content == b2.Content && b2.Content == b3.Content)
                {
                    WinnersGrid.Visibility = Visibility.Visible;
                    WinText.Text = b1.Content + " WINS!";
                    return;
                }
            }
            if (btns.All(x => !string.IsNullOrWhiteSpace(x.Content?.ToString())))
            {
                WinnersGrid.Visibility = Visibility.Visible;
                WinText.Text = "Cat's game";
                return;
            }
        }

        private void PlayerClick(object sender, RoutedEventArgs e)
        {
            var btn = (ToggleButton)sender;
            CurrPlayer = btn.Content.ToString() == "X" ? Players.X : Players.O;
            if (CurrPlayer == Players.X)
            {
                SetPlayerXActive();
            }
            else
            {
                SetPlayerOActive();
            }
        }

        private void SetPlayerXActive()
        {
            playerX.IsChecked = true;
            playerO.IsChecked = false;
        }

        private void SetPlayerOActive()
        {
            playerX.IsChecked = false;
            playerO.IsChecked = true;
        }

        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            WinnersGrid.Visibility = Visibility.Collapsed;
            var btns = Utils.FindVisualChildren<Button>(Root);
            foreach (var item in btns)
            {
                if (item.Content != null && item.Content.ToString() != "Replay")
                    item.Content = null;
                CurrPlayer = Players.X;
            }
        }
    }
}
