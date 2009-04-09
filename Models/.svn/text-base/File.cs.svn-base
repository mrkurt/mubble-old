using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

using ActiveObjects;
using ActiveObjects.SqlServer;

namespace Mubble.Models
{

	public partial class File
    {
        #region Storage settings
        private static string fileStoreBase;

        /// <summary>
        /// Gets or sets the base path for the file store
        /// </summary>
        public static string FileStoreBase
        {
            get
            {
                if (fileStoreBase != null)
                {
                    return fileStoreBase;
                }
                throw new NullReferenceException("The file store location was not properly set");

            }
            set { fileStoreBase = value; }
        }
        #endregion

        #region Extended properties
        // Stick your code here
        /// <summary>
        /// Returns the complete mime type of the file
        /// </summary>
        public string MimeType
        {
            get { return this.MediaType + "/" + this.MediaSubType; }
        }
        /// <summary>
        /// Gets the mime type for resized versions of this media
        /// </summary>
        public string ThumbnailMimeType
        {
            get
            {
                if (this.ThumbnailsAvailable)
                {
                    return this.MimeType;
                }
                else
                {
                    return "image/png";
                }
            }
        }

        public bool FileExists
        {
            get 
            {
                if (this.PhysicalPath == null) return false;
                return System.IO.File.Exists(this.SourceFilePath); 
            }
        }

        public string SourceFilePath
        {
            get
            {
                if (this.PhysicalPath == null) return null;
                return Path.Combine(FileStoreBase, Path.Combine("Raw", this.PhysicalPath));
            }
        }

        /// <summary>
        /// True if this media can be resized, false if not.
        /// </summary>
        public bool ThumbnailsAvailable
        {
            get
            {
                List<string> imageTypes = new List<string>(new string[] { ".jpg", ".jpeg", ".gif", ".png" });
                FileInfo info = new FileInfo(this.PhysicalPath);
                return imageTypes.Contains(info.Extension.ToLower());
            }
        }
        #endregion

        #region Media helper functions

        /// <summary>
        /// Saves the the media item, and corresponding physical file.
        /// </summary>
        /// <param name="fileContent">The file contents to save</param>
        public void Save(Stream fileContent)
        {
            //Need to get/build the real path where this file will reside
            if (this.PhysicalPath == null || this.PhysicalPath == string.Empty)
            {
                this.PhysicalPath = this.BuildPhysicalPath(this.FileName);
            }
            FileInfo info = new FileInfo(this.PhysicalPath);

            string registryMediaType = (string)Registry.GetValue(
                "HKEY_CLASSES_ROOT\\" + info.Extension,
                "Content Type",
                "application/octet-stream") ?? "application/octet-stream";

            string[] mediaTypes = registryMediaType.Split(new char[] { '/' });

            this.MediaType = mediaTypes[0];
            this.MediaSubType = mediaTypes[1];

            string fullPath = FileStoreBase + "raw\\" + this.PhysicalPath;

            FileInfo fullPathInfo = new FileInfo(fullPath);
            Directory.CreateDirectory(fullPathInfo.DirectoryName);

            using (FileStream file = new FileStream(fullPath, FileMode.CreateNew))
            {
                byte[] buffer = new byte[fileContent.Length];
                fileContent.Read(buffer, 0, buffer.Length);

                file.Write(buffer, 0, buffer.Length);
                file.Close();
            }

            bool fileExists = true;
            FileInfo fileNameInfo = new FileInfo(this.FileName);

            string baseFileName = fileNameInfo.Name.Substring(
                0,
                fileNameInfo.Name.Length - fileNameInfo.Extension.Length
                );

            int count = 1;
            while (fileExists)
            {
                File test = new File();
                if (test.Load(this.ControllerID, this.FileName))
                {
                    this.FileName = string.Format("{0}-{1}{2}", baseFileName, count++, fileNameInfo.Extension);
                }
                else
                {
                    fileExists = false;
                }
            }

            this.DataManager.Save();
        }

        protected string BuildPhysicalPath(string fileName)
        {
            FileInfo info = new FileInfo(fileName);
            DateTime now = DateTime.Now;
            int week = (now.DayOfYear / 52);

            return string.Format(
                @"{0}\w{1:0#}\m{2:0#}-d{3:0#}[t{4}]{5}",
                now.Year,
                week,
                now.Month,
                now.Day,
                now.Ticks,
                info.Extension
            );
        }
        #endregion

        #region Media removal functions
        /// <summary>
        /// Deletes media from the file store and database
        /// </summary>
        public void Delete()
        {
            string fullPath = FileStoreBase + "raw\\" + this.PhysicalPath;

            string thumbPath = FileStoreBase + "Thumbs\\" + this.PhysicalPath;

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            if (Directory.Exists(thumbPath))
            {
                Directory.Delete(thumbPath, true);
            }

            this.DataManager.Delete();
        }
        #endregion

        #region Thumbnail Functions
        /// <summary>
        /// Gets the location of a thumbnail using the requested size
        /// </summary>
        /// <param name="width">The desired thumbnail width. 0 for maximum.</param>
        /// <param name="height">The desired thumbnail height. 0 for maximum.</param>
        /// <returns>A full path to the thumbnail.</returns>
        public string GetThumbnail(int width, int height)
        {
            string sourceFile = FileStoreBase + "Raw\\" + this.PhysicalPath;
            if (width == 0 && height == 0)
            {
                return sourceFile;
            }
            FileInfo sourceFileInfo = new FileInfo(sourceFile);
            string thumbFolder = FileStoreBase + "Thumbs\\" + this.PhysicalPath + "\\";
            string thumbFile = string.Format(
                "{0}Thumbs\\{1}\\{2}-{3}x{4}{5}",
                FileStoreBase,
                this.PhysicalPath,
                "Normal",
                width,
                height,
                sourceFileInfo.Extension
                );

            if (!this.ThumbnailsAvailable)
            {
                #region locate icons
                sourceFile = string.Format(
                    "{0}Icons\\{1}\\{2}.png",
                    FileStoreBase,
                    this.MediaType,
                    this.MediaSubType
                    );
                thumbFile = string.Format(
                    "{0}Icons\\{1}\\{2}.thumbs\\{3}-{4}x{5}.png",
                    FileStoreBase,
                    this.MediaType,
                    this.MediaSubType,
                    "Normal",
                    width,
                    height
                    );
                if (!System.IO.File.Exists(sourceFile))
                {
                    sourceFile = string.Format(
                        "{0}Icons\\{1}.png",
                        FileStoreBase,
                        this.MediaType
                    );
                    thumbFile = string.Format(
                        "{0}Icons\\{1}.thumbs\\{2}-{3}x{4}.png",
                        FileStoreBase,
                        this.MediaType,
                        "Normal",
                        width,
                        height
                    );
                }
                if (!System.IO.File.Exists(sourceFile))
                {
                    sourceFile = string.Format(
                        "{0}Icons\\default.png",
                        FileStoreBase
                    );
                    thumbFile = string.Format(
                        "{0}Icons\\{1}.thumbs\\{2}-{3}x{4}.png",
                        FileStoreBase,
                        "default",
                        "Normal",
                        width,
                        height
                    );
                }
                #endregion
            }
            //If it exists, we're done
            sourceFileInfo = new FileInfo(sourceFile);
            FileInfo thumbFileInfo = new FileInfo(thumbFile);
            if (System.IO.File.Exists(thumbFile) && thumbFileInfo.LastWriteTime > sourceFileInfo.LastWriteTime)
            {
                return thumbFile;
            }
            if (!Directory.Exists(thumbFileInfo.DirectoryName))
            {
                Directory.CreateDirectory(thumbFileInfo.DirectoryName);
            }
            //Thumbnail hasn't been created
            //Resize the image and return the thumbnail location
            if (!Directory.Exists(thumbFolder))
            {
                Directory.CreateDirectory(thumbFolder);
            }
            Bitmap srcImg = new Bitmap(sourceFile);

            try
            {
                if ((width == 0 || srcImg.Width <= width) && (height == 0 || srcImg.Height <= height))
                {
                    System.IO.File.Copy(sourceFile, thumbFile, true);
                }
                else
                {
                    Bitmap newImg = this.ResizeImage(srcImg, width, height);
                    //newImg.Save(thumbFile);
                    //newImg.Dispose();
                    this.SaveWithEncoder(newImg, thumbFile);

                    newImg.Dispose();
                    newImg = null;
                }
            }
            finally
            {
                srcImg.Dispose();
                srcImg = null;
            }

            return thumbFile;
        }

        /// <summary>
        /// Saves a Bitmap object using some sane codec parameters
        /// </summary>
        /// <param name="newImg">The bitmap object to save.</param>
        /// <param name="fileName">The file to save the bitmap to.</param>
        protected void SaveWithEncoder(Bitmap newImg, string fileName)
        {
            // Setup encoder information
            ImageCodecInfo encoder = null;
            Encoder qualityEncoder = Encoder.Quality;
            Encoder compressionEncoder = Encoder.Compression;
            EncoderParameter q = new EncoderParameter(qualityEncoder, 60L);
            EncoderParameters codecParams = new EncoderParameters(1);
            codecParams.Param[0] = q;

            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == this.ThumbnailMimeType)
                {
                    encoder = encoders[j];
                    break;
                }
            }
            // Encoders done

            newImg.Save(fileName, encoder, codecParams);
            newImg.Dispose();
        }

        public Bitmap ResizeImage(Bitmap srcImg, int width, int height)
        {
            System.Drawing.Bitmap bmpOut = null;
            Bitmap loBMP = srcImg;
            ImageFormat loFormat = loBMP.RawFormat;
            decimal lnRatio;
            int lnNewWidth = 0;
            int lnNewHeight = 0;

            if (width == 0 && height > 0)
            {
                decimal ratio = (decimal)height / (decimal)loBMP.Height;
                width = (int)((decimal)loBMP.Width * ratio);
            }
            else if (width == 0)
            {
                width = loBMP.Width;
            }
            if (height == 0 && width > 0)
            {
                decimal ratio = (decimal)width / (decimal)loBMP.Width;
                height = (int)((decimal)loBMP.Height * ratio);
            }
            else if (height == 0)
            {
                height = loBMP.Height;
            }

            //*** If the image is smaller than a thumbnail just return it
            if (loBMP.Width < width && loBMP.Height < height)
            {
                return loBMP;
            }

            if (loBMP.Width > loBMP.Height)
            {
                lnRatio = (decimal)width / loBMP.Width;
                lnNewWidth = width;
                decimal lnTemp = loBMP.Height * lnRatio;
                lnNewHeight = (int)lnTemp;
            }
            else
            {
                lnRatio = (decimal)height / loBMP.Height;
                lnNewHeight = height;
                decimal lnTemp = loBMP.Width * lnRatio;
                lnNewWidth = (int)lnTemp;
            }

            // *** This code creates cleaner (though bigger) thumbnails and properly
            // *** and handles GIF files better by generating a white background for
            // *** transparent images (as opposed to black)
            bmpOut = new Bitmap(lnNewWidth, lnNewHeight, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmpOut);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

            loBMP.Dispose();

            return bmpOut;
        }
        #endregion

    }
}
