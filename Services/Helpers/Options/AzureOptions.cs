using System;
namespace mvc.Services.Helpers.Options
{
	public class AzureOptions
	{
		public AzureOptions()
		{
		}
		public string ResourceGroup { get; set; }
		public string Account { get; set; }
		public string Container { get; set; }
		public string ConnectionString { get; set; }
	}
}

