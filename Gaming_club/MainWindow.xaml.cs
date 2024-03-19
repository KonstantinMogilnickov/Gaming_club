using Gaming_club.Windows;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;


namespace Gaming_club
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
            this.Close();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}
