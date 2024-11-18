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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gadara
{
    /// <summary>
    /// Interaction logic for View_tasks.xaml
    /// </summary>
    public partial class View_tasks : Page
    {
        GadarEntities db = new GadarEntities();
        public int id { get; set; }
        public View_tasks(int id)
        {
            InitializeComponent();
            this.id = id;
            var rec = db.Users.FirstOrDefault(x=>x.U_id == id);
            txtname.Text = $"Welcome {rec.U_name}";
            var rec2= db.Tasks.Where(x => x.T_status.Contains("Completed")).Select(x => new
            {
                id=x.T_id,
                title=x.T_title,
                description=x.T_desc,
                stat = x.T_status,
            }).ToList();
            dgc.ItemsSource = rec2;
            var rec3 = db.Tasks.Where(x => x.T_status.Contains("Pending")).Select(x => new
            {
                id = x.T_id,
                title = x.T_title,
                description = x.T_desc,
                stat = x.T_status,
            }).ToList();
            dgp.ItemsSource = rec3;
            cm.ItemsSource = db.Tasks.Select(d => d.T_status).Distinct().ToList();
            cm.SelectedIndex = 0;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Task rec = new Task();
            int num = int.Parse(Txttid.Text);
            rec = db.Tasks.FirstOrDefault(x => x.T_id == num);
            if (rec != null)
            {
                var user = db.Users.FirstOrDefault(d => d.U_id == id);
                if (user != null)
                {
                    rec.U_id = user.U_id;
                    rec.T_status = cm.SelectedItem.ToString();
                }
                db.Tasks.AddOrUpdate(rec);
                db.SaveChanges();
                MessageBox.Show("data changed");
                var rec2 = db.Tasks.Where(x => x.T_status.Contains("Completed")).Select(x => new
                {
                    id = x.T_id,
                    title = x.T_title,
                    description = x.T_desc,
                    stat = x.T_status,
                }).ToList();
                dgc.ItemsSource = rec2;
                var rec3 = db.Tasks.Where(x => x.T_status.Contains("Pending")).Select(x => new
                {
                    id = x.T_id,
                    title = x.T_title,
                    description = x.T_desc,
                    stat = x.T_status,
                }).ToList();
                dgp.ItemsSource = rec3;
            }
            else
            {
                MessageBox.Show("you entered invalid task id");
            }
        }
    }
}
