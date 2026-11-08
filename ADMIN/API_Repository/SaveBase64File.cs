using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ADMIN.API_Repository;

namespace ADMIN.CommonClasses
{
    public class SaveBase64File
    {
        public static string SaveBase64Image(long ID, string FileCategory, string FileName, string Base64string, string FileType)
        {
            string FileSavedPath = "";
            try
            {  
                if (!string.IsNullOrEmpty(Base64string))
                {
                    Base64string = Base64string.Substring(Base64string.IndexOf(",") + 1);
                    FileName = FileName.Trim();
                    string FolderPath = "~\\Images\\" + FileCategory + "\\" + ID;
                    //string FilePath = "~\\Images\\" + FileCategory + "\\" + ID + "\\" + FileName + FileType;
                    //FilePath = HttpContext.Current.Server.MapPath(FilePath);
                    string FileServerPath = HttpContext.Current.Server.MapPath(FolderPath); //Path

                    //Create directory if not exist
                    if (!System.IO.Directory.Exists(FileServerPath))
                    {
                        System.IO.Directory.CreateDirectory(FileServerPath);
                    }
                    //Delete all files in the folder
                    if (Directory.Exists(FileServerPath))
                    {
                        foreach (string fileDocument in Directory.GetFiles(FileServerPath))
                        {
                            File.Delete(fileDocument);
                        }
                    }
                    string imageName = FileName + "_" + ID + FileType;

                    //set the image path
                    FileSavedPath = Path.Combine(FileServerPath, imageName);

                    //Load the file to path
                    byte[] imageBytes = Convert.FromBase64String(Base64string);
                    System.IO.File.WriteAllBytes(FileSavedPath, imageBytes);

                    FileSavedPath = FileSavedPath.Replace(Static_Data.ServerDirectory, "http://");
                    FileSavedPath = FileSavedPath.Replace("\\", "/");
                }
                return FileSavedPath;
            }
            catch
            {
                return "";
            }
        }
    }
}