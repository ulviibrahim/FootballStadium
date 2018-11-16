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
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        ReservationEntities db = new ReservationEntities();
        //private object cmbContact;
        MainWindow mw;
   
        
        public ReservationEntities Db { get => db; set => db = value; }
        public object MessageBoxButtons { get; private set; }
        public Contacts SelectedUser { get; private set; }

        public Contacts selectedContact;
        public AddPerson(MainWindow main)
        {
            InitializeComponent();
            //FillContacts();
            FillFullName();
            this.mw = main;
        }
       

      
        private void FillFullName()
        {
            foreach (Contacts contact in Db.Contacts.OrderBy(c => c.Name).ToList())
            {
                Fullname fullname = new Fullname
                {
                    Id = contact.Id,
                    All = contact.Name + " " + contact.Surname + " " + contact.Phone
                };
                CmbContact.Items.Add(fullname);
                
            }
           
        }

        private void CmbContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BtnUpdate.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Hidden;

           
            Fullname fullname = CmbContact.SelectedItem as Fullname;
            Contacts contact = db.Contacts.Find(fullname.Id);
            if (contact != null)
            {
                TxtName.Text = contact.Name;
                TxtSurname.Text = contact.Surname;
                TxtPhone.Text = contact.Phone;
                TxtEmail.Text = contact.Email;

           
                
            }

          
        }
        //Reset metodu
        public void Reset()
        {
            BtnUpdate.Visibility = Visibility.Hidden;
            BtnDelete.Visibility = Visibility.Hidden;
            BtnAdd.Visibility = Visibility.Visible;

            TxtName.Text = "";
            TxtSurname.Text = "";
            TxtPhone.Text = "";
            TxtEmail.Text = "";
        }

        //Add buttonunua click eventi
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtName.Text) && string.IsNullOrEmpty(TxtSurname.Text) && string.IsNullOrEmpty(TxtPhone.Text) && string.IsNullOrEmpty(TxtEmail.Text))
            {
                MessageBox.Show("xanalari doldur");
              
                return;

            }
            CmbContact.Items.Clear();
            Contacts contact = new Contacts
            {

                Name = TxtName.Text,
                Surname = TxtSurname.Text,
                Phone = TxtPhone.Text,
                Email = TxtEmail.Text
            };
              


      
            
                db.Contacts.Add(contact);
            db.SaveChanges();
            FillFullName();
            mw.FillContacts();
           
            Reset();


        }

        //private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        //{

        //    Contacts selectedContact = new Contacts
        //    {


        //        Name = TxtName.Text,
        //        Surname = TxtSurname.Text,
        //        Phone = TxtPhone.Text,
        //        Email = TxtEmail.Text
        //    };
        //    db.SaveChanges();
        //    mw.FillContacts();

        //    Reset();
        //}

      
    }
}
