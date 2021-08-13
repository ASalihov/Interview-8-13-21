using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Domain;

namespace Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IFolderAnalyzer folderAnalyzer;

        public FoldersController(IFolderAnalyzer folderAnalyzer)
        {
            this.folderAnalyzer = folderAnalyzer;
        }

        [HttpPost]
        public List<Folder> DetermineHangingFolders([FromBody] List<Folder> folders)
        {
            if (!folders.Any())
            {
                return new List<Folder>();
            }

            return folderAnalyzer.GetHangingFolders(folders);
        }
    }
}