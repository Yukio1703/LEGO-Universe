using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace LegoStore
{
    /// <summary>
    /// Логика взаимодействия для card.xaml
    /// </summary>
    public partial class card : Window
    {
        int userID = 1;
        public card(int userid)
        {
            InitializeComponent();
            items.entity = new Entities4();
            Card.ItemsSource = AppConnect.model0db.OrderDetail.ToList();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1(userID);
            window.Show();
            this.Close();
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.DataContext as OrderDetail;
            var itemDel = AppConnect.model0db.OrderDetail.Where(x => x.OrderDetailID == id.OrderDetailID);
            try
            {
                AppConnect.model0db.OrderDetail.RemoveRange(itemDel);
                AppConnect.model0db.SaveChanges();
                AppConnect.model0db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                Card.ItemsSource = AppConnect.model0db.OrderDetail.ToList();
                MessageBox.Show("Товар удален");
            }
            catch
            {
                MessageBox.Show("Товар не удален");
            }
        }
    }
}
