using System;

namespace Hospital.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 100;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Disease { get; set; }
        public DateTime DateTime { get; set; }
        public string Staff { get; set; }
        public string Patient { get; set; }
        public int? StaffId { get; set; }
        public int? PatientId { get; set; }
    }
}