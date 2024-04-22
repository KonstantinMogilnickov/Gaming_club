using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gaming_club.Windows
{
    /// <summary>
    /// Логика взаимодействия для GameDetailsWindow.xaml
    /// </summary>
    public partial class GameDetailsWindow : Window
    {
        private date_of_game_library gameInfo;
        public GameDetailsWindow(date_of_game_library selectedGame)
        {
            InitializeComponent();
            gameInfo = selectedGame;
            lblTitle.Content = selectedGame.title;
        }
    }
}
