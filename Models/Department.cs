using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hospital.API.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Staff> Staffs{ get; set; } 

        public Department()
        {
            Staffs = new Collection<Staff>();
        }
    }
}