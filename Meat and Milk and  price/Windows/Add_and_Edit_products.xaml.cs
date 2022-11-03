using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Meat_and_Milk_and__price.Model;
using Meat_and_Milk_and__price.Windows;

namespace Meat_and_Milk_and__price.Windows
{
    /// <summary>
    /// Логика взаимодействия для Add_and_Edit_products.xaml
    /// Данный класс предназначен для добавленияи ректирования продукта
    /// Конструктор класса принимает 2 аргумента
    /// 1. Экземпляр предыдущего окна
    /// 2. Обект типа данных Products (класс описывающий таблицу продуктов)
    /// </summary>
    public partial class Add_and_Edit_products : Window
    {
        Products _productsWindow;
        Model.Products _products;
        public Add_and_Edit_products(Products productsWindow, Model.Products products = null)
        {
            InitializeComponent();
            _productsWindow = productsWindow;
            _products = products;
            if(_products!=null)
            {
                Name_tb.Text = _products.Name;
                Sort_tb.Text = _products.Sort;
                Id_Product_Group.Text = _products.Id_Product_Group.ToString();
                Id_Combines.Text = _products.Id_Combines.ToString();
                Add.Content = "Изменить";
            }
        }
        /// <summary>
        /// Обработчик события на нажатие кнопки Add
        /// Добавляет или редактирует продукт
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (Price_AccountEntities priceent = new Price_AccountEntities())
            {
                try
                {
                    if (_products==null)
                    {
                        List<Model.Products> products = new List<Model.Products>();
                        products = priceent.Products.OrderBy(p => p.Id).ToList();
                        Model.Products lastproduct = products.Last();
                        priceent.Products.Add(new Model.Products
                        {
                            Id = lastproduct.Id + 1,
                            Name = Name_tb.Text,
                            Sort = Sort_tb.Text,
                            Id_Product_Group = Convert.ToInt32(Id_Product_Group.Text),
                            Id_Combines = Convert.ToInt32(Id_Combines.Text)
                        });
                    }
                    else
                    {
                        priceent.Products.Attach(_products);
                        _products.Name = Name_tb.Text;
                        _products.Sort = Sort_tb.Text;
                        _products.Id_Product_Group = Convert.ToInt32(Id_Product_Group.Text);
                        _products.Id_Combines = Convert.ToInt32(Id_Combines.Text);                        
                    }
                    priceent.SaveChanges();
                    _productsWindow.Refresh_Grid();
                    _productsWindow.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Заполните поля правильными типами данных");
                }
            }
        }
    }
}
