using System;
namespace mvc.Services.Helpers.Interface
{
	public interface IUploadHelper
	{
		string UploadToAzure(IFormFile File);

	}
}

