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
        MainWindow Sta;
        ReservationEntities db = new ReservationEntities();
       


        public Stadiums Meydanca;
        public Stadium(MainWindow main)

        {
           
            InitializeComponent();
            this.Sta = main;
            Fillstadiums();
         
           
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
        public void FillAllFields()
        {
            CmbStadiums.SelectedValuePath = Meydanca.Id.ToString();
            TxtStadium.Text = Meydanca.Name;
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
                Id = Convert.ToInt32(CmbStadiums.SelectedValue),
              Name=TxtStadium.Text
            };
            db.Stadiums.Add(stadium);
            db.SaveChanges();
            
            Sta.fillStadiums();
            
            Close();
        
    }
        public void ForUpdate()
        {
            BtnUpdate.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Hidden;
        }
        //  Stadion yenilemek
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtStadium.Text))
            {
                MessageBox.Show("xanalari doldur");

                return;

            }



          
            Stadiums C = db.Stadiums.Find(Meydanca.Id);
            C.Name = TxtStadium.Text;
            
            
            db.SaveChanges();
            MessageBox.Show("Meydanca yeniləndi");
            Fillstadiums();
            Sta.fillStadiums();

            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            Stadiums con = db.Stadiums.FirstOrDefault(x => x.Id == Meydanca.Id);
            db.Stadiums.Remove(con);

            db.SaveChanges();
            MessageBox.Show("Şəxs silindi");
            Sta.fillStadiums();
            Fillstadiums();
            this.Close();
        }
    }
}
