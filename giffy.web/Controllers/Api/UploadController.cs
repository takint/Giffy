using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Giffy.ApiControllers
{
    [Authorize]
    public class UploadController : BaseApiController
    {
        private Cloudinary cloudinary = null;

        public UploadController()
        {
            Account account = new Account(
                  "bongvl",
                  "711689592998382",
                  "Q_oD8yTW9w-PEfFNb7dMmuFa3xo");
            cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        [Route("api/upload")]
        public RawUploadResult Upload()
        {
            RawUploadResult result = null;

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["file"];

                try
                {
                    if (httpPostedFile.ContentType == "video/mp4")
                    {
                        var videoUploadParams = new VideoUploadParams()
                        {
                            File = new FileDescription(httpPostedFile.FileName, httpPostedFile.InputStream)
                        };

                        result = cloudinary.Upload(videoUploadParams);
                    }
                    else
                    {
                        Stream processedImg = httpPostedFile.ContentType == "image/gif" ? httpPostedFile.InputStream : ResizeUploadImage(httpPostedFile.InputStream, null, null);
                        var imageUploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(httpPostedFile.FileName, processedImg)
                        };
                        result = cloudinary.Upload(imageUploadParams);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
    
            return result;
        }

        [HttpPost]
        [Route("api/uploadavatar")]
        public RawUploadResult UploadAvatar()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                HttpPostedFile uploadedImg = HttpContext.Current.Request.Files["file"];
                int maxUploadSize = int.Parse(ConfigurationManager.AppSettings["MaxiumUploadSize"]);
                int avatarWidth = int.Parse(ConfigurationManager.AppSettings["AvatarWidth"]);
                int avatarHeight = int.Parse(ConfigurationManager.AppSettings["AvatarHeight"]);

                if (uploadedImg.ContentLength > 0 && uploadedImg.ContentLength <= maxUploadSize) {

                    Stream processedImg = ResizeUploadImage(uploadedImg.InputStream, avatarWidth, avatarHeight);

                    ImageUploadParams avatarUploadParam = new ImageUploadParams()
                    {
                        File = new FileDescription(uploadedImg.FileName, processedImg),
                        Folder = ConfigurationManager.AppSettings["AvatarFolder"],
                        Format = "jpeg"
                    };
                    
                    return cloudinary.Upload(avatarUploadParam); ;
                }
            }

            throw new Exception("Please provide image file for your avatar.");
        }

        private Stream ResizeUploadImage(Stream uploadedImg, int? width, int? height)
        {
            Image sourceImg = Image.FromStream(uploadedImg);
            if(width == null)
            {
                width = sourceImg.Width;
            }
            if(height == null)
            {
                height = sourceImg.Height;
            }
            Stream resultImg = new MemoryStream();
            Rectangle convertRegtangle = new Rectangle(0, 0, width.Value, height.Value);

            using (Bitmap destImg = new Bitmap(width.Value, height.Value))
            {
                destImg.SetResolution(sourceImg.HorizontalResolution, sourceImg.VerticalResolution);

                using (Graphics drawObject = Graphics.FromImage(destImg))
                {
                    drawObject.CompositingMode = CompositingMode.SourceCopy;
                    drawObject.CompositingQuality = CompositingQuality.HighQuality;
                    drawObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    drawObject.SmoothingMode = SmoothingMode.HighQuality;
                    drawObject.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (ImageAttributes wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        drawObject.Clear(Color.White);
                        drawObject.DrawImage(sourceImg, convertRegtangle, 0, 0, sourceImg.Width, sourceImg.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                destImg.Save(resultImg, ImageFormat.Jpeg);
                resultImg.Position = 0;

                return resultImg;
            }
        }
    }
}