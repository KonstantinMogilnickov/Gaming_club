using Gaming_club.classes;
using Microsoft.Win32;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Gaming_club.Windows
{
    public partial class AdminPanel : Window
    {
        Gaming_clubEntities db = new Gaming_clubEntities();
        private string sourceImage;
        public AdminPanel()
        {
            InitializeComponent();

            FillComboBox();
            LoadUser();
        }

        private void editImageBtn_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    string imagePath = openFileDialog.FileName;

                    // Закрываем ресурсы текущего изображения, если они есть
                    if (profileImage.Source is BitmapImage bitmapImage)
                    {
                        bitmapImage.StreamSource?.Close(); // Закрываем поток, если он был открыт
                        bitmapImage = null; // Очищаем ссылку на объект BitmapImage
                    }
                    // Загружаем новое изображение
                    BitmapImage newBitmapImage = new BitmapImage(new Uri(imagePath));
                    profileImage.Source = newBitmapImage;
                    sourceImage = ImageHelper.SaveImage(imagePath, profileImage.Source, AppDomain.CurrentDomain.BaseDirectory, "GamesImage", $"game_{randomNumber.ToString()}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillComboBox()
        {
            FillComboboxGenre();
            FillComboBoxCategory();
            FillComboBoxThematics();
            FillComboBoxGameExclusive();
            FillComboBoxGame();
            FillComboBoxResponsible();
            FillComboBoxTourGame();
            FillComboBoxTourMaster();
        }

        private void FillComboBoxTourMaster()
        {
            User_data user_Data = new User_data();
            var masters = db.User_data.Where(user => user.id_permission == 3).ToList();
            cmbTourMaster.ItemsSource = masters;
            cmbTourMaster.DisplayMemberPath = "name";
        }

        private void FillComboBoxTourGame()
        {
            var games = db.games.ToList();
            cmbTourGame.ItemsSource = games;
            cmbTourGame.DisplayMemberPath = "title";
        }

        private void FillComboboxGenre()
        {
            var genres = db.game_genre.ToList();
            cmbGenre.ItemsSource = genres;
            cmbGenre.DisplayMemberPath = "genre";
        }

        private void FillComboBoxCategory()
        {
            var category = db.game_category.ToList();
            cmbGameCategory.ItemsSource = category;
            cmbGameCategory.DisplayMemberPath = "category";
        }

        private void FillComboBoxThematics()
        {
            var thematicks = db.game_thematic.ToList();
            cmbGameThematick.ItemsSource = thematicks;
            cmbGameThematick.DisplayMemberPath = "thematic";
        }

        private void FillComboBoxGameExclusive()
        {
            var exclusive_Games = db.exclusive_game.ToList();
            cmbExclusiveGame.ItemsSource = exclusive_Games;
            cmbExclusiveGame.DisplayMemberPath = "exclusive";
        }

        private void FillComboBoxGame()
        {
            var games = db.games.ToList();
            cmbGame.ItemsSource = games;
            cmbGame.DisplayMemberPath = "title";
        }

        private void FillComboBoxResponsible()
        {
            User_data user_Data = new User_data();
            var masters = db.User_data.Where(user => user.id_permission == 3).ToList();
            cmbAddMaster.ItemsSource = masters;
        }

        private void btnAddGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                game game = new game()
                {
                    title = txtTitle.Text.Trim(),
                    description = txtDescr.Text.Trim(),
                    id_genre = ((game_genre)cmbGenre.SelectedItem).id,
                    id_thematics = ((game_thematic)cmbGameThematick.SelectedItem).id,
                    age_limit = Convert.ToInt32(txtAgeLimit.Text.Trim()),
                    id_exclusive = ((exclusive_game)cmbExclusiveGame.SelectedItem).id,
                    id_category = ((game_category)cmbGameCategory.SelectedItem).id,
                    price = txtPrice.Text.Trim(),
                    image = sourceImage,
                };

                db.games.Add(game);
                db.SaveChanges();
                MessageBox.Show("Игра добавлена!");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Свойство: {validationError.PropertyName} Ошибка: {validationError.ErrorMessage}");
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cmbExclusiveGame.SelectedItem != null && cmbExclusiveGame.SelectedItem.ToString() == "Нет")
            {
                txtPrice.Visibility = Visibility.Hidden;
            }
            else
            {
                txtPrice.Visibility = Visibility.Visible;
            }
        }

       private void LoadUser()
        {
            var userData = db.User_data.ToList();
            listBoxUser.ItemsSource = userData;
        }

        private void btnAllow_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUser.SelectedItem != null)
            {
                User_data selectedUser = (User_data)listBoxUser.SelectedItem;
                if(selectedUser.id_permission == 2)
                {
                    MessageBox.Show("Вы не можете это сделать!");
                }
                else
                {
                    selectedUser.id_permission = 1;//Разблокировка пользователя
                    db.SaveChanges();
                    MessageBox.Show("Пользователь разблокирован.");
                    LoadUser(); // обновление списка пользователей
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для разрешения доступа к сервису.");
            }
        }

        private void btnProhibit_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxUser.SelectedItem != null)
            {
                User_data selectedUser = (User_data)listBoxUser.SelectedItem;
                if (selectedUser.id_permission == 2)
                {
                    MessageBox.Show("Вы не можете заблокировать администратора!");
                }

                else
                {
                    selectedUser.id_permission = 0; //Блокировка пользователя
                    db.SaveChanges();
                    MessageBox.Show("Пользователь заблокирован.");
                    LoadUser(); // обновление списка пользователей
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для блокировки.");
            }
        }

        private void btnBackAuth_Click(object sender, RoutedEventArgs e)
        {
           MainWindow mainWindow = new MainWindow();
           mainWindow.Show();
            this.Close();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if(listBoxUser.SelectedItems != null)
            {
                User_data selectedUser = (User_data)listBoxUser.SelectedItem;
                if(selectedUser.id_permission == 2)
                {
                    MessageBox.Show("Вы не можете удалить администратора!");
                }
                else
                {
                    db.User_data.Remove(selectedUser);
                    MessageBox.Show("Пользователь удален");
                    db.SaveChanges();
                    LoadUser();
                }     
            }


            else
            {
                MessageBox.Show("Выберите пользователя для удаления");
            }
        }

        private void btnAddMaster_Click(object sender, RoutedEventArgs e)
        {
            if(listBoxUser.SelectedItems != null)
            {
                User_data selectedUser = (User_data)listBoxUser.SelectedItem;
                selectedUser.id_permission = 3;
                db.SaveChanges();
                LoadUser();
                MessageBox.Show("Мастер добавлен!");
            }

            else
            {
                MessageBox.Show("Выберите пользователя, чтобы назначить его мастером");
            }
        }

        private void btnAddGameLibrary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                date_of_game_library gameLibrary = new date_of_game_library()
                {
                    id_game = ((game)cmbGame.SelectedItem).id,
                    start_date = dpDateGameLibrary.DisplayDate,
                    min_number_of_palyers = Convert.ToInt32(txtMinPlayers.Text.Trim()),
                    max_number_of_palyers = Convert.ToInt32(txtMaxPlayers.Text.Trim()),
                    id_user_data = ((User_data)cmbAddMaster.SelectedItem).id,
                    min_age = Convert.ToInt32(txtMinAge.Text.Trim()),
                    title = txtGameLibraryTitle.Text.Trim(),
                    description = txtGameLibraryDescription.Text.Trim(),
                };
                db.date_of_game_library.Add(gameLibrary);
                db.SaveChanges();
                MessageBox.Show("Игротека добавлена!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddTournament_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tournament tournament = new tournament()
                {
                    id_game = ((game)cmbTourGame.SelectedItem).id,
                    id_userData = ((User_data)cmbTourMaster.SelectedItem).id,
                    date_of_tournament = dpTourDate.DisplayDate,
                    min_age = Convert.ToInt32(txtTourMinAge.Text.Trim()),
                    price = Convert.ToInt32(txtTourPrice.Text.Trim()),
                    title = txtTourTitle.Text.Trim(),
                    description = txtTourDescr.Text.Trim(),
                };

                db.tournaments.Add(tournament);
                db.SaveChanges();
                MessageBox.Show("Данные о турнире успешно добавлены!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Information);

            // Проверяем, что пользователь выбрал "да"
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}