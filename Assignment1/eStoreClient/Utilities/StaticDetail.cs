namespace eStoreClient.Utilities
{
	public class StaticDetail
	{
		public static string eStoreAPIBase { get; set; } = "http://localhost:7000";
		public enum APIType
		{
			GET,
			POST,
			PUT,
			DELETE
		}


		public const string TOKEN_COOKIE = "JWTToken";
	}
}
