using Gaming_club.Windows;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Gaming_club.classes
{

    public class RegistrationUser
    {
        private Gaming_clubEntities db;

        public RegistrationUser(Gaming_clubEntities dbContext)
        {
            this.db = dbContext;
        }

        public void RegisterUser(string name, string surname, string patrynumic, string phoneNumber, int permission, string login, string password, string age, string image, string confirmPassword, RegistrationWindow registration)
        {
            Regex passwordRegex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");
            //проверка на заполнение полей
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(patrynumic) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(login) || age == null)
            {
                MessageBox.Show("Вы не заполнили все поля!");
            }

            else if(password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
            }

           else  if (db.User_data.Any(u => u.login == login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует. Пожалуйста, выберите другой логин.");
                return;
            }

            //Проверка сложности пароля
            
            else if (!passwordRegex.IsMatch(password))
            {
                MessageBox.Show("Пароль не соответствует требуемым критериям сложности:\n" +
                                "- Минимум 8 символов\n" +
                                "- Минимум 1 заглавная буква\n" +
                                "- Минимум 1 цифра\n" +
                                "- Минимум 1 дополнительный символ по типу: -+%$#");
                return;
            }

            else {
                try
                {
                    permission = 1;
                    User_data user_Data = new User_data()
                    {
                        name = name,
                        surname = surname,
                        patrrynumic = patrynumic,
                        phone_number = phoneNumber,
                        id_permission = permission,
                        login = login,
                        password = password,
                        age = age,
                        image = image,
                    };
                    db.User_data.Add(user_Data);
                    db.SaveChanges();
                    MessageBox.Show("Регистрация успешна!");
                    Authorization authorization = new Authorization();
                    authorization.Show();
                    registration.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex}");
                }
            }
        }
    }
}
