using Gaming_club.classes;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Gaming_club.Windows
{

    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        // Метод вызывается при изменении текста в текстовом поле для номера телефона
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Если в текстовом поле только один символ и это не "+", заменяем его на "+"
            if (txtPhoneNumber.Text.Length == 1 && txtPhoneNumber.Text != "+")
            {
                txtPhoneNumber.Text = "+";
                // Перемещаем курсор в конец текста
                txtPhoneNumber.SelectionStart = txtPhoneNumber.Text.Length;
            }
        }

        // Метод вызывается перед вводом текста в текстовое поле для номера телефона
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            // Сохраняем текущее выделение текста
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;

            // Вставляем вводимый символ в текущий текст и форматируем номер телефона
            string newText = textBox.Text.Substring(0, selectionStart) + e.Text + textBox.Text.Substring(selectionStart + selectionLength);
            string formattedText = FormatPhoneNumber(newText);

            // Если удалось сформатировать текст, устанавливаем его в текстовое поле
            if (formattedText != null)
            {
                textBox.Text = formattedText;

                // Перемещаем курсор в конец текста
                textBox.CaretIndex = textBox.Text.Length;
            }

            // Помечаем событие как обработанное, чтобы предотвратить ввод символов в текстовое поле
            e.Handled = true;
        }

        // Метод форматирует номер телефона
        private string FormatPhoneNumber(string phoneNumber)
        {
            // Удаление всех символов, кроме цифр
            phoneNumber = Regex.Replace(phoneNumber, @"\D", "");

            // Форматирование номера телефона
            if (phoneNumber.Length == 0)
                return "";
            else if (phoneNumber.Length <= 1)
                return "+" + phoneNumber;
            else if (phoneNumber.Length <= 4)
                return "+" + phoneNumber.Substring(0, 1) + " (" + phoneNumber.Substring(1) + ")";
            else if (phoneNumber.Length <= 7)
                return "+" + phoneNumber.Substring(0, 1) + " (" + phoneNumber.Substring(1, 3) + ") " + phoneNumber.Substring(4);
            else if (phoneNumber.Length <= 11)
                return "+" + phoneNumber.Substring(0, 1) + " (" + phoneNumber.Substring(1, 3) + ") " + phoneNumber.Substring(4, 3) + "-" + phoneNumber.Substring(7);
            else
                return "+" + phoneNumber.Substring(0, 1) + " (" + phoneNumber.Substring(1, 3) + ") " + phoneNumber.Substring(4, 3) + "-" + phoneNumber.Substring(7, 4);
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

        private void btnReg_MouseEnter(object sender, MouseEventArgs e)
        {
            // Создаем анимацию изменения цвета фона кнопки
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)ColorConverter.ConvertFromString("#27949a");
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3); // Длительность анимации

            // Применяем анимацию к свойству Background кнопки
            btnReg.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем остальные свойства
            btnReg.BorderBrush = Brushes.Transparent;
            btnReg.BorderThickness = new Thickness(1);
            btnReg.Cursor = Cursors.Hand;
        }

        private void btnReg_MouseLeave(object sender, MouseEventArgs e)
        {
            // Создаем анимацию изменения цвета фона кнопки
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)ColorConverter.ConvertFromString("#34c6cd");
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3); // Длительность анимации

            // Применяем анимацию к свойству Background кнопки
            btnReg.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем остальные свойства
            btnReg.BorderThickness = new Thickness(0);
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            //логика регистрации тут

            RegistrationUser registrationUser = new RegistrationUser(new Gaming_clubEntities());
            string name = txtName.Text.Trim();
            string surname = txtSurname.Text.Trim();
            string patrynumic = txtPatrynumic.Text.Trim();
            string phoneNumber = txtPhoneNumber.Text.Trim();
            int permission = 1;
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();
            int age = Convert.ToInt32(txtAge.Text.Trim());
            string image = null;

            registrationUser.RegisterUser(name, surname, patrynumic, phoneNumber, permission, login, password, age, image, confirmPassword,this);
        }

        //возврат на главную форму 
        private void btnBack_click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();//закрываем эту форму, т.к даллее она не используется 
        }

        private void btnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            // Создаем анимацию изменения цвета фона кнопки
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)ColorConverter.ConvertFromString("#27949a");
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3); // Длительность анимации

            // Применяем анимацию к свойству Background кнопки
            btnBack.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем остальные свойства
            btnBack.BorderBrush = Brushes.Transparent;
            btnBack.BorderThickness = new Thickness(1);
            btnBack.Cursor = Cursors.Hand;
        }

        private void btnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            // Создаем анимацию изменения цвета фона кнопки
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)ColorConverter.ConvertFromString("#34c6cd");
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3); // Длительность анимации

            // Применяем анимацию к свойству Background кнопки
            btnBack.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Устанавливаем остальные свойства
            btnBack.BorderThickness = new Thickness(0);
        }

    }
}
