using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Project.PL.Helper
{
    public class DocumentSetting
    {
        public static string Upload(IFormFile file,string FolderName)
        {
            //Get Folder Path 

            //string FolderPath= "D:\ASP.NET\PROJECT MVC\MVC_Project.PL\wwwroot\Files\Images\";
            string FolderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files",FolderName);

            //Get File Name then make it Unique
            string FileName=$"{Guid.NewGuid()}{file.FileName}";

            //Get File Path
            string FilePath=Path.Combine(FolderPath,FileName);

            //Save File as streams
            using var filestream=new FileStream(FilePath,FileMode.Create);
            file.CopyTo(filestream);

            return FileName;

        }
        public static void DeleteFile(string FileName, string FolderName)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName, FileName);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
