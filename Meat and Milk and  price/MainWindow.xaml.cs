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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Meat_and_Milk_and__price.Model;
using Meat_and_Milk_and__price.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Meat_and_Milk_and__price
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// Класс предназначенный для перехода на окна продуктов, группы продуктов и экспорта таблицы Producs в PDF файл
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик события на нажатие кнопки Product_Group_bt
        /// Перенаправляет на страницу просмотра списка Группы продуктов
        /// </summary>
        private void Product_Group_bt_Click(object sender, RoutedEventArgs e)
        {
            Windows.Product_Group pg = new Windows.Product_Group();
            pg.Show();
            this.Close();
        }
        /// <summary>
        /// Обработчик события на нажатие кнопки Products_btn
        /// Перенаправляет на страницу Продуктов
        /// </summary>
        private void Products_btn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Products products = new Windows.Products();
            products.Show();
            this.Close();
        }
        /// <summary>
        /// Обработчик события на нажатие кнопки Products_pdf_btn
        /// Экспорт таблицы Products в PDF файл
        /// </summary>
        private void Products_pdf_btn_Click(object sender, RoutedEventArgs e)
        {
            using (Price_AccountEntities ent = new Price_AccountEntities())
            {
                List<Model.Products> products = ent.Products.OrderBy(x => x.Name).ToList();
                var app = new Word.Application();
                Word.Document doc = app.Documents.Add();
                Word.Paragraph paragraph = doc.Paragraphs.Add();
                Word.Range range = paragraph.Range;
                range.Text = "Продукты";
                paragraph.set_Style("Заголовок 1");
                range.InsertParagraphAfter();
                Word.Paragraph tableParagraph = doc.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table teachersTable = doc.Tables.Add(tableRange, products.Count() + 1, 5);
                teachersTable.Borders.InsideLineStyle = teachersTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                teachersTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                Word.Range cellRange;
                cellRange = teachersTable.Cell(1, 1).Range;
                cellRange.Text = "Порядковый номер";
                cellRange = teachersTable.Cell(1, 2).Range;
                cellRange.Text = "Название";
                cellRange = teachersTable.Cell(1, 3).Range;
                cellRange.Text = "Сорт";
                cellRange = teachersTable.Cell(1, 4).Range;
                cellRange.Text = "Порядковый номер группы продуктов";
                cellRange = teachersTable.Cell(1, 5).Range;
                cellRange.Text = "Порядковый номер комбайна";
                teachersTable.Rows[1].Range.Bold = 1;
                teachersTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int i = 1;
                foreach (var currentProduct in products)
                {
                    cellRange = teachersTable.Cell(i + 1, 1).Range;
                    cellRange.Text = currentProduct.Id.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = teachersTable.Cell(i + 1, 2).Range;
                    cellRange.Text = currentProduct.Name.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = teachersTable.Cell(i + 1, 3).Range;
                    cellRange.Text = currentProduct.Sort.ToString().Split(' ')[0];
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = teachersTable.Cell(i + 1, 4).Range;
                    cellRange.Text = currentProduct.Id_Product_Group.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = teachersTable.Cell(i + 1, 5).Range;
                    cellRange.Text = currentProduct.Id_Combines.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    i++;
                }
                app.Visible = true;
                doc.SaveAs2(@"C:\Users\Никита\Downloads\Products.pdf", Word.WdExportFormat.wdExportFormatPDF);
            }
        }
    }
}
