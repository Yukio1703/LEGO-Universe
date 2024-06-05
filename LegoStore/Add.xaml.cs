using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace LegoStore
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }
        private Products editMain;

        public Add(Products editMain)
        {
            InitializeComponent();
            this.editMain = editMain;

            //получение данных из admin для изменение
            var nameItemCh = editMain.Name.ToString();
            var infoItemCh = editMain.Description.ToString();
            var priceCh = editMain.Price.ToString();
            var photoItemCh = editMain.photoItem.ToString();
            var idCategorCh = editMain.CategoryID;


            //ввод этих данных на их места
            nameItem.Text = nameItemCh;
            infoItem.Text = infoItemCh;
            price.Text = Convert.ToString(priceCh);
            photoItem.Text = photoItemCh;
            idCategor.SelectedIndex = Convert.ToInt32(idCategorCh) - 1;
        }
        //Добавление товара в products
        private void Additem_Click(object sender, RoutedEventArgs e)
        {
            //получение данных в переменную
            var NA = nameItem.Text.ToString();
            var PR = price.Text.ToString();
            var INF = infoItem.Text.ToString();
            var PHOTOITEM = photoItem.Text.ToString();
            var CA = idCategor.SelectedIndex + 1;

            //проверка данных на корректность
            int error = 0;
            if (NA.Length < 1)
            {
                error++;
            }
            if (PR.Length < 1)
            {
                error++;
            }
            if (INF.Length < 1)
            {
                error++;
            }
            if (PHOTOITEM.Length < 1)
            {
                error++;
            }
            if (CA == 0)
            {
                error++;
            }

            //если ошибок нет, то добавляем товар
            if (error == 0)
            {
                try
                {
                    var ProductID = 0;
                    if (editMain != null)
                    {
                        ProductID = editMain.ProductID;
                    }
                    Products mainItems = new Products()
                    {
                        ProductID = ProductID,
                        Name = nameItem.Text,
                        Description = infoItem.Text,
                        Price = Convert.ToInt32(price.Text),
                        photoItem = photoItem.Text,
                        CategoryID = idCategor.SelectedIndex + 1,
                    };

                    AppConnect.model0db.Products.AddOrUpdate(mainItems);
                    AppConnect.model0db.SaveChanges();
                    MessageBox.Show("товар добавлен");

                    //переходим на страницу с товаром
                    Admin admin = new Admin();
                    admin.Show();
                    Close();
                }
                catch (Exception ex) { MessageBox.Show("что то пошло не так"); }
            }
            //если есть ошибки то предупреждение
            else
            {
                MessageBox.Show("Обнаружены пустые данные");
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            Close();
        }
    }
}
