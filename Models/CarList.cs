using CarWash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    class CarList
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Icon { get; set; }

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

    }

   

}
