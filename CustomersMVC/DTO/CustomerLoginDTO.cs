namespace CustomersMVC.DTO
{
	public class CustomerLoginDTO
	{
        public string? Username { get; set; }
		public string? Password { get; set; }
		public bool KeepLoggedIn { get; set; }
    }
}
