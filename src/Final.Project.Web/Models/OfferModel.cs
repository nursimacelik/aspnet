namespace Final.Project.Web.Models
{
    public class OfferModel
    {
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public bool UsePercentage { get; set; }
        public int Percentage { get; set; }
    }
}
