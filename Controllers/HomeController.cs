using System.Diagnostics;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using mvc.Models;
using mvc.Services.Helpers.Interface;

namespace mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUploadHelper _uploadHelper;
    public readonly ICosmosFileHistoryService _cosmosService;
    public HomeController(ILogger<HomeController> logger,
        IUploadHelper uploadHelper,ICosmosFileHistoryService cosmosFileHistoryService)
    {
        _logger = logger;
        _uploadHelper = uploadHelper;
        _cosmosService = cosmosFileHistoryService;
    }
    [Authorize]
    public async Task<IActionResult>Index()
    {
        var FileModel = new FileViewModel();
        var result = await _cosmosService.GetByEmail(User.Claims.ElementAt(4).Value);
        var vm = new IndexViewModel();
        vm.newFile = FileModel;
        vm.Files = result;
        return View(vm) ;

    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveFile(FileViewModel file)
    {
        
        if(file.File == null || file.File.FileName == null)
          return View("Index");


        var historyFile = new HistoryFile();
        historyFile.Id = Guid.NewGuid().ToString();
        historyFile.UserId = User.Claims.ElementAt(4).Value;
        historyFile.DateCreated = DateTime.Now.ToString();
        historyFile.Type = Path.GetExtension(file.File.FileName).ToString().Replace('.',' ');
        var url = _uploadHelper.UploadToAzure(file.File);
        historyFile.Size = file.File.Length.ToString();
        historyFile.Description = "testing";
        historyFile.Url = url;
        var result = await _cosmosService.AddAsync(historyFile);

        return RedirectToRoute(new { action = "Index", controller = "Home", area = "" });
    }

    public async Task<IActionResult>Privacy()
    {
        var sqltest = "Select * from c";
        var result = await _cosmosService.Get(sqltest);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
   
}
