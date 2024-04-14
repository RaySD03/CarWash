using CarWash.Models;
using CarWash.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    public class Appointment
    {
        public string ApptID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Agent { get; set; }
        public string AgentID { get; set; }

        public static Appointment MyAppointment = new Appointment();

        public static ObservableCollection<Appointment> MyAppointments = new ObservableCollection<Appointment>();
    }
}
