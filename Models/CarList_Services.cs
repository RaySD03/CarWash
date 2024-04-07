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
        public string Identifier { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Service { get; set; }

        public static ObservableCollection<CarList_Services> Cars = new ObservableCollection<CarList_Services>();
        public Command<CarList_Services> RemoveCommand
        {
            get
            {
                return new Command<CarList_Services>((Car) => {
                    Cars.Remove(Car);
                });
            }
        }
        public Command<CarList_Services> AddCommand
        {
            get
            {
                return new Command<CarList_Services>((Car) => {
                    Cars.Add(Car);
                });
            }
        }
    }
}
