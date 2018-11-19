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
        public MainWindow mw;
        
   
        
        public ReservationEntities Db { get => db; set => db = value; }
        public object MessageBoxButtons { get; private set; }
        public Contacts SelectedUser { get; private set; }
        public Contacts Contact;
        public Contacts selectedContact;

        public AddPerson(MainWindow main)
           
        {
            InitializeComponent();
            //FillContacts();
            //FillFullName();
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
        //Update metodu
        private void update()
        {

            
           

        }

        //Add buttonunua click eventi
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

          if (string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtSurname.Text) || string.IsNullOrEmpty(TxtPhone.Text) || string.IsNullOrEmpty(TxtEmail.Text))
            {
                MessageBox.Show("xanalari doldur");

                return;

            }
          

            

            Contacts C = new Contacts();
            C.Name = TxtName.Text;
            C.Surname = TxtSurname.Text;
            C.Phone = TxtPhone.Text;
            C.Email = TxtEmail.Text;
           
            db.Contacts.Add(C);

            db.SaveChanges();
            FillFullName();
            mw.FillContacts();
          
            Reset();
            this.Close();


        }
       
       public void ForUpdate()
        {
            BtnUpdate.Visibility = Visibility.Visible;
            BtnDelete.Visibility = Visibility.Visible;
            BtnAdd.Visibility = Visibility.Hidden;
        }
        // Btn Update metodu
        private void BtnUpdate_Click_1(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(TxtName.Text) && string.IsNullOrEmpty(TxtSurname.Text) && string.IsNullOrEmpty(TxtPhone.Text) && string.IsNullOrEmpty(TxtEmail.Text))
            {
                MessageBox.Show("xanalari doldur");

                return;

            }



            //  Contacts c = CmbContact.SelectedItem as Contacts;
            Contacts C = db.Contacts.Find(Contact.Id);
            C.Name = TxtName.Text;
            C.Surname = TxtSurname.Text;
            C.Phone = TxtPhone.Text;
            C.Email = TxtEmail.Text;

            db.SaveChanges();
            FillFullName();
            db.SaveChanges();
            MessageBox.Show("Şəxs yeniləndi");
            mw.FillContacts();

            this.Close();







        }
        public void FillAllFields()
        {

            TxtName.Text = Contact.Name;
            TxtSurname.Text = Contact.Surname;
            TxtPhone.Text = Contact.Phone;
            TxtEmail.Text = Contact.Email
                ;

        }


        //Btn Delete metodu


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
          
             
            Contacts con = db.Contacts.FirstOrDefault(x => x.Id == Contact.Id); 
            db.Contacts.Remove(con);
          
            db.SaveChanges();
            MessageBox.Show("Şəxs silindi");
            mw.FillContacts();
            FillFullName();
            this.Close();
        }
    }
}
