using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static GILibrary.GroupImageLib;

namespace GILibrary
{
    public static class Sorting
    {
        public static IList<Image2PdfModel> SortGI(this IList<Image2PdfModel> obj)
        {
            string[] arr = new string[obj.Count()];
            var model = new List<Image2PdfModel>();

            for (int i = 0; i < arr.Length; i++)
            {
                var textSplit = obj[i]._imagesPaths.Split('\\');
                arr[i] = textSplit.Last();                
            }

            string path = GetPath(obj[0]._imagesPaths);
            var list = NaturalSort(arr);

            foreach(var newList in list)
            {
                var _fullpath = path + newList;
                var temp = obj.Where(s => s._imagesPaths == _fullpath).ToList();
                model.Add(new Image2PdfModel { _FolderPaths = temp[0]._FolderPaths, _imagesPaths = temp[0]._imagesPaths });
            }

            return model;
        }
        public static List<GISModel> SortGI(this List<GISModel> obj)
        {
            string[] arr = new string[obj.Count()];
            var model = new List<GISModel>();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = obj[i].fullName;
            }

            var list = NaturalSort(arr);

            foreach (var newList in list)
            {
                var temp = obj.Where(s => s.fullName == newList).ToList();
                model.Add(new GISModel { fullName = temp[0].fullName, lootFoldername = temp[0].lootFoldername,subFoldername=temp[0].subFoldername });
            }

            return model;
        }
        private static string GetPath(string _path)
        {
            string[] path = _path.Split('\\');
            string fullPath = string.Empty;
            foreach(var p in path)
            {
                if(!p.Equals(path.Last()))
                    fullPath += p + '\\';
            }

            return fullPath;
        }
        public static IEnumerable<string> NaturalSort(IEnumerable<string> list)
        {
            int maxLen = list.Select(s => s.Length).Max();
            Func<string, char> PaddingChar = s => char.IsDigit(s[0]) ? ' ' : char.MaxValue;

            return list
                    .Select(s =>
                        new
                        {
                            OrgStr = s,
                            SortStr = Regex.Replace(s, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, PaddingChar(m.Value)))
                        })
                    .OrderBy(x => x.SortStr)
                    .Select(x => x.OrgStr);
        }
    }
}
