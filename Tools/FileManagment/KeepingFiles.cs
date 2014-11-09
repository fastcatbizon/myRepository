using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ExpansesControlSystem.Tools.FileManagment
{
    public class KeepingFiles
    {
        /// <summary>
        /// Seve unique files on server directory
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Save path for server (unFull)</returns>
        public string SaveUploadFile(HttpPostedFileBase file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Content/UploadedFiles/";
            string filename = Path.GetFileName(file.FileName);
            var fileinf = new FileInfo(filename);
            string fname = fileinf.Name.Remove((fileinf.Name.Length - fileinf.Extension.Length));
            fname = fname + DateTime.Now.ToString("_ddMMyyhhmmss") + fileinf.Extension;
            if (filename != null) file.SaveAs(Path.Combine(path, fname));
            var filePath = "Content/UploadedFiles/" + fname;
            return filePath;
        }
    }
}