namespace Entity.Model
{
    public class Debts
    {
        public int Id { get; set; }
        public DateTime DebtDate { get; set; }
        public float GrossValue { get; set; }
        public float IvaValue { get; set; }
        public float DebtTotal { get; set; }
    }
}
