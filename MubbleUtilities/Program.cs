using System;
using System.Text;
using System.Collections.Specialized;
using NUnit.Framework;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Mubble.Models;
using System.Text.RegularExpressions;

namespace MubbleUtilities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            //ConnectionStringsSection section = (ConnectionStringsSection)
            //        ConfigurationManager.GetSection("connectionStrings");
            //string cnString = section.ConnectionStrings["mubbleDB"].ConnectionString;
            //Mubble.MubbleObject.ReadDb = Mubble.MubbleObject.WriteDb = cnString;
            //#region Category mapping
            //Dictionary<string, Content> map = new Dictionary<string, Content>();
            //map.Add("Acoustics_Construction", new Content("/hometheaterDIY/constructionwiring"));
            //map.Add("Acoustics_General", new Content("/hometheaterDIY/roumacousticsgeneral"));
            //map.Add("Acoustics_Projects", new Content("/hometheaterDIY/DIY"));
            //map.Add("Acoustics_Treatment", new Content("/hometheaterDIY/acoustics"));
            //map.Add("AudioPrinciples_Amps", new Content("/hometheatereducation/amplifiertechnology"));
            //map.Add("AudioPrinciples_Cables", new Content("/hometheatereducation/cablesinterconnects"));
            //map.Add("AudioPrinciples_Cables_Spons", new Content("/hometheatereducation/cablesinterconnects"));
            //map.Add("AudioPrinciples_Speakers", new Content("/hometheatereducation/loudspeakerbasics"));
            //map.Add("BuyingGuide_Cables", new Content("/buying-guides/how-to-shop"));
            //map.Add("BuyingGuide_Furniture", new Content("/buying-guides/how-to-shop"));
            //map.Add("BuyingGuide_Hardware", new Content("/buying-guides/how-to-shop"));
            //map.Add("BuyingGuide_Speakers", new Content("/buying-guides/how-to-shop"));
            //map.Add("CEDIA_Amps", new Content("/newspresseditorials/tradeshows/CEDIA-show/cedia-amplifiers"));
            //map.Add("CEDIA_Displays", new Content("/newspresseditorials/tradeshows/CEDIA-show/HDTV-televisions"));
            //map.Add("CEDIA_Furniture", new Content("/newspresseditorials/tradeshows/CEDIA-show/home-theater-furniture"));
            //map.Add("CEDIA_MiscHardware", new Content("/newspresseditorials/tradeshows/CEDIA-show/misc-show-coverage"));
            //map.Add("CEDIA_Processors", new Content("/newspresseditorials/tradeshows/CEDIA-show/receivers-preprocessors"));
            //map.Add("CEDIA_Speakers", new Content("/newspresseditorials/tradeshows/CEDIA-show/loudspeakers"));
            //map.Add("CEDIA_Tech", new Content("/newspresseditorials/tradeshows/CEDIA-show/specs-formats-technology"));
            //map.Add("CEDIA_Transports", new Content("/newspresseditorials/tradeshows/CEDIA-show/cd-dvd-BD-transports"));
            //map.Add("CESamps", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/cedia-amplifiers"));
            //map.Add("CESdisplays", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/HDTV-televisions"));
            //map.Add("CESfurniture", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/home-theater-furniture"));
            //map.Add("CESmisc", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/misc-show-coverage"));
            //map.Add("CESprocessors", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/receivers-preprocessors"));
            //map.Add("CESspeakers", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/loudspeakers"));
            //map.Add("CEStechnology", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/specs-formats-technology"));
            //map.Add("CEStransports", new Content("/newspresseditorials/tradeshows/consumer-electronics-show/cd-dvd-BD-transports"));
            //map.Add("Editorials", new Content("/newspresseditorials/Editorials by Audioholics"));
            //map.Add("FAQs", new Content("/hometheatereducation/faqs"));
            //map.Add("PressReleases", new Content("/newspresseditorials/pr"));
            //map.Add("Setup_BassManagement", new Content("/hometheaterDIY/base-management"));
            //map.Add("Setup_Displays", new Content("/hometheatereducation/Connecting Your System"));
            //map.Add("Setup_Hardware", new Content("/hometheatereducation/Connecting Your System"));
            //map.Add("Setup_Speakers", new Content("/hometheatereducation/Connecting Your System"));
            //map.Add("Setup_TestGear", new Content("/test-measurement-equipment"));
            //map.Add("Software_CDother", new Content("/dvdmoviereviews/cdreviews"));
            //map.Add("Software_DVDAudioSACD", new Content("/dvdmoviereviews/sacddvdaudioreviews"));
            //map.Add("Software_DVDVideo", new Content("/dvdmoviereviews/dvdmoviereviews"));
            //map.Add("SpecsFormats_AVFormats", new Content("/hometheatereducation/audioformatscompression"));
            //map.Add("SpecsFormats_Compression", new Content("/hometheatereducation/audioformatscompression"));
            //map.Add("SpecsFormats_SurroundDefs", new Content("/hometheatereducation/surround-sound-formats"));
            //map.Add("ehx2004", new Content("/newspresseditorials/tradeshows/EHX-expo"));
            //map.Add("ehx2005", new Content("/newspresseditorials/tradeshows/EHX-expo"));
            //#endregion
            ///*
            // * 0 - Title
            // * 1 - Author
            // * 2 - Body
            // * 3 - Key
            // * 4 - Timestamp
            // * 5 - Category
            // * 6 - Filename
            // * 7 - Read link *OPTIONAL*
            // */
            ///*
            // * <PR> - page break
            // */

            //using(TextReader rdr = (TextReader)File.OpenText(@"c:\Documents and Settings\Kurt\Desktop\newsdat.txt"))
            //{
            //    Dictionary<string, int> categories = new Dictionary<string, int>();
            //    int onePagers = 0, multiplePagers = 0, newsPosts = 0;
            //    string line = null;
            //    List<string> authors = new List<string>();
            //    while ((line = rdr.ReadLine()) != null)
            //    {
            //        string[] parts = line.Split(new string[] { "``x" }, StringSplitOptions.None);
            //        int timestamp = 0;
            //        if (parts.Length < 7) continue;
            //        int.TryParse(parts[4], out timestamp);
            //        if (parts.Length >= 7 && parts[5] == "(default)")
            //        {
            //            //Console.WriteLine("{0} wrote at {1},{2}", parts[1], unixEpoch.AddSeconds(timestamp), timestamp);
            //            Guid contentId = new Guid("563e1096-9293-467a-95ec-88bab059edf2");
            //            Mubble.Post p = new Post();
            //            Regex regex = new Regex("(\\[.*?\\<A\\shref=\"(?<MoreLink>[^\"]*)\".*?\\].*?\\[.*?\\<A\\shref=\"(?<DiscussionLink>[^\"]*)\".*?\\])", 
            //                RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
            //            Match match = regex.Match(parts[2]);
            //            string moreUrl = parts.Length >= 8 ? parts[7] : null, discussUrl = null;
            //            if (match.Success)
            //            {
            //                moreUrl = match.Groups["MoreLink"].Value;
            //                discussUrl = match.Groups["DiscussionLink"].Value;
            //            }
            //            if (!p.Load(contentId, parts[4]))
            //            {
            //                newsPosts++;
            //                p.ContentId = contentId;
            //                p.Title = parts[0];
            //                p.Slug = parts[4];
            //                p.UserName = parts[1];
            //                p.Body = parts[2];
            //                p.PostDate = unixEpoch.AddSeconds(timestamp);
            //                p.Status = PublishStatus.Published;
            //                p.MoreUrl = moreUrl;
            //                p.DiscussionUrl = discussUrl;
            //                p.Save();
            //            }
            //            else if (p.MoreUrl != moreUrl || p.DiscussionUrl != discussUrl)
            //            {
            //                p.DiscussionUrl = discussUrl;
            //                p.MoreUrl = moreUrl;
            //                p.Save();
            //            }
            //        }
            //        /*
            //         * 0 - Title
            //         * 1 - Author
            //         * 2 - Body
            //         * 3 - Key
            //         * 4 - Timestamp
            //         * 5 - Category
            //         * 6 - Filename
            //         * 7 - Read link *OPTIONAL*
            //         */
            //        else if (parts.Length >= 7 && parts[5] != "(default)" && false)
            //        {
            //            if (map.ContainsKey(parts[5]) && map[parts[5]].Id != Guid.Empty)
            //            {
            //                string oldPath = string.Format("{0}/{1}", map[parts[5]].Path, parts[6]);
            //                FeaturedContent fc = new FeaturedContent();
            //                if (fc.Load("Path", oldPath)) continue;
            //                fc.Name = parts[0];
            //                fc.ModuleId = new Guid("d95340c7-2310-49a6-8e8e-f0fb203331c3");
            //                fc.ModuleControl = "Article.aspx";
            //                fc.TemplateId = new Guid("602b9110-855e-4888-a721-2d61804fd981");
            //                fc.TemplateControl = "Default.master";
            //                //Mubble.User user = new Mubble.User(
            //                //fc.Users.Add(new Mubble.User(parts[1]));
            //                fc.ParentId = map[parts[5]].Id;
            //                fc.FileName = parts[6];
            //                fc.PublishDate = unixEpoch.AddSeconds(timestamp);
            //                fc.ModifyDate = DateTime.Now;
            //                fc.Status = PublishStatus.Published;
            //                fc.Save();

            //                string[] pages = parts[2].Split(new string[] { "<PR>" }, StringSplitOptions.RemoveEmptyEntries);

            //                int i = 1;
            //                foreach (string p in pages)
            //                {
            //                    FeaturedContentPage page = new FeaturedContentPage();
            //                    page.Name = "";
            //                    page.Body = p;
            //                    page.ContentId = fc.Id;
            //                    page.PageNumber = i;
            //                    page.Save();
            //                    i++;
            //                }
                            
            //                if (pages.Length > 1)
            //                {
            //                    multiplePagers++;
            //                    Console.WriteLine("{0} wrote a {1} page article at,{2}",
            //                        parts[1],
            //                        pages.Length,
            //                        unixEpoch.AddSeconds(timestamp)
            //                        );
            //                }
            //                else
            //                {
            //                    onePagers++;
            //                }
            //            }
            //        }
            //    }
            //    Console.WriteLine("There are {0} single page articles and {1} multipage articles.", onePagers, multiplePagers);
            //}
            //Console.ReadLine();
        }
    }
}
