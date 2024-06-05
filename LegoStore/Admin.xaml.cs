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
using System.Windows.Shapes;

namespace LegoStore
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            ListView1.ItemsSource = FindMain();
            sortItems.SelectedIndex = 0;
        }

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

            if (sortItems.SelectedIndex != 0)
            {
                mains = mains.Where(x => x.CategoryID.ToString() == sortItems.SelectedIndex.ToString()).ToList();
            }

            var mainAll = mains;
            return mains.ToArray();
        }

        // поиск
        private void findItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }




        //фильтрация
        private void sortItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
        }

        //кнопка для удаление товара из таблицы
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ProductID = button.Tag;
            var itemDel = AppConnect.model0db.Products.Where(x => x.ProductID == (int?)ProductID);
            try
            {
                AppConnect.model0db.Products.RemoveRange(itemDel);
                AppConnect.model0db.SaveChanges();
                AppConnect.model0db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ListView1.ItemsSource = AppConnect.model0db.Products.ToList();
            }
            catch
            {
                MessageBox.Show("Обьект не удален");
            }
        }

        //переход в добавление
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add();
            add.Show();
            Close();
        }
        //Изменение (перход на страницу с добавлением и сохраняем данных)
        private void change_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var itemSend = button.DataContext as Products;
            Add addItem = new Add(itemSend);
            addItem.Show();
            Close();
        }
    }
}
