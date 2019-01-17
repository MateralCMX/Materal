using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Materal.ConsoleApp.Model;
using Materal.ConvertHelper;
using Materal.FileHelper;

namespace Materal.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("请输入一个文件夹路径:");
            string filePath = Console.ReadLine();
            if (!string.IsNullOrEmpty(filePath))
            {
                if (Directory.Exists(filePath))
                {
                    var directoryInfo = new DirectoryInfo(filePath);
                    DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();
                    List<Base64ImageModel> images = new List<Base64ImageModel>();
                    foreach (DirectoryInfo item in directoryInfos)
                    {
                        FileInfo[] fileInfos = item.GetFiles();
                        images.AddRange(fileInfos.Select(fileInfo => new Base64ImageModel {Name = $"{item.Name}/{fileInfo.Name}", Image = ImageFileManager.GetBase64Image(fileInfo.FullName)}));
                    }
                    string result = images.ToJson();
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("文件夹不存在");
                }
            }
            else
            {
                Console.WriteLine("文件夹路径不能为空");
            }
            Console.ReadKey();
        }
    }
}
