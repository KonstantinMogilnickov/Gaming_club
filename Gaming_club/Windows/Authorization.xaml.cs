using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Gaming_club.Windows
{
    public partial class Authorization : Window
    {
        Gaming_clubEntities db = new Gaming_clubEntities(); 

        public Authorization()
        {
            InitializeComponent();
        }
        private string password;
        private Label passwordLabel;
        private bool isPasswordVisible = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            password = txtPassword.Password;
            if (!string.IsNullOrEmpty(password))
            {
                if (passwordLabel == null)
                {
                    passwordLabel = new Label();
                    gridPassword.Children.Add(passwordLabel);
                }

                if (!isPasswordVisible)
                {
                    passwordLabel.Content = "Пароль: " + password;
                    btnShowPassword.Content = "Скрыть пароль";
                    isPasswordVisible = true;
                }
                else
                {
                    passwordLabel.Content = ""; // Скрываем пароль
                    btnShowPassword.Content = "Показать пароль";
                    isPasswordVisible = false;
                }
            }
            else
            {
                btnShowPassword.Content = "Показать пароль";
                passwordLabel.Content = ""; // Очищаем содержимое метки
                isPasswordVisible = false;
            }
        }


        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = txtLogin.Text.Trim();
                string password = txtPassword.Password.Trim();
                var user_Data = db.User_data.FirstOrDefault(u => u.login == login);

                if (user_Data != null)
                {
                    if (user_Data.password == password)
                    {
                        if (user_Data.permission == 1 || user_Data.permission == 3)
                        {
                            MainMenuWindow mainMenuWindow = new MainMenuWindow(user_Data);
                            mainMenuWindow.Show();
                            this.Close();
                        }

                        else if (user_Data.permission == 0)
                        {
                            MessageBox.Show("Вы были заблокированы за нарушение правил пользования сервисом!");
                        }

                        else
                        {
                            AdminPanel admin = new AdminPanel();
                            admin.Show();
                            this.Close();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Неправильный пароль!");
                    }
                }

                else
                {
                    MessageBox.Show("Пользователя с указанным логином не существует!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MainMenuWindow mainMenuWindow = new MainMenuWindow();
            //mainMenuWindow.Show();
            //this.Close();
        }

        private void btnAuth_MouseEnter(object sender, MouseEventArgs e)
        {
            // Создаем анимацию изменения цвета фона кнопки
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)ColorConverter.ConvertFromString("#27949a");
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3); // Длительность анимации

            // Применяем анимацию к свойству Background кнопки
            btnAuth.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем остальные свойства
            btnAuth.BorderBrush = Brushes.Transparent;
            btnAuth.BorderThickness = new Thickness(1);
            btnAuth.Cursor = Cursors.Hand;
        }

        private void btnAuth_MouseLeave(object sender, MouseEventArgs e)
        {
            // Создаем анимацию изменения цвета фона кнопки
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)ColorConverter.ConvertFromString("#34c6cd");
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3); // Длительность анимации

            // Применяем анимацию к свойству Background кнопки
            btnAuth.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем остальные свойства
            btnAuth.BorderThickness = new Thickness(0);
        }
    }
}
