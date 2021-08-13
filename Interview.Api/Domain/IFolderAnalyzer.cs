using System.Collections.Generic;

namespace WebApplication1.Domain
{
    public interface IFolderAnalyzer
    {
        List<Folder> GetHangingFolders(List<Folder> folders);
    }
}