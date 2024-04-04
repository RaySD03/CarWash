using CarWash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    class Garage
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }

        public static ObservableCollection<Garage> Cars = new ObservableCollection<Garage>();

        public Command<Garage> RemoveCommand
        {
            get
            {
                return new Command<Garage>((Car) => {
                    Cars.Remove(Car);
                });
            }
        }
        public Command<Garage> AddCommand
        {
            get
            {
                return new Command<Garage>((Car) => {
                    Cars.Add(Car);
                });
            }
        }

    }

   

}
