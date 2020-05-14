using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ghostscript.NET;

namespace PILibrary
{
    public class PDF2Image
    {
        #region model
        #endregion

        #region prop
        #endregion

        #region method
        public PDF2Image()
        {
        }

        public void LoadImage(string InputPDFFile, int PageNumber)
        {

            //string outImageName = @"D:\testGI\AA\Meatslave_Aiko_Iwase.pdf";
            string outImageName = InputPDFFile;
            outImageName = outImageName + "_" + PageNumber.ToString() + "_.jpg";


            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png256);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(290, 290);
            dev.InputFiles.Add(InputPDFFile);
            dev.Pdf.FirstPage = PageNumber;
            dev.Pdf.LastPage = PageNumber;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.OutputPath = @"D:\testGI\AA\" + outImageName;
            dev.Process();

        }
        #endregion
    }
}
