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
        int userID = 1;
        public Window1(int userid)
        {
            InitializeComponent();
            items.entity = new Entities4();
            ListView1.ItemsSource = FindMain();
            Sort.SelectedIndex = 0;
            userID = userid;
        }
        private void card_Click(object sender, RoutedEventArgs e)
        {

        }

        private void card_Click_1(object sender, RoutedEventArgs e)
        {
            card card = new card(userID);
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
            var id = button.DataContext as Products;
            int userId = (int)App.Current.Properties["userEmail"];

            try
            {
                // Получить или сгенерировать OrderID
                int orderId = GetOrCreateOrderId(userId);

                OrderDetail card = new OrderDetail()
                {
                    OrderID = orderId,
                    UserID = userId,
                    ProductID = id.ProductID,
                    Quantity = 1,
                };

                Entities4.GetContext().OrderDetail.Add(card);
                Entities4.GetContext().SaveChanges();

                MessageBox.Show("товар добавлен в корзину");
            }
            catch (Exception ex)
            {
                MessageBox.Show("товар не добавлен в корзину: " + ex.Message);
            }
        }

        private int GetOrCreateOrderId(int userId)
        {
            var context = Entities4.GetContext();

            // Проверить, есть ли открытый ордер у пользователя
            var order = context.Orders.FirstOrDefault(o => o.OrderID == userId);

            if (order == null)
            {
                // Создать новый заказ, если его нет.
                order = new Orders()
                {
                    OrderID = userId,
                   
                    OrderDate = DateTime.Now
                };
                context.Orders.Add(order);
                context.SaveChanges();
            }

            return order.OrderID;
        }
    }
}

