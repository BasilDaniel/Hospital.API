namespace Hospital.API.Dtos
{
    public class AdminsList
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }

        public SharedPositionDetailed Position { get; set; }

        public SharedDepartmentDetailed Department { get; set; }
    }
}