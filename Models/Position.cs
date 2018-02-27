using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.API.Models
{
   public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Responsibility { get; set; }
        public ICollection<Staff> Staffs { get; set; }

        public Position()
        {
            Staffs = new Collection<Staff>();
        }
    }
}