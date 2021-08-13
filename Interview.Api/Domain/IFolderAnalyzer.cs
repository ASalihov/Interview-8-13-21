using System.Collections.Generic;

namespace Interview.Api.Domain
{
    public interface IFolderAnalyzer
    {
        List<Folder> GetHangingFolders(List<Folder> folders);
    }
}