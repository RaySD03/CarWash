using CarWash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    class CarList_Selected
    {
        public string Identifier { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Service { get; set; }

        public static ObservableCollection<CarList_Selected> Cars = new ObservableCollection<CarList_Selected>();
        public Command<CarList_Selected> RemoveCommand
        {
            get
            {
                return new Command<CarList_Selected>((Car) => {
                    Cars.Remove(Car);
                });
            }
        }

        public Command<CarList_Selected> AddCommand
        {
            get
            {
                return new Command<CarList_Selected>((Car) => {
                    Cars.Add(Car);
                });
            }
        }

    }

}
