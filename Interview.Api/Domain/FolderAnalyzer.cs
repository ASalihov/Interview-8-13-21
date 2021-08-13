using System.Collections.Generic;
using System.Linq;

namespace Interview.Api.Domain
{
    public class FolderAnalyzer : IFolderAnalyzer
    {
        private const char separator = '.';

        public List<Folder> GetHangingFolders(List<Folder> folders)
        {
            List<Folder> hangingFolders = new();
            var folderMap = folders.ToDictionary(k => k.Index, v => v);

            foreach (var folder in folders)
            {
                if (!ParentExistIter(folderMap, folder.Index))
                {
                    hangingFolders.Add(folder);
                }
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

            if (IsLastSegment(parentIndex))
            {
                return true;
            }

            var lastSeparatorIndex = parentIndex.LastIndexOf(separator);
            
            if (IsIndexIncomplete(parentIndex, lastSeparatorIndex))
            {
                return false;
            }

            var nextParentIndex = parentIndex.Substring(0, lastSeparatorIndex);
            return ParentExistIter(folders, nextParentIndex);
        }

        private bool IsIndexIncomplete(string index, int lastSeparatorIndex)
        {
            return index.Length == lastSeparatorIndex + 1;
        }

        private bool IsLastSegment(string index)
        {
            return !index.Contains(separator);
        }
    }
}