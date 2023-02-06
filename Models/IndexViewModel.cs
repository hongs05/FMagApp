using System;
namespace mvc.Models
{
	public class IndexViewModel
	{
		public IndexViewModel()
		{
			Files = new List<HistoryFile>();
		}
		public List<HistoryFile> Files { get; set; }
		public FileViewModel newFile { get; set; }
	}
}

