using System;
using System.Collections.Generic;
using System.Globalization;
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
using ReservationFootballStadiums.Models;


namespace ReservationFootballStadiums
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  ReservationEntities entitiden bir instans aliriq
        ReservationEntities db = new ReservationEntities();
        public Bookings Booking;
        public MainWindow()
        {
            InitializeComponent();
            fillcmbSearcStadium();
            fillsrcHours();
            FillContacts();
            
            ClnCalendar.DisplayDateStart = DateTime.Today;



        }


        //Visibility methodu. Butonlari ve Labelleri gizletmek ucun
        private void Visibilhidden()
        {
            CmbHours.Items.Clear();
            CmbHours.Text = "";
            CmbContacts.Items.Clear();
            CmbContacts.Visibility = Visibility.Hidden;
            LblContacts.Visibility = Visibility.Hidden;
            CmbStadiums.Visibility = Visibility.Hidden;
            CmbStadiums.Text = "";
            CmbContacts.Text = "";
            BtnAddStadium.Visibility = Visibility.Hidden;
            BtnReserv.Visibility = Visibility.Hidden;
            BtnUpdate_Delete_Stad.Visibility = Visibility.Hidden;
            BtnUpdate_Delete.Visibility = Visibility.Hidden;
            LblStadium.Visibility = Visibility.Hidden;
            BtnPersonAdd.Visibility = Visibility.Hidden;
        }


        //Saatları doldur
        public void FillHours()

        {
            fillStadiums();
            Visibilhidden();




            TimeSpan StartTime = new TimeSpan(10, 0, 0);
            TimeSpan EndTime = new TimeSpan(2, 0, 0);
            int interval = (int)(EndTime.Subtract(StartTime).TotalHours + 24);

            for (int i = 0; i < interval; i++)
            {


                CmbHours.Items.Add(StartTime.ToString(@"hh\:mm"));




                StartTime = StartTime.Add(new TimeSpan(1, 0, 0));

                if (StartTime.Hours == 0)
                {
                    StartTime = new TimeSpan(0, 0, 0);
                }


            }

        }

        //Şəxsləri commoboxa əlavə etmək üçün
        public void FillContacts()

        {
            CmbContacts.Items.Clear();

            foreach (Contacts contact in db.Contacts.OrderBy(c => c.Name).ToList())
            {
                Fullname fullname = new Fullname
                {
                    Id = contact.Id,
                    All = contact.Name + " " + contact.Surname + " " + contact.Phone
                };
                CmbContacts.Items.Add(fullname);



            }
        }

        // Saatları dəyişmə eventlərində görülən işlər

        private void CmbHours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            fillStadiums();
            if (CmbContacts.SelectedIndex == -1)
            {
                FillContacts();



                CmbStadiums.Visibility = Visibility.Visible;

                BtnAddStadium.Visibility = Visibility.Visible;
                LblStadium.Visibility = Visibility.Visible;



            }

        }

        //Contactlari elave etmek ucun metod.

        private void CmbContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BtnUpdate_Delete.Visibility = Visibility.Visible;
            BtnPersonAdd.Visibility = Visibility.Hidden;
            BtnReserv.Visibility = Visibility.Visible;
        }


        //Şəxsləri əlavə et butonu
        private void BtnPersonAdd_Click_1(object sender, RoutedEventArgs e)
        {
            AddPerson addPerson = new AddPerson(this);
            addPerson.ShowDialog();
        }



        //Stadionlari elave etmek ucun metod.
        #region FillStadium
        public void fillStadiums()
        {
            CmbStadiums.Items.Clear();

            Stadiums stad = db.Stadiums.FirstOrDefault(s => s.Name == CmbStadiums.Text);
            DateTime date = ClnCalendar.SelectedDate.Value;
            if (CmbHours.SelectedItem != null)
            {
                string hour = CmbHours.SelectedItem.ToString();
                TimeSpan time = TimeSpan.Parse(hour);





                


                    foreach (Stadiums stadium in db.Stadiums.Where(s => s.Bookings.Where(b => b.Date == date && b.Time == time).Count() == 0).ToList().OrderBy(n => n.Name))
                {
                    CmbStadiums.Items.Add(stadium);

                }
            }
        }
        #endregion

        //Calendarın dəyişmə eventi
        private void ClnCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            FillHours();


        }



       // BtnUpdate_Delete_Click eventi Contactlar ucun

        private void BtnUpdate_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CmbContacts.SelectedItem == null)
            {

                MessageBox.Show("sehv var");
                LblContacts.Foreground = Brushes.Red;
                return;
            }

            Fullname vwReservs = CmbContacts.SelectedItem as Fullname;
            AddPerson update = new AddPerson(this);
            LblContacts.Foreground = Brushes.White;
            update.Title = "Yenilə və ya Sil";
            update.lblentry.Content = "Şəxləri yenilə və ya sil";
            update.mw = this;
            update.ForUpdate();
            update.Contact = db.Contacts.Find(vwReservs.Id);
            update.FillAllFields();
            update.ShowDialog();



        }
        
        private void BtnAddStadium_Click(object sender, RoutedEventArgs e)
        {
            Stadium add = new Stadium(this);
            add.ShowDialog();
        }

        private void BtnReserv_Click(object sender, RoutedEventArgs e)
        {



            if (string.IsNullOrEmpty(CmbHours.Text) && string.IsNullOrEmpty(CmbStadiums.Text) && string.IsNullOrEmpty(CmbContacts.Text))
            {
                MessageBox.Show("xanalari doldur");

                return;

            }
            TimeSpan StartTime = new TimeSpan(10, 0, 0);
            TimeSpan time = new TimeSpan(Convert.ToInt32(CmbHours.Text.Split(':')[0]), 0, 0);
            string phone = CmbContacts.Text.Split(' ')[2];
            DateTime date = ClnCalendar.SelectedDate.Value;
            if (time.Hours < StartTime.Hours)
            {
                date = date.AddDays(1);
            }



            Bookings booking = new Bookings
            {
                StadiumId = db.Stadiums.FirstOrDefault(s => s.Name == CmbStadiums.Text).Id,
                Date = date,
                Time = time,
                ContactId = db.Contacts.FirstOrDefault(c => c.Phone == phone).Id,

            };
            MessageBox.Show("Rezervasiya uğurla sonlandırıldı");
            db.Bookings.Add(booking);
            db.SaveChanges();
            FillHours();

            ClnCalendar.SelectedDate = DateTime.Now;




        }
        private void fillsrcHours()
        {

            TimeSpan StartTime = new TimeSpan(10, 0, 0);
            TimeSpan EndTime = new TimeSpan(2, 0, 0);
            int interval = (int)(EndTime.Subtract(StartTime).TotalHours + 24);


            for (int i = 0; i < interval; i++)
            {


                cmbSearchHours.Items.Add(StartTime.ToString(@"hh\:mm"));




                StartTime = StartTime.Add(new TimeSpan(1, 0, 0));

                if (StartTime.Hours == 0)
                {
                    StartTime = new TimeSpan(0, 0, 0);
                }


            }

        }
        private void fillcmbSearcStadium()
        {
            cmbSearchStadiums.Items.Clear();

            CmbStadiums.Items.Clear();
            Stadiums stadiumAll = new Stadiums
            {
                Id = 0,
                Name = "-Hamısı-"
            };
            cmbSearchStadiums.Items.Add(stadiumAll);
            cmbSearchStadiums.SelectedValuePath = "0";






            foreach (Stadiums stadium in db.Stadiums.ToList().OrderBy(n => n.Name))
            {
                cmbSearchStadiums.Items.Add(stadium);

            }


        }

        private void CmbStadiums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnAddStadium.Visibility = Visibility.Hidden;
            BtnUpdate_Delete_Stad.Visibility = Visibility.Visible;
            LblContacts.Visibility = Visibility.Visible;
            CmbContacts.Visibility = Visibility.Visible;
            BtnPersonAdd.Visibility = Visibility.Visible;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            DgBookings.Items.Clear();

            int stadiumId = 0;

            if (!string.IsNullOrEmpty(cmbSearchStadiums.Text) && cmbSearchStadiums.Text != "-Hamısı-")
            {
                stadiumId = db.Stadiums.FirstOrDefault(s => s.Name == cmbSearchStadiums.Text).Id;
            }




            List<Bookings> bookings = db.Bookings.Where(b => b.Date == dpSearch.SelectedDate.Value &&

                (stadiumId != 0 ? b.StadiumId == stadiumId : true)
                && (b.Contacts.Name.ToLower().Contains(txtSearchname.Text.ToLower())
                || b.Contacts.Surname.ToLower().Contains(txtSearchname.Text.ToLower())
                || b.Contacts.Phone.Contains(txtSearchname.Text))).ToList();


            foreach (Bookings book in bookings.Where(b => b.Date == dpSearch.SelectedDate.Value).ToList())

            {

                VwReservs item = new VwReservs
                {
                    Id = book.Id,
                    Fullname = book.Contacts.Name + " " + book.Contacts.Surname,
                    Date = book.Date.ToString("dd.MM.yyyy"),
                    Time = book.Time.ToString(@"hh\:mm"),
                    Email = book.Contacts.Email,
                    Stadion = book.Stadiums.Name,
                    curPrice = book.Stadiums.price


                };
                DgBookings.Items.Add(item);

            }

        
                
            }

        private void BtnUpdate_Delete_Stad_Click(object sender, RoutedEventArgs e)
        {
            if (CmbStadiums.SelectedItem == null)
            {

                MessageBox.Show("sehv var");
                LblStadium.Foreground = Brushes.Red;
                return;
            }
           
            Stadiums vwReservs = CmbStadiums.SelectedItem as Stadiums;
            Stadium updateStadium = new Stadium(this);
            updateStadium.ShowDialog();
            
            LblStadium.Foreground = Brushes.White;
            updateStadium.Title = "Yenilə və ya Sil";
            updateStadium.LblEntry.Content = "Meydanca yenilə və ya sil";
           
            updateStadium.ForUpdate();
            updateStadium.Meydanca = db.Stadiums.Find(vwReservs.Id);
            
            updateStadium.FillAllFields();
         

        }
    }

    
}
