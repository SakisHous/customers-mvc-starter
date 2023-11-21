namespace CustomersMVC.DTO
{
	public class ProductReadOnlyDTO
	{
        public int Id { get; set; }
        public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Price { get; set; }
		public int Quantity { get; set; }
	}
}
