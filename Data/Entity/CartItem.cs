namespace Data.Entity
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int PersonId { get; set; }
        public User? Person { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
