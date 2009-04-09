using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Mubble.Models;
using System.Diagnostics;



namespace MubbleUtilities.Tests
{
    [TestFixture]
    [Category("Middle Tier")]
    public class DataObjects
    {
        [Test]
        public void PostCreation()
        {
            Post p = new Post();
            p.ControllerID = new Guid("51799860-8138-43b4-9584-183af29503e4");
            p.Slug = p.Title = "Test Post";
            p.Body = "This is long and eloquent";
            p.ID = Guid.Empty;
            p.PublishDate = DateTime.Now;
            p.UserName = "kurt";
            p.Status = PublishStatus.Published;

            p.Save();

            Assert.AreNotEqual(p.ID, Guid.Empty, "The new ID was not returned");
        }
        [Test]
        public void ContentLoad()
        {
            Console.WriteLine("Loading content \"/default\"");
            Controller cn = new Controller("/default");
            Console.WriteLine(string.Format("ID for {0} is {1}", cn.Title, cn.ID));
            Console.WriteLine("Checking content properties");
            //Assert.IsNotNull(cn.Users, "The users for the test content came back as null.");
            Assert.IsNotNull(cn.Files, "The media for this content came back as null (not empty, null)");
            Assert.IsNotNull(cn.Controllers, "The list of children came back as null (not empty, null)");


            Console.WriteLine("Template ID: {0}", cn.TemplateID);
            Assert.IsNotNull(cn.Template, "The content template is null");
        }

        [Test]
        public void FeaturedContent()
        {
            Controller cn = new Controller("/default");
            Page page = new Page();
            page.PageNumber = 1;
            page.Name = "New featured content page";
            page.Body = "Yay";

            Page page2 = new Page();
            page2.PageNumber = 2;
            page2.Name = "Page 2";
            page2.Body = "Not page 3";

            cn.AddPage(page);

            cn.AddPage(page2);

            cn.Save();

            cn = new Controller("/default");
            cn.RemovePage(1);

            cn.Save();
        }

        [Test]
        public void ContentParent()
        {
            Controller cn = new Controller("/default");
            Controller child = cn.Controllers[0];
            Controller parent = child.Parent;

            Assert.AreEqual(cn.ID, parent.ID, string.Format("Id={0}, ParentId={1}", cn.ID, parent.ID));
        }

        [Test]
        public void ContentSaving()
        {
            Console.WriteLine("Saving content \"/test\"");
            Controller cn = new Controller("/test");

            cn.Title = "Test Content";
            cn.ControllerID = new Guid("51799860-8138-43b4-9584-183af29503e4");
            cn.FileName = "test";
            cn.ModuleControlID = new Guid("152D7361-5CFD-4A76-B2C9-4FB230F3773C");
            cn.TemplateID = new Guid("602b9110-855e-4888-a721-2d61804fd981");
            cn.TemplateControl = "Default.master";
            cn.PublishDate = DateTime.Now;
            cn.ModifyDate = DateTime.Now;
            cn.Status = PublishStatus.Draft;
            //cn.DataManager["Settings"] = string.Empty;

            cn.Save();
            Assert.AreNotEqual(cn.ID, Guid.Empty, "The Content's ID was an empty GUID.  Doh.");
        }

        [TestFixtureSetUp]
        public void SetupDB()
        {
            Console.WriteLine("Setting up database");
            #region Create newest DB
            Server s = new Server(".\\sqlexpress");

            Database db = s.Databases["mubble_test"];

            if (db != null)
            {

                db.ExecuteNonQuery(@"ALTER DATABASE mubble_test
                                    SET SINGLE_USER 
                                    WITH ROLLBACK IMMEDIATE");
                db.Drop();
                Console.WriteLine("Database dropped");

            }
            db = new Database(s, "mubble_test");
            db.Create();



            Console.WriteLine("Database created");

            ProcessStartInfo proc = new ProcessStartInfo("creator.exe");
            proc.Arguments = "migrate /mode test";

            proc.WorkingDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\..\");
            proc.UseShellExecute = false;
            proc.RedirectStandardError = true;

            Process process = Process.Start(proc);
            process.WaitForExit();

            string errors = process.StandardError.ReadToEnd();

            Assert.IsEmpty(errors, string.Format("The errors returned were {0}", errors));

            Console.WriteLine();

            Console.WriteLine("DB Structure updated");

            #endregion

            ConnectionStringsSection section = (ConnectionStringsSection)
                    ConfigurationManager.GetSection("connectionStrings");
            Assert.IsNotNull(section.ConnectionStrings["mubbleTestDB"], "No mubbleTestDB connection string defined in config.");
            string cnString = section.ConnectionStrings["mubbleTestDB"].ConnectionString;

            Console.WriteLine(cnString);
            SqlConnection cn = new SqlConnection(cnString);
            cn.Open();
            Console.WriteLine("Connected successfully: {0}", cn.Database);
            cn.Close();

            ActiveObjects.SqlServer.SqlDataUtility.ReadDB = ActiveObjects.SqlServer.SqlDataUtility.WriteDB = cn.ConnectionString;

            Mubble.Config.Module.LoadDbFromFileSystem(System.IO.Path.Combine(Environment.CurrentDirectory, @"..\..\..\web\Modules"), "~/Modules/");

            try
            {
                db.ExecuteNonQuery(System.IO.File.ReadAllText("db_inserts.sql"));
            }
            catch (Exception ex)
            {
                Exception x = ex;
                while (x.InnerException != null && x.InnerException.Message != null)
                {
                    Console.WriteLine(" -- " + x.InnerException.Message);
                    x = x.InnerException;
                }
                throw ex;
            }

            Console.WriteLine("DB crapola inserted");

            if (System.IO.Directory.Exists("Store"))
            {
                System.IO.Directory.Delete("Store", true);
            }
            System.IO.Directory.CreateDirectory("Store\\");
            Mubble.Models.Settings.Lucene lucene = new Mubble.Models.Settings.Lucene();
            lucene.IndexLocation = "Store\\Lucene";
            Mubble.Models.Settings.Application.Lucene = lucene;
        }
    }
}
