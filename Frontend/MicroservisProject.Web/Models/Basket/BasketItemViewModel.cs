namespace MicroservisProject.Web.Models.Basket
{
    public class BasketItemViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        private decimal? DiscountAppliedPrice { get; set; }

        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
        public decimal CurrentPrice => DiscountAppliedPrice.HasValue ? DiscountAppliedPrice.Value : Price;
    }
}
