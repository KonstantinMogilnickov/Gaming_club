using System;
using System.Linq;
using System.Windows;

namespace Gaming_club.Windows
{
    public partial class GameDetailsWindow : Window
    {
        private Gaming_clubEntities db = new Gaming_clubEntities();
        private date_of_game_library gameInfo;
        private User_data _User;
        public GameDetailsWindow(date_of_game_library selectedGame, User_data user)
        {
            InitializeComponent();
            this._User = user;
            this.gameInfo = selectedGame;
            lblTitle.Content = "Название игры: " + selectedGame.title;
            lblDescription.Content = "Описание игры: " + selectedGame.description;
            lblAge.Content = "Минимальный возраст: " + selectedGame.min_age;
            lblDate.Content = "Дата начала: " + selectedGame.start_date;
            lblMinPlayer.Content = "Минимальное количество игроков: " + selectedGame.min_number_of_palyers;
            lblMaxPlayer.Content = "Максимальное количество игроков: " + selectedGame.max_number_of_palyers;
    
            var userName = db.User_data.FirstOrDefault(u => u.id == selectedGame.id_user_data);
            var gameTitle = db.games.FirstOrDefault(g => g.id == selectedGame.id_game);

            lblMaster.Content = "Мастер: " + userName.name;
            lblGame.Content = "Название игры в игротеке: " + gameTitle.title;
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            int age =(int)_User.age;
            int minAge = (int)gameInfo.min_age;
            if (age <minAge)
            {
                MessageBox.Show("ты че мелочь куда лезешь во взрослые игры?");
            }
            else if (_User.id_permission == 3 || _User.id_permission == 2)
            {
                MessageBox.Show("Возможность заблокирована для сотрудников");
            }

            else
            {
                players_of_game_library player_Of_Game_Library = new players_of_game_library
                {
                    id_game_library = gameInfo.id,
                    id_user_data = _User.id,
                    age = age,
                };
                db.players_of_game_library.Add(player_Of_Game_Library);
                db.SaveChanges();
                MessageBox.Show("Успешно!");
            }
        }
    }
}
