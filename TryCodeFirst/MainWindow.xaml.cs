using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TryCodeFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyContext db = new MyContext();

        public MainWindow()
        {
            InitializeComponent();

            var customer = new Customer { Name = "David", Age = 18, Birthday = DateTime.Now };
            
            db.Customers.Add(customer);
            db.SaveChanges();


            var customer2 = db.Database.SqlQuery<Customer>(
                "Customer_Select @CustomerID", new SqlParameter("CustomerID", customer.CustomerID))
                .SingleOrDefault();
            customer2.Age = 30;
            Update(customer2);

            db.SaveChanges();
            //var customer3 = db.Customers.Find(customer.CustomerID);

            int i = 1;
        }

        /// <summary>
        /// Update Entity (fix same-key-already-exists issue [when automapper])
        /// </summary>
        /// <param name="entity"></param>
        /// <see cref="http://stackoverflow.com/a/12587752/823247"/>
        private void Update(Customer entity)
        {
            var entry = db.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = db.Set<Customer>();
                Customer attachedEntity = set.Local.SingleOrDefault(e => e.CustomerID == entity.CustomerID); // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = db.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }
        }

      
    }
}
