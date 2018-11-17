using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFootballStadiums.Models
{
    class VwReservs
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Stadion { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Email { get; set; }
        public decimal? curPrice
        {
            get;
            set;
        }

    }
}
