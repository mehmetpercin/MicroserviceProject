namespace MicroservisProject.Web.Models.Basket
{
    public class BasketViewModel
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _items { get; set; }
        public decimal TotalPrice => _items.Sum(x => x.CurrentPrice);
        public bool HasDiscount => !string.IsNullOrEmpty(DiscountCode);
        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _items.ForEach(x =>
                    {
                        var discountPrice = x.Price * (decimal)DiscountRate / 100;
                        x.AppliedDiscount(Math.Round(x.Price - discountPrice, 2));
                    });
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

    }
}
