using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            items.entity = new Entities3();
            ListView1.ItemsSource = FindMain();
            Sort.SelectedIndex = 0;
        }
        private void card_Click(object sender, RoutedEventArgs e)
        {

        }

        private void card_Click_1(object sender, RoutedEventArgs e)
        {
            card card = new card();
            card.Show();
            this.Close();
        }


        // poisk
        Products[] FindMain()
        {
            List<Products> mains = AppConnect.model0db.Products.ToList();

            if (String.IsNullOrEmpty(findItems.Text) || String.IsNullOrWhiteSpace(findItems.Text))
            {

            }
            else
            {
                mains = mains.Where(x => x.Name.ToLower().Contains(findItems.Text.ToLower())).ToList();
            }
            if (Sort.SelectedIndex != 0)
            {
                mains = mains.Where(x => x.CategoryID.ToString() == Sort.SelectedIndex.ToString()).ToList();
            }

            var mainAll = mains;
            return mains.ToArray();
        }

        private void findItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }
        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }
        //korzina
        private void buy_Click(object sender, RoutedEventArgs e)
        {
          

        }

        private void buy_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag;
            int userId = (int)App.Current.Properties["userEmail"];
            id = Convert.ToInt32(id);

            try
            {
                OrderDetails card = new OrderDetails()
                {
                    OrderID = (int?)id,
                    UserID = userId,
                    ProductID = (int?)id,
                    Quantity = 1,
                };
                AppConnect.model0db.OrderDetails.Add(card);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("товар отправлен в корзину + " + userId + " + " + Convert.ToInt32(id));
            }
            catch (Exception ex) { MessageBox.Show("товар не отправлен в корзину"); }
        }
    }
}
