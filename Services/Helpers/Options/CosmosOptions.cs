using System;
namespace mvc.Services.Helpers.Options
{
	public class CosmosOptions
    {
	
		public CosmosOptions()
		{
		}
		public string URL { get; set; }
		public string PrimaryKey { get; set; }
		public string DatabaseName { get; set; }
		public string ContainerName { get; set; }
	}
}

