using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace GILibrary
{
     public class GroupImageLib
    {
        #region Model
        public class GIModel
        {
            public string foldername { get; set; }
            public string fullName { get; set; }
        }
        public class Image2PdfModel
        {
            public string _imagesPaths;
            public string _FolderPaths;
        }
        public class PathsModel
        {
            public string _Paths;
        }
        public class GroupImageLibModel
        {
            public string _dir;
            public string _to;
            public string _from;
            public string[] termsList;
            public string Folder;
            public string[] arrFolder;
            public int index;
            public string FilePath;
            public string listPaths;
        }
        #endregion

        #region Prop
        public List<GIModel> _GIModel;
        public GroupImageLibModel GILModel;
        public string ErrorMassage { get; set; }
        private readonly string[] _SexukaCase = { "[LR][SEXUKA.COM]", "[SEXUKA.COM]", "Sexuka.com", "[BT][SEXUKA.COM]" };
        private readonly int _case = 2;
        //private string _dir;
        //private string _to;
        //private string _from;
        //private string[] termsList;
        //private string Folder;
        //private string[] arrFolder;
        //private int index;
        public Rectangle PageSize { get; set; }
        public float Margin { get; set; }
        public readonly IList<Image2PdfModel> I2PModel;
        public readonly IList<PathsModel> _Path;
        //public string FilePath;
        //public string listPaths;
        #endregion

        #region Method
        //constructor
        public GroupImageLib()
        {
            _GIModel = new List<GIModel>();
            GILModel = new GroupImageLibModel();
            I2PModel = new List<Image2PdfModel>();
            _Path = new List<PathsModel>();
            PageSize = iTextSharp.text.PageSize.A4;
            Margin = 15f;
            ErrorMassage = "";
        }

        //method
        public void GenerateFolderPdf(IList<Image2PdfModel> model)
        {
            string _path;
            string exists;
            var folder = model.GroupBy(item => item._FolderPaths)
                                 .Select(group => new { _pdfPath = group.Key, _image = group.ToList() })
                                 .ToList();

            GILModel.listPaths = "ListPaths \r\n";
            foreach (var pdfPath in folder)
            {
                var doc = new Document();
                doc.SetMargins(Margin, Margin, Margin, Margin);
                _path = GILModel.FilePath + @"\" + pdfPath._pdfPath + ".pdf";

                GILModel.listPaths += "'" + pdfPath._pdfPath + "\r\n";
                exists = pdfPath._pdfPath + ".pdf";

                if ((from p in _Path where p._Paths.Equals(exists) select p).Any())
                    goto NextPaths;

                using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfWriter.GetInstance(doc, stream);

                    doc.Open();

                    foreach (var imagePath in pdfPath._image)
                    {
                        using (var imageStream = new FileStream(imagePath._imagesPaths, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            var image = Image.GetInstance(imageStream);

                            #region Checks orientation

                            doc.SetPageSize(image.Width > image.Height
                                      ? PageSize.Rotate()
                                      : PageSize);

                            #endregion Checks orientation

                            doc.NewPage();

                            #region Configures image

                            image.ScaleToFit(new Rectangle(0, 0, doc.PageSize.Width - (doc.RightMargin + doc.LeftMargin + 1), doc.PageSize.Height - (doc.BottomMargin + doc.TopMargin + 1)));
                            image.Alignment = Image.ALIGN_CENTER;

                            #endregion Configures image

                            #region Creates elements

                            var table = new PdfPTable(1)
                            {
                                WidthPercentage = 100
                            };

                            var cell = new PdfPCell
                            {
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                MinimumHeight = doc.PageSize.Height - (doc.BottomMargin + doc.TopMargin),
                                Border = 0,
                                BorderWidth = 0,
                                Padding = 0,
                                Indent = 0
                            };

                            cell.AddElement(image);

                            table.AddCell(cell);

                            #endregion Creates elements

                            doc.Add(table);
                        }
                    }

                    doc.Close();
                }
                NextPaths: continue;
            }

        }
        public void CheckPaths(string directory)
        {
            string sourceDirectory = directory;
            try
            {
                var allFiles
                  = Directory.EnumerateFiles(sourceDirectory, "*.pdf", SearchOption.AllDirectories);

                foreach (string currentFile in allFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    var item = fileName.Split('_');

                    _Path.Add(new PathsModel { _Paths = item[0] });
                }
            }
            catch (Exception e)
            {
                ErrorMassage = e.Message;
            }
        }
        public void AddDirectory(string directory, string searchPatterns = "*.jpg")
        {
            string sourceDirectory = directory;
            try
            {
                var allFiles
                  = Directory.EnumerateFiles(sourceDirectory, searchPatterns, SearchOption.AllDirectories);

                foreach (string currentFile in allFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    var item = fileName.Split('\\');

                    I2PModel.Add(new Image2PdfModel { _FolderPaths = item[0], _imagesPaths = currentFile });
                }
            }
            catch (Exception e)
            {
                ErrorMassage = e.Message;
            }
        }
        public void FindDirectory(string directory, string searchPatterns = "*.jpg")
        {
            try
            {
                var allFiles = Directory.EnumerateFiles(directory, searchPatterns, SearchOption.AllDirectories);
                foreach (string currentFile in allFiles)
                {
                    //get file name in directory
                    string fileName = currentFile.Substring(directory.Length + 1);

                    //process file
                    GILModel.termsList = fileName.Split(new[] { "__" }, StringSplitOptions.None);

                    GILModel.Folder = GILModel.termsList.Count() == _case ? NumberCase(GILModel.termsList) : SexukaCase(GILModel.termsList);
                    _GIModel.Add(new GIModel { fullName = fileName, foldername = GILModel.Folder });
                }
            }
            catch (Exception ex)
            {
                ErrorMassage = ex.Message;
            }
        }
        private string DoujinTHCase(string[] list)
        {
            GILModel.index = list.Count() - 1;
            GILModel.arrFolder = list[GILModel.index].Split(new[] { "_" }, StringSplitOptions.None);
            return GILModel.arrFolder[0];
        }
        private string NumberCase(string[] list)
        {
            Regex regex = new Regex(@"^[0-9]*$");

            foreach(var item in list)
            {
                if (regex.IsMatch(item))
                {
                    return item;
                }
            }
            return DoujinTHCase(list);
        }
        private string NMCase(string[] list)
        {
            if(list.Count() == 3)
            {
                GILModel.index = list.Count() - 2;
            }
            else
            {
                GILModel.index = list.Count() - 3;
            }
            return list[GILModel.index];
        }
        private string SexukaCase(string[] list)
        {
            GILModel.index = list.Count() - 2;

            foreach (var file in _SexukaCase)
            {
                GILModel.arrFolder = (from _folder in list
                             where _folder.Contains(file)
                             select _folder).ToArray();
                if (GILModel.arrFolder.Count() > 0)
                {
                    GILModel.arrFolder = list[GILModel.index].Split(new[] { GILModel.arrFolder.First() }, StringSplitOptions.None);
                    return DoujinTHCase(GILModel.arrFolder);
                }
            }
            return NMCase(list);
        }
        public void Move(string directoryFrom, string directoryTo)
        {
            try
            {
                foreach (var item in _GIModel)
                {
                    GILModel._dir = directoryTo + "\\" + item.foldername;
                    GILModel._to = GILModel._dir + "\\" + item.fullName;
                    GILModel._from = directoryFrom + "\\" + item.fullName;

                    if (!Directory.Exists(GILModel._dir))
                    {
                        Directory.CreateDirectory(GILModel._dir);
                        File.Move(GILModel._from, GILModel._to);
                    }
                    else
                    {
                        File.Move(GILModel._from, GILModel._to);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMassage = ex.Message;
            }
        }
        #endregion
    }
}
