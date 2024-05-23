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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            items.entity = new Entities();
            ListView1.ItemsSource = items.entity.Products.ToList();
            ///hkhigk
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


        // 1
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

            var mainAll = mains;
            return mains.ToArray();
        }

        private void findItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListView1.ItemsSource = FindMain();
            //ssswssq
        }
    }
}
