using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace LegoStore
{
    public partial class Registration : Page
        {
            public Registration()
            {
                InitializeComponent();
            }

            private void Vhod_Click(object sender, RoutedEventArgs e)
            {
                AppFrame.mainframe.Navigate(new Authorization());
            }

            private void Regis_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    User personObj = new User()
                    {
                        Username = Username.Text,
                        Password = Password.Text,

                    };

                    AppConnect.model0db.User.Add(personObj);
                    AppConnect.model0db.SaveChanges();
                    MessageBox.Show("Пользователь добавлен");
                    AppFrame.mainframe.Navigate(new Authorization());
                }
            catch (Exception) { MessageBox.Show("Ошибка"); }
            }

        }
    }
