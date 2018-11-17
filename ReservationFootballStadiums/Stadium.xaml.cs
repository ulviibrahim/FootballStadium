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
using ReservationFootballStadiums.Models;

namespace ReservationFootballStadiums
{
    /// <summary>
    /// Interaction logic for Stadium.xaml
    /// </summary>
    public partial class Stadium : Window
    {
        ReservationEntities db = new ReservationEntities();
        MainWindow Sta;
        public Stadium(MainWindow main)

        {
           
            InitializeComponent();
            Fillstadiums();
           this.Sta = main;
        }
        private void Fillstadiums()
        {
            foreach (Stadiums stadium in db.Stadiums.OrderBy(s=>s.Name).ToList())
            {
                CmbStadiums.Items.Add(stadium);
            }
            
        }

        private void CmbStadiums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stadiums name = CmbStadiums.SelectedItem as Stadiums;
            if (name != null)
            {
                MessageBox.Show(name.ToString());
                Stadiums stadium = db.Stadiums.Find(name.Id);
                if (stadium != null)
                {
                    TxtStadium.Text = stadium.Name;
                }
            }
            Stadiums stad = db.Stadiums.FirstOrDefault(s => s.Name == CmbStadiums.Text);
            if (stad != null)
            {
                stad.Name = TxtStadium.Text;
            }

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtStadium.Text))
                
                {
                MessageBox.Show("Zəhmət olmasa Meydança əlavə edin");
                return;
            }
          
            CmbStadiums.Items.Clear();
            Stadiums stadium = new Stadiums
            {
              Name=TxtStadium.Text
            };
            db.Stadiums.Add(stadium);
            db.SaveChanges();
            
            Sta.fillStadiums();
            
            Close();
        
    }


        //Stadiums stadium = Cm.SelectedItem as Stadiums;
        //Contacts contact = db.Contacts.Find(fullname.Id);
        //    if (contact != null)
        //    {
        //        TxtName.Text = contact.Name;
    }
}
