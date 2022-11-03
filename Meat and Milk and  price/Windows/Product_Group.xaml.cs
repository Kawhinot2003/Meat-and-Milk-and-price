using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using Meat_and_Milk_and__price.Model;

namespace Meat_and_Milk_and__price.Windows
{
    /// <summary>
    /// Логика взаимодействия для Product_Group.xaml
    /// Данный класс предназначен для вывода группы продуктов
    /// </summary>
    public partial class Product_Group : Window
    {
        public Product_Group()
        {
            InitializeComponent();
            using (Price_AccountEntities price = new Price_AccountEntities())
            {
                GroupsGrid.ItemsSource = price.Product_Group.ToList();
            }
        }
        /// <summary>
        /// Обработчик события на нажатие кнопки Backbtn
        /// Открывается страница MainWindow
        /// </summary>
        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
