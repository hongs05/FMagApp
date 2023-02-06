using System;
namespace mvc.Models
{
	public class UploadFileModel
	{
		public UploadFileModel()
		{
		}
		public string Details { get; set; }
		public IFormFile File { get; set; }
	}
}

