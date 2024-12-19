namespace Data.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
        public List<CartItem> CartItem { get; set; } = new List<CartItem>();
    }
}
