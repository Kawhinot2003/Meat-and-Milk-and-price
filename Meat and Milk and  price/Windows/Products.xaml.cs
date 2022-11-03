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
using Meat_and_Milk_and__price.Model;
using Meat_and_Milk_and__price.Windows;

namespace Meat_and_Milk_and__price.Windows
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// Данный класс предназначен для вывода, удаления продуктов и перенаправления на страницу добавления, редактирования и изменения продуктов
    /// </summary>
    public partial class Products : Window
    {
        public Products()
        {
            InitializeComponent();
            Refresh_Grid();
        }
        /// <summary>
        /// Функция для обновления данных в таблице
        /// </summary>
        public void Refresh_Grid()
        {
            using (Price_AccountEntities price = new Price_AccountEntities())
                ProductsGrid.ItemsSource = price.Products.ToList();
        }
        /// <summary>
        /// Обработчик события на нажаите кнопки BtnEdit
        /// Перенаправляет на страницу редактирования продукта
        /// </summary>
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Add_and_Edit_products aaep = new Add_and_Edit_products(this, (sender as Button).DataContext as Model.Products);
            this.Hide();
            aaep.Show();
        }
        /// <summary>
        /// Обработчик события на нажаите кнопки BtnDelete
        /// Удаляет продукт из Базы Данных
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Model.Products product = (sender as Button).DataContext as Model.Products;
            using (Model.Price_AccountEntities context = new Model.Price_AccountEntities())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                context.Products.Remove(product);
                context.SaveChanges();
                Refresh_Grid();
            }
        }
        /// <summary>
        /// Обработчик события на нажаите кнопки Add
        /// Перенаправляет на страницу добавления продукта
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Add_and_Edit_products aaep = new Add_and_Edit_products(this, (sender as Button).DataContext as Model.Products);
            this.Hide();
            aaep.Show();
        }
        /// <summary>
        /// Обработчик события на нажаите кнопки BackBtn
        /// Перенаправляет на страницу MainWindow
        /// </summary>
        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}
