using System.Collections.Generic;
using System.Linq;

namespace Interview.Api.Domain
{
    public class FolderAnalyzer : IFolderAnalyzer
    {
        public List<Folder> GetHangingFolders(List<Folder> folders)
        {
            List<Folder> hangingFolders = new();
            var folderMap = folders.ToDictionary(k => k.Index, v => v);
            foreach (var folder in folders)
            {
                if (ParentExistIter(folderMap, folder.Index))
                {
                    continue;
                }

                hangingFolders.Add(folder);
            }

            return hangingFolders;
        }

        private bool ParentExistIter(Dictionary<string, Folder> folders, string parentIndex)
        {
            var parentExist = folders.ContainsKey(parentIndex);

            if (!parentExist)
            {
                return false;
            }

            if (!parentIndex.Contains('.'))
            {
                return true;
            }

            var i = parentIndex.LastIndexOf('.');
            if (parentIndex.Length == i + 1)
            {
                return false;
            }
            var nextParentIndex = parentIndex.Substring(0, i);
            return ParentExistIter(folders, nextParentIndex);
        }
    }
}