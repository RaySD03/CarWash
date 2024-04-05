using CarWash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash.Models
{
    public class Agent
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }


        public static ObservableCollection<Agent> agent = new ObservableCollection<Agent>();

        public Command<Agent> RemoveCommand
        {
            get
            {
                return new Command<Agent>((Agent) => {
                    agent.Remove(Agent);
                });
            }
        }

        public Command<Agent> AddCommand
        {
            get
            {
                return new Command<Agent>((Agent) => {
                    agent.Add(Agent);
                });
            }
        }

    }

   

}
