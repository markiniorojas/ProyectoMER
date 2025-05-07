namespace Entity.Model
{
    public class Rents
    {
        public int Id { get; set; }
        public DateTime ContracDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SocialReason { get; set; }
        public bool state { get; set; }
    }
}
