using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GILibrary;

namespace WFA_GroupImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new List<Model>();
            string sourceDirectory = @"E:\WindownFormApplication\WFA-SearchAndMove\WFA-SearchAndMove\bin\Debug\test\test";
            string sourceDirectoryto = @"E:\WindownFormApplication\WFA-SearchAndMove\WFA-SearchAndMove\bin\Debug\test\test\testmove";
            try
            {
                string[] searchPatterns = { "*.jpg", "*.txt"};
               var aaa= searchPatterns.AsParallel()
             .SelectMany(searchPattern =>
                    Directory.EnumerateFiles(sourceDirectory, searchPattern, SearchOption.TopDirectoryOnly));
                //ตรวจ Length
                var allFiles
                  = Directory.EnumerateFiles(sourceDirectory, "*.jpg *.txt", SearchOption.AllDirectories);
                foreach (string currentFile in aaa)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    model.Add(new Model { name = fileName });
                }
            }
            catch (Exception eqq)
            {
                Console.WriteLine(eqq.Message);
            }

            //work
            string[] arr = { "DOUJIN-TH.COM", "Doujin-TH.com", "HENTAITHAI.COM", "HENTAITHAI.COM__S-Second__91.jpg" };
            var query1 = (from p in model
                          where p.name.Contains(arr[0])
                          select p).ToList();
            //var query2 = (from p in model
            //              where p.name.Contains(arr[1])
            //              select p).ToList();
            //

            string[] arr2 = { "Doujin-TH.com__000634_0001.jpg","[BT][HENTAITHAI.COM]__006018__002(MzY).jpg",
                                "[BT][HENTAITHAI.COM]__008088__022(OTM).jpg",
                                "[BT][SEXUKA.COM]__1273169_030__3432297(wNz).jpg",
                                "[BT][SEXUKA.COM]__493743_050__124660(xNT).jpg",
                                "[BT][SEXUKA.COM]__771738_004__26093(1Mg).jpg",
                                "[BT][SEXUKA.COM]__964834_004__2047166(zNg).jpg",
                                "[HENTAITHAI.COM]__004710__003(MzA).jpg",
                                "[LR][HENTAITHAI.COM]__005057__016(MTI).jpg",
                                "[LR][SEXUKA.COM]__109679_019__3431241(wMQ).jpg",
                                "[LR][SEXUKA.COM]__1280191_026__3430544(5Nj).jpg",
                                "[SEXUKA.COM]__1228275_001__3426057(3NQ).jpg",
                                "[SEXUKA.COM]__395374_003__133753(yMg).jpg",
                                "Doujin-TH.com__Akujo_Kousatsu_1__034.jpg",
                                "Doujin-TH.com__Amazing_EIGHTH_WONDER__0001__GLZ.jpg",
                                "DOUJIN-TH.COM__Aneman_10__01.jpg",
                                "Doujin-TH.com__Ayamachi_1__01.jpg",
                                "Doujin-TH.com__Ayamachi_5__080__FuX.jpg",
                                "Doujin-TH.com__Bitch_with_the_Beast_Ch-1__008.jpg",
                                "Doujin-TH.com__Bitch_with_the_Beast_Ch-1__credit.jpg",
                                "Doujin-TH.com__Boku-wa-Anata__001_001.jpg",
                                "DOUJIN-TH.COM__Brawling_Go_75__12.jpg",
                                "Doujin-TH.com__Chairman-s_Introduction__07.jpg",
                                "Doujin-TH.com__Chairman-s_Introduction__credit.jpg",
                                "Doujin-TH.com__Confession_Heat_Up__01.jpg",
                                "Doujin-TH.com__F.L.O.W.E.R_02__25.jpg",
                                "Doujin-TH.com__F.L.O.W.E.R_03__05.jpg",
                                "Doujin-TH.com__Furusato__01__IJx.jpg",
                                "DOUJIN-TH.COM__Haijin__01.jpg",
                                "Doujin-TH.com__Himitsu_5__079.jpg",
                                "Doujin-TH.com__Hitozuma_Miyuki-san_ni_Otto_Kounin_de_Mainichi_Tanetsuke_Sex__39.jpg",
                                "Doujin-TH.com__Magetsukan_Kitan_11__006.jpg",
                                "DOUJIN-TH.COM__Mane_Seku__07.jpg",
                                "DOUJIN-TH.COM__Mane_Seku__21_2.jpg",
                                "Doujin-TH.com__Megumi__06.jpg",
                                "Doujin-TH.com__Midara_2__12__lrE.jpg",
                                "Doujin-TH.com__Moshimo_Hatsukoi_ga_Kanatte_Itara_Ch-03__49.jpg",
                                "Doujin-TH.com__Nineteen_0__26.jpg",
                                "Doujin-TH.com__Peak_of_Summer__04.jpg",
                                "Doujin-TH.com__Sei_Myurisu_Gakuin_e_Youkoso_2__001.jpg",
                                "Doujin-TH.com__Seika__02.jpg",
                                "Doujin-TH.com__Shocking_Pink_1__17.jpg",
                                "Doujin-TH.com__Soushisouai_Note_Nisatsume_Ch_8__167.jpg",
                                "Doujin-TH.com__Soushisouai_Note_Nisatsume_Ch_8__credit.jpg",
                                "Doujin-TH.com__Student_Motivation__11.jpg",
                                "Doujin-TH.com__Sweet_Hearts_Lesson_4__21.jpg",
                                "Doujin-TH.com__Sweet_Hearts_Lesson_4__credit.jpg",
                                "Doujin-TH.com__Teisou_Gyakuten_Sekai_Shoujo_Kari_5__01__h5B.jpg",
                                "Doujin-TH.com__Tomodachi_Kareshi__22__AE0.jpg",
                                "Doujin-TH.com__TS-ko_1__03__Ysj.jpg",
                                "Doujin-TH.com__Uiuishii_Futari__10__VrL.jpg",
                                "Doujin-TH.com__Ushiro__302__YJF.jpg",
                                "Doujin-TH.com__Welcome_to_Tokoharusou_4.5__credit.jpg",
                                "Doujin-TH.com__Welcome_to_Tokoharusou_5__20.jpg",
                                "HENTAITHAI.COM__000527__005.jpg",
                                "HENTAITHAI.COM__004411__001.jpg",
                                "HENTAITHAI.COM__Brawling-Go_86__004.jpg",
                                "HENTAITHAI.COM__Friend-s_Mother_2__08.jpg",
                                "HENTAITHAI.COM__Kimi_ga_Haramu_8__14.jpg",
                                "HENTAITHAI.COM__Office_Love_Scramble_4__36.jpg",
                                "HENTAITHAI.COM__Osananajimi__09.jpg",
                                "HENTAITHAI.COM__S-Second__92.jpg",
                                "HENTAITHAI.COM__Suna_no_Kusari_15__018.jpg",
                                "HENTAITHAI.COM__Tachibana__098.jpg",
                                "HENTAITHAI.COM__Take_Me_to_Eromanga_Island__09.jpg",
                                "Sexuka.com__1021879_122__2440744.jpg",
                                "Sexuka.com__1237768_009__3427093.jpg",
                                "Sexuka.com__353318_006__138928.jpg",
                                "Sexuka.com__660564_002__79228.jpg",
                                "Sexuka.com__766069_006__33221.jpg",
                                "Sexuka.com__915050_002__1733218.jpg" };
            var sl = "HENTAITHAI.COM__S-Second__91.jpg";
            var after = sl.Split(new[] { "__" }, StringSplitOptions.None);
            var _model = new List<Model2>();
            string[] __model;
            int i = 0;
            string[] termsList;

            List<string> termsList1 = new List<string>();
            List<string> List12 = new List<string>();

            //get file
            foreach (var _file in arr)
            {             
                if(termsList1.Count() > 0)
                {
                    __model = (from p in arr2
                                   where p.Contains(_file)
                                   select p).ToArray();
                    foreach(var ii in __model)
                    {
                        termsList1.Add(ii);
                    }
                }
                else
                {
                    termsList1 = (from p in arr2
                                  where p.Contains(_file)
                                  select p).ToList();
                }
            }

            //set file
            foreach (var item in arr2)
            {

                termsList = item.Split(new[] { "__" }, StringSplitOptions.None);
                _model.Add(new Model2 { name = item, arrfile = termsList });
            }

            //find file
            foreach (var item in _model)
            {
                if(item.arrfile.Count() == 2)
                {
                    //
                    termsList = item.arrfile[1].Split(new[] { "_" }, StringSplitOptions.None);
                    string folder = termsList[0];
                }
            }

            var gi = new GroupImageLib();

            //gi.FindDirectory(sourceDirectory);
            //gi.Move(sourceDirectory, sourceDirectoryto);
        }

        
    }

    public class Model
    {
        public string name { get; set; }
    }
    public class Model2
    {
        public string[] arrfile { get; set; }
        public string name { get; set; }
    }
}
