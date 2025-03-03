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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Footprint
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static FootprintBaseEntities baza = new FootprintBaseEntities();
        public History history;
        DataBaseFootprint dataBaseFootprint;
        public static bool isClosingHandled = false;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                dataBaseFootprint = new DataBaseFootprint(baza);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            cbAccessLevel.ItemsSource = dataBaseFootprint.baza.AccessLevels.ToList();
        }
        
        /// <summary>
        /// Вход в программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignInToTheProgram_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbAccessLevel.SelectedIndex == -1)
                    throw new Exception("Пожалуйста, выберите пользователя из списка.");

                if (passwordBox.Password == "")
                    throw new Exception("Введите пароль.");

                string accessLevel = (cbAccessLevel.SelectedItem as AccessLevels).AccessLevel;
                string password = passwordBox.Password;
                string userName = GetStaffName(accessLevel, password);

                if (VerifyPassword(accessLevel, password))
                {
                    var staff = dataBaseFootprint.baza.Staff.ToList();
                    int staffId = staff.FirstOrDefault(s => s.FIO == userName)?.StaffId ?? 0;

                    if (staffId != 0)
                    {
                        History history = new History()
                        {
                            StaffId = staffId,
                            EntryDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")
                        };

                        dataBaseFootprint.baza.History.Add(history);
                        dataBaseFootprint.baza.SaveChanges();

                        switch (accessLevel)
                        {
                            case "Администратор":
                                Windows.Окно_администратора окно_Администратора = new Windows.Окно_администратора(userName, staff.FirstOrDefault(s => s.FIO == userName));
                                окно_Администратора.Show();
                                break;
                            case "Кладовщик":
                                Windows.Окно_кладовщика окно_кладовщика = new Windows.Окно_кладовщика(userName, staff.FirstOrDefault(s => s.FIO == userName));
                                окно_кладовщика.Show();
                                break;
                            case "Продавец":
                                Windows.Окно_продавца окно_продавца = new Windows.Окно_продавца(userName, staff.FirstOrDefault(s => s.FIO == userName));
                                окно_продавца.Show();
                                break;
                        }

                        Hide();
                    }
                    else
                    {
                        throw new Exception("Сотрудник с таким именем не найден.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Проверка правильности введенного пароля
        /// </summary>
        /// <param name="accessLevel"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string accessLevel, string password)
        {
            try
            {
                var user = baza.Staff.FirstOrDefault(u => u.AccessLevel == accessLevel && u.Password == password);

                if (user == null)
                {
                    MessageBox.Show("Ошибка: Неверный пароль", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Функция для отображения имени пользователя на форме
        /// </summary>
        /// <param name="accessLevel"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetStaffName(string accessLevel, string password)
        {
            try
            {
                var user = baza.Staff.FirstOrDefault(u => u.AccessLevel == accessLevel && u.Password == password);

                if (user == null)
                {
                    return null;
                }

                return user.FIO;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Функция отображения и скрытия пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxPwd_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                textBoxForVisible.Text = passwordBox.Password;
                textBoxForVisible.Visibility = Visibility.Visible;
                passwordBox.Visibility = Visibility.Hidden;
            }
            else
            {
                passwordBox.Password = textBoxForVisible.Text;
                textBoxForVisible.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Открытие справки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void F1Shortcut1(object sender, ExecutedRoutedEventArgs e)
        {
            string commandText = $"Справка информационной системы Footprint.chm";
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}
