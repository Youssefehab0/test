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
    /// Interaction logic for User_Mangment.xaml
    /// </summary>
    public partial class User_Mangment : Page
    {
        GadarEntities db = new GadarEntities();
        public int id { get; set; }
        public User_Mangment(int id)
        {
            InitializeComponent();
            this.id = id;
            var rec = db.Tasks.Select(x => new
            {
                useremail = x.User.U_email,
                x.T_id,
                Name = x.T_title,
                desc = x.T_desc,
                stat = x.T_status,
            }).ToList();
            dg.ItemsSource = rec;
            cm.ItemsSource = db.Tasks.Select(d => d.T_status).Distinct().ToList();
            cm.SelectedIndex = -1;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            Task rec = new Task();
            var user = db.Users.FirstOrDefault(d => d.U_email == txtemail.Text);
            if (user != null)
            {
                rec.U_id = user.U_id;
                rec.T_title = txtTitle.Text;
                rec.T_desc = txtdesc.Text;
                rec.T_status = cm.SelectedItem.ToString();
            }
            db.Tasks.Add(rec);
            db.SaveChanges();
            MessageBox.Show("the task was Added");
            var rec2 = db.Tasks.Select(x => new
            {
                useremail = x.User.U_email,
                x.T_id,
                Name = x.T_title,
                desc = x.T_desc,
                stat = x.T_status,
            }).ToList();
            dg.ItemsSource = rec2;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Task rec = new Task();
            int num = int.Parse(Txttid.Text);
            rec = db.Tasks.FirstOrDefault(x => x.T_id == num);
            if (rec != null)
            {
                db.Tasks.Remove(rec);
                db.SaveChanges();
                MessageBox.Show("the task was deleted");
                var rec2 = db.Tasks.Select(x => new
                {
                    useremail = x.User.U_email,
                    x.T_id,
                    Name = x.T_title,
                    desc = x.T_desc,
                    stat = x.T_status,
                }).ToList();
                dg.ItemsSource = rec2;
            }
            else
            {
                MessageBox.Show("you entered invalid task id");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txttid.Text) || string.IsNullOrWhiteSpace(txtdesc.Text) || string.IsNullOrWhiteSpace(txtemail.Text) || string.IsNullOrWhiteSpace(txtemail.Text))
            {
                MessageBox.Show("you should enter all data");
            }
            else
            {
                Task rec = new Task();
                int num = int.Parse(Txttid.Text);
                rec = db.Tasks.FirstOrDefault(x => x.T_id == num);
                if (rec != null)
                {
                    var user = db.Users.FirstOrDefault(d => d.U_email == txtemail.Text);
                    if (user != null)
                    {
                        rec.U_id = user.U_id;
                        rec.T_title = txtTitle.Text;
                        rec.T_desc = txtdesc.Text;
                        rec.T_status = cm.SelectedItem.ToString();
                    db.Tasks.AddOrUpdate(rec);
                    db.SaveChanges();
                    MessageBox.Show("data edited");
                    }
                    else
                    {
                        MessageBox.Show("this email not in the system");
                    }
                    var rec2 = db.Tasks.Select(x => new
                    {
                        useremail = x.User.U_email,
                        x.T_id,
                        Name = x.T_title,
                        desc = x.T_desc,
                        stat = x.T_status,
                    }).ToList();
                    dg.ItemsSource = rec2;
                }
                else
                {
                    MessageBox.Show("you entered invalid Task id");
                }
            }
        }
    }
}
