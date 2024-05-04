using System;
using System.Linq;
using System.Windows;

namespace Gaming_club.Windows
{
    /// <summary>
    /// Логика взаимодействия для TouurnamentDetails.xaml
    /// </summary>
    public partial class TouurnamentDetails : Window
    {
        private User_data currentUser;
        private tournament currentTournament;

        Gaming_clubEntities db = new Gaming_clubEntities();

        public TouurnamentDetails(tournament tournament, User_data user_)
        {
            InitializeComponent();
            this.currentUser = user_;
            this.currentTournament = tournament;

            lblTitle.Content = "Название игры: " + currentTournament.title;
            lblDescription.Content = "Описание игры: " + currentTournament.description;
            lblAge.Content = "Минимальный возраст: " + currentTournament.min_age;
            lblDate.Content = "Дата начала: " + currentTournament.date_of_tournament;
           

            var userName = db.User_data.FirstOrDefault(u => u.id == currentTournament.id_userData);
            var gameTitle = db.games.FirstOrDefault(g => g.id == currentTournament.id_game);

            lblMaster.Content = "Мастер: " + userName.name;
            lblGame.Content = "Название игры в игротеке: " + gameTitle.title;
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            int age = (int)currentUser.age;
            int minAge = (int)currentTournament.min_age;
            if (age < minAge)
            {
                MessageBox.Show("ты че мелочь куда лезешь во взрослые игры?");

                players_of_tournament tournament = new players_of_tournament()
                {
                    id_tournament = currentTournament.id,
                    id_user_data = currentUser.id,
                    reg_date = DateTime.Now,
                    id_status = 2
                };

                db.players_of_tournament.Add(tournament);
                db.SaveChanges();
            }

            else if (currentUser.id_permission == 3 || currentUser.id_permission == 2)
            {
                MessageBox.Show("Возможность заблокирована для сотрудников");
            }
            else
            {
                players_of_tournament tournament = new players_of_tournament()
                {
                    id_tournament = currentTournament.id,
                    id_user_data = currentUser.id,
                    reg_date = DateTime.Now,
                    id_status = 1
                };

                db.players_of_tournament.Add(tournament);
                db.SaveChanges();

                MessageBox.Show("Успешно!");
            }
        }
    }
}
