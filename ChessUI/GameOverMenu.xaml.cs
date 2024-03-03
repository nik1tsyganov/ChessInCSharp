using ClassLogic;
using System.Windows;
using System.Windows.Controls;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;

        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();
            Result result = gameState.Result;
            if (result != null)
            {
                WinnerText.Text = GetWinnerText(result.Winner);
                ReasonText.Text = GetReasonText(result.Reason, result.Winner);
            }
        }

        private static string GetWinnerText(Player winner)
        {
            return winner switch
            {
                Player.White => "WHITE WINS!",
                Player.Black => "BLACK WINS!",
                _ => "IT'S A DRAW"
            };
        }


        private static string PlayerString(Player player)
        {
            return player switch
            {
                Player.White => "WHITE",
                Player.Black => "BLACK",
                _ => string.Empty
            };
        }

        private static string GetReasonText(EndReason reason, Player player)
        {
            return reason switch
            {
                EndReason.Checkmate => $"CHECKMATE - {PlayerString(player)} CAN'T MOVE",
                EndReason.Stalemate => $"STALEMATE - {PlayerString(player)} CAN'T MOVE",
                EndReason.FiftyMoveRule => "FIFTY-MOVE RULE",
                EndReason.InsufficientMaterial => "INSUFFICIENT MATERIAL",
                EndReason.ThreefoldRepetition => "THREEFOLD REPETITION",
                _ => string.Empty
            };
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }
    }
}
