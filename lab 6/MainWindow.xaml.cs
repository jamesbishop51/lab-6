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

namespace lab_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities db = new NORTHWNDEntities();
        public MainWindow()
        {
            InitializeComponent();
        }
        // exercise one
        private void Ex1Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Categories
                        join p in db.Products on c.CategoryName equals p.Category.CategoryName
                        orderby c.CategoryName
                        select new { Category = c.CategoryName, Product = p.ProductName };

            var result = query.ToList();
            Ex1LbDisplay.ItemsSource = result;


        }
        // exercise two
        private void Ex2Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Products
                        orderby p.Category.CategoryName, p.ProductName
                        select new { Category = p.Category.CategoryName, Product = p.ProductName };

            var result = query.ToList();

            Ex2LbDisplay.ItemsSource = result;
        }
        //exercise three
        private void Ex3Button_Click(object sender, RoutedEventArgs e)
        {
            //return the total number of orders for products 7
            var query1 = (from detail in db.Order_Details
                          where detail.ProductID == 7
                          select detail);
            // returns the total value of the orders for product 7
            var query2 = (from detail in db.Order_Details
                          where detail.ProductID == 7
                          select detail.UnitPrice * detail.Quantity);

            int numberOfOrders = query1.Count();
            decimal totalValue = query2.Sum();
            decimal AverageValue = query2.Average();

            Ex3LbDisplay.Text = string.Format(
                "Total number of orders {0}\nValue of Orders {1:C}\nAverage Order Value {2:c}", numberOfOrders, totalValue, AverageValue);
        }
        //exercise 4
        private void Ex4Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from customer in db.Customers
                        where customer.Orders.Count >= 20
                        select new
                        {
                            Name = customer.CompanyName,
                            OrderCount = customer.Orders.Count
                        };
            Ex4LbDisplay.ItemsSource = query.ToList();

        }
    }
}
