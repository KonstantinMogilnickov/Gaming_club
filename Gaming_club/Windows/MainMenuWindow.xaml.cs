using Microsoft.Win32;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace Gaming_club.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        private User_data _data;
        Gaming_clubEntities db = new Gaming_clubEntities();
        public MainMenuWindow(User_data user)
        {
            InitializeComponent();
            _data = user;
            FillUserData();
        }

        private void FillUserData()
        {
            txtName.Text = _data.name;
            txtSurname.Text = _data.surname;
            txtPatrynumic.Text = _data.patrrynumic;
            txtPhoneNumber.Text = _data.phone_number;
            txtLogin.Text = _data.login;
            txtPassword.Text = _data.password;
            // Загрузка изображения профиля
            if (!string.IsNullOrEmpty(_data.image))
            {
                profileImage.Source = new BitmapImage(new Uri(_data.image));
            }
        }

        private string SavePhoneNumber(string phoneNumber)
        {
            // Просто возвращаем номер телефона
            return phoneNumber;
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (gridCabinet.Visibility == Visibility.Hidden)
            {
                gridCabinet.Visibility = Visibility.Visible;
            }

            else
            {
                gridCabinet.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Добро пожаловать {_data.name}");
        }

        private string SaveProfileImage()
        {
            string imagesDirectory = "ProfilesImages";
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, imagesDirectory);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fileName = $"{_data.id}_profile_image.jpg";
            string filePath = Path.Combine(directoryPath, fileName);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapImage)profileImage.Source));
                encoder.Save(memoryStream);

                File.WriteAllBytes(filePath, memoryStream.ToArray());
            }

            return filePath;
        }

        private void editImageBtn_Click(object sender, RoutedEventArgs e)
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
                _data.image = imagePath;
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string surname = txtSurname.Text.Trim();
                string patrynumic = txtPatrynumic.Text.Trim();
                string login = txtLogin.Text.Trim();
                string password = txtPassword.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();
                User_data userToUpdate = db.User_data.FirstOrDefault(u => u.id == _data.id);
                // Проверка на заполненность всех полей
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(patrynumic) || string.IsNullOrWhiteSpace(login) ||
                    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber))
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                else if (userToUpdate != null)
                {
                    userToUpdate.name = name;
                    userToUpdate.surname = surname;
                    userToUpdate.patrrynumic = patrynumic;
                    userToUpdate.login = login;
                    userToUpdate.password = password;
                    userToUpdate.phone_number = SavePhoneNumber(phoneNumber);

                    if (!string.IsNullOrEmpty(_data.image))
                    {
                        userToUpdate.image = SaveProfileImage();
                    }
                    db.SaveChanges();
                    MessageBox.Show("Изменения сохранены!");
                }
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
    }
}
