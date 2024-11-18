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

namespace Gadara
{
    /// <summary>
    /// Interaction logic for Login_page.xaml
    /// </summary>
    public partial class Login_page : Page
    {
        GadarEntities db = new GadarEntities();
        public Login_page()
        {
            InitializeComponent();
        }

        private void Loginbutton_Click(object sender, RoutedEventArgs e)
        {
            var rec = db.Users.FirstOrDefault(x=>x.U_email == txtEmail.Text && x.U_password == txtpassword.Text);
            if (rec != null && rec.U_role.Contains("Manger"))
            {
                NavigationService.Navigate(new User_Mangment(rec.U_id));
            }
            else if (rec != null)
            {
                NavigationService.Navigate(new View_tasks(rec.U_id));
            }
            else
            {
                MessageBox.Show("you entered in invailed data");
            }
        }
    }
}
