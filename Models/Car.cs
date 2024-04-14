using CarWash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    public class Car 
    {
        public string Identifier { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }

        public static ObservableCollection<Car> Cars = new ObservableCollection<Car>();
        public Command<Car> RemoveCommand
        {
            get
            {
                return new Command<Car>((Car) => {
                    Cars.Remove(Car);
                });
            }
        }
        public Command<Car> AddCommand
        {
            get
            {
                return new Command<Car>((Car) => {
                    Cars.Add(Car);
                });
            }
        }
    }
}
