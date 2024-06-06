using Aspose.BarCode.Generation;
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

            //общая стоимость товаров в корзине 
            var a = AppConnect.model0db.OrderDetail.ToList();
            int sumPrice = 0;

            for (int i = 0; i < a.Count; i++)
            {
                int gooid = (int)a[i].ProductID;
                Products b = AppConnect.model0db.Products.FirstOrDefault(x => x.ProductID == gooid);
                sumPrice += (int)b.Price;
            }
            coast.Text = sumPrice.ToString();

            userID = userid;
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
        //qr функция
        int a = 1;

        private void doQR()
        {
            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://youtu.be/XBk3Oa_YHKM?si=BQkWec58r2vGD5H2");
            gen.Parameters.Barcode.XDimension.Pixels = 34;
            string dataDir = @"S:\USERS\51-02\Хлопяник Андрей Игоревич\LegoStore\LegoStore";
            gen.Save(dataDir + a.ToString() + "1.png", BarCodeImageFormat.Png);
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + a.ToString() + "1.png");
            bitmap.EndInit();
            QRimg.Source = bitmap;
            a++;
        }

        private void by_Click(object sender, RoutedEventArgs e)
        {
            doQR();
        }
    }
}
