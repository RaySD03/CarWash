using CarWash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    class CarList_Services
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Service { get; set; }

        public static ObservableCollection<CarList> Cars = new ObservableCollection<CarList>();

        public Command<CarList> RemoveCommand
        {
            get
            {
                return new Command<CarList>((Car) => {
                    Cars.Remove(Car);
                });
            }
        }

        public Command<CarList> AddCommand
        {
            get
            {
                return new Command<CarList>((Car) => {
                    Cars.Add(Car);
                });
            }
        }

    }

}
