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
        public class GISModel
        {
            public string lootFoldername { get; set; }
            public string subFoldername { get; set; }
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
        public List<GISModel> _GISModel;
        public List<ResultPathModel> ResultPaths;
        public GroupImageLibModel GILModel;
        public string ErrorMassage { get; set; }
        private readonly string[] _SexukaCase = { "[LR][SEXUKA.COM]", "[SEXUKA.COM]", "Sexuka.com", "[BT][SEXUKA.COM]" };
        private readonly int _case = 2;
        public Rectangle PageSize { get; set; }
        public float Margin { get; set; }
        public readonly IList<Image2PdfModel> I2PModel;
        public readonly IList<PathsModel> _Path;
        #endregion

        #region Method
        //constructor
        public GroupImageLib()
        {
            _GIModel = new List<GIModel>();
            _GISModel = new List<GISModel>();
            GILModel = new GroupImageLibModel();
            I2PModel = new List<Image2PdfModel>();
            ResultPaths = new List<ResultPathModel>();
            _Path = new List<PathsModel>();
            PageSize = iTextSharp.text.PageSize.A4;
            Margin = 15f;
            ErrorMassage = "";            
        }

        //method
        public void GenerateSubFolderPdf(IList<Image2PdfModel> model)
        {
            #region init
            string _path;
            string exists;
            #endregion

            foreach(var lootFolder in model)
            {
                var subFolder = _GISModel.Where(w => w.lootFoldername == lootFolder._imagesPaths).ToList();
                var folder = subFolder.GroupBy(item => item.subFoldername)
                                .Select(group => new { _pdfPath = group.Key, _image = group.ToList() })
                                .ToList();

                foreach (var pdfPath in folder)
                {
                    #region pdf exist
                    var doc = new Document();
                    doc.SetMargins(Margin, Margin, Margin, Margin);
                    _path = GILModel.FilePath + @"\" + lootFolder._imagesPaths + "\\" + pdfPath._pdfPath + ".pdf";

                    SaveResultSinglePath(pdfPath._pdfPath);
                    exists = pdfPath._pdfPath + ".pdf";

                    if ((from p in _Path where p._Paths.Equals(exists) select p).Any())
                        goto NextPaths;
                    #endregion

                    using (var stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfWriter.GetInstance(doc, stream);

                        doc.Open();

                        foreach (var imagePath in pdfPath._image)
                        {                            
                            using (var imageStream = new FileStream(GeneratePath(imagePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
        }
        private string GeneratePath(GISModel _path)
        {
            return GILModel.FilePath + "\\" + _path.lootFoldername + "\\" + _path.subFoldername + "\\" + _path.fullName;
        }
        private void SaveResultSinglePath(string _path)
        {
            try
            {
                ResultPaths.Add(new ResultPathModel
                {
                    ImageCode = GILModel.FilePath.Split('\\').Last(),
                    ImageName = _path
                });
            }
            catch
            {
                ResultPaths.Add(new ResultPathModel
                {
                    ImageName = _path,
                    ImageCode = _path
                });
            }
        }
        public void AddLootDirectory(string directory, string searchPatterns = "*.jpg")
        {
            string sourceDirectory = directory;
            try
            {
                var allFiles
                  = Directory.EnumerateFiles(sourceDirectory, searchPatterns, SearchOption.AllDirectories);

                var lootPath = sourceDirectory.Split('\\');
                foreach (string currentFile in allFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    var item = fileName.Split('\\');

                    //_FolderPaths = loot folder
                    //_imagesPaths = sub folder
                    if (I2PModel.Where(i => i._imagesPaths == item[0]).Count() == 0)
                        I2PModel.Add(new Image2PdfModel { _FolderPaths = lootPath.Last(), _imagesPaths = item[0] });
                }
            }
            catch (Exception e)
            {
                ErrorMassage = "Cann't Generate Folder Pdf. Please Re-Check File Name.";
            }
        }
        public void FindSubDirectory(string directory, string searchPatterns = "*.jpg")
        {
            try
            {
                foreach (var subFolder in I2PModel)
                {
                    var allFilesINsubFolder = Directory.EnumerateFiles(directory + '\\' + subFolder._imagesPaths,
                        searchPatterns, SearchOption.AllDirectories);

                    foreach (string currentFile in allFilesINsubFolder)
                    {
                        //get file name in directory
                        string fileName = currentFile.Substring(directory.Length + 1);

                        var subFiles = fileName.Split('\\');

                        //process file
                        GILModel.termsList = subFiles.Last().Split(new[] { "__" }, StringSplitOptions.None);

                        GILModel.Folder = GILModel.termsList.Count() == _case ? NumberCase(GILModel.termsList) : SexukaCase(GILModel.termsList);
                        _GISModel.Add(new GISModel { lootFoldername = subFiles.First(), subFoldername = GILModel.Folder,fullName = subFiles .Last()});
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMassage = "Cann't Move. Please Re-Check File Name.";
            }
        }
        public void MoveSub(string directoryFrom, string directoryTo)
        {
            try
            {
                var GIModel = _GISModel.GroupBy(item => item.lootFoldername)
                                .Select(group => new { lootFoldername = group.Key, SubFolder = group.ToList() })
                                .ToList();
                foreach (var item in GIModel)
                {
                    foreach (var itemSub in item.SubFolder)
                    {
                        GILModel._dir = directoryTo + "\\" + itemSub.lootFoldername + "\\" + itemSub.subFoldername;
                        GILModel._to = GILModel._dir +"\\" + itemSub.fullName;
                        GILModel._from = directoryFrom +"\\"+ itemSub.lootFoldername + "\\"+ itemSub.fullName;

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
            }
            catch (Exception ex)
            {
                ErrorMassage = "Cann't Move or Folder's Null. Please Re-Check File Name.";
            }
        }

        public void GenerateFolderPdf(IList<Image2PdfModel> model, bool sort = true)
        {
            #region init
            string _path;
            string exists;
            var folder = model.GroupBy(item => item._FolderPaths)
                                 .Select(group => new { _pdfPath = group.Key, _image = group.ToList() })
                                 .ToList();

            GILModel.listPaths = "ListPaths \r\n";
            #endregion

            foreach (var pdfPath in folder)
            {
                #region pdf exist
                var doc = new Document();
                doc.SetMargins(Margin, Margin, Margin, Margin);
                _path = GILModel.FilePath + @"\" + pdfPath._pdfPath + ".pdf";

                GILModel.listPaths += "'" + pdfPath._pdfPath + "\r\n";
                exists = pdfPath._pdfPath + ".pdf";

                if ((from p in _Path where p._Paths.Equals(exists) select p).Any())
                    goto NextPaths;
                #endregion              

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
        public void GenerateFolderPdf(IList<Image2PdfModel> model)
        {
            #region init
            string _path;
            string exists;
            var folder = model.GroupBy(item => item._FolderPaths)
                                 .Select(group => new { _pdfPath = group.Key, _image = group.ToList() })
                                 .ToList();

            GILModel.listPaths = "ListPaths \r\n";
            #endregion

            foreach (var pdfPath in folder)
            {
                #region pdf exist
                var doc = new Document();
                doc.SetMargins(Margin, Margin, Margin, Margin);
                _path = GILModel.FilePath + @"\" + pdfPath._pdfPath + ".pdf";

                SaveResultSinglePath(pdfPath._pdfPath);
                exists = pdfPath._pdfPath + ".pdf";

                if ((from p in _Path where p._Paths.Equals(exists) select p).Any())
                    goto NextPaths;
                #endregion              

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
                ErrorMassage = "Cann't Generate Folder Pdf. Please Re-Check File Name.";
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
                ErrorMassage = "Cann't Move. Please Re-Check File Name.";
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
        private string OtherCase(string[] list)
        {
            Regex regex = new Regex(@"[_]");
            if (regex.IsMatch(list[0]))
            {
                return DoujinTHCase(list);
            }
            else
            {
                list[0] = "GroupImage_";
                return DoujinTHCase(list);
            }
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

            if (GILModel.index < 0)
            {
                return OtherCase(list);
            }

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
                ErrorMassage = "Cann't Move. Please Re-Check File Name.";
            }
        }
        #endregion
    }
}
