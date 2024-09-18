namespace TourUI.Model
{
    public class TourMessage
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }

        public TourMessage(string name, string email, string location, string type)
        {
            Name = name;
            Email = email;
            Location = location;
            Type = type;
        }
    }
}
