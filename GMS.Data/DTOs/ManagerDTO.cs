namespace GMS.Data.DTOs
{
    public class ManagerDTO
    {
        public int ManagerId { get; set; }
        public string ManagerType { get; set; }
        public string GymName { get; set; }
    }
    public class CreateManagerDTO
    {
        public string GMSUserId { get; set; }
        public string ManagerType { get; set; }
        public int GymId { get; set; }
    }
}