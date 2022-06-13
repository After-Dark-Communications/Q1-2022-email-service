namespace EmailService.Models
{
    [Serializable]
    public class Reservation
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? date { get; set; }
    }
}
