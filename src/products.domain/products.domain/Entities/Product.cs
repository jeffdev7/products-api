namespace products.domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        protected Product() { }

        public static Product Create(string name, decimal price, int stock)
            => new()
            {
                Name = name,
                Price = price,
                Stock = stock,
            };
    }
}
