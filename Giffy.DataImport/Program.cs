using Giffy.DataAccess;
using Giffy.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Giffy.DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader importData = File.OpenText(@"E:\Workshop\MyGit\giffy\Giffy.DataImport\SourceData\VozGifList.txt");
            GiffyContext db = new GiffyContext();
            string gifUrl = string.Empty;
            string imgName = string.Empty;

            do
            {
                gifUrl = importData.ReadLine();
                imgName = Regex.Match(gifUrl, @"\/([^\/]+)\/?$").Groups[1].Value;

                Image gifImg = new Image()
                {
                    Url = gifUrl,
                    IsActived = true,
                    ImageType = ImageType.Gif,
                    Name = imgName,
                    Description = "Imported giff",
                    ObjectState = ObjectState.Added,
                    CreatedDate = DateTime.UtcNow,
                };

                Post gifPost = new Post()
                {
                    Title = imgName,
                    PostType = PostType.GAG,
                    ObjectState = ObjectState.Added,
                    CreatedDate = DateTime.UtcNow
                };

                gifPost.Images = new List<Image>();
                gifPost.Images.Add(gifImg);

                db.Posts.Add(gifPost);
                db.SaveChanges();

                Console.WriteLine("Insert {0} into database.", imgName);

            } while (gifUrl != string.Empty);
        }
    }
}