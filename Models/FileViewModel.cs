using System;
namespace mvc.Models
{
	public class FileViewModel
	{
		public FileViewModel()
		{

		}
		public int Id { get; set; }
		public string Name { get; set; }
		public float sizeOnKb { get; set; }
		public DateTime DateCreated { get; set; }
		public IFormFile File { get; set; }
	}
}

