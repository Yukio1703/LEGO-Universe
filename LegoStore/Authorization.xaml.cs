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

namespace LegoStore
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainframe.Navigate(new Registration());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userObj = AppConnect.model0db.User.FirstOrDefault(x => x.Username == Username.Text && x.Password == Password.Password);
            //проверка если у пользователя 1 уровень доступа то переход на стараницу админ
            if (userObj != null)
            {
                App.Current.Properties["userEmail"] = userObj.UserID;
                Window1 window = new Window1((int)userObj.RoleID);
                if (userObj.RoleID == 1)
                {
                    App.Current.Properties["userEmail"] = userObj.UserID;
                    Admin admin = new Admin();
                    admin.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    window.Show();
                    Application.Current.MainWindow.Close();
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}
