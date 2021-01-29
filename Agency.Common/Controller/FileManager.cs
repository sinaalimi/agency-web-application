using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Agency.Common.Controller
{
    public static class FileManager
    {
        public static string Upload(this BaseController controller, HttpPostedFileBase postedFile, string path)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
            var imagePath = Path.Combine(controller.Server.MapPath(path), fileName);
            postedFile.SaveAs(imagePath);
            return fileName;
        }

        public static string Upload(HttpPostedFileBase postedFile, string path)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
            var imagePath = Path.Combine(HttpContext.Current.Server.MapPath(path), fileName);
            postedFile.SaveAs(imagePath);
            return fileName;
        }

        public static bool Delete(string path)
        {
            try
            {
                string fullPath = HttpContext.Current.Server.MapPath(path);
                File.Delete(fullPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("فایل مورد نظر حذف نشد");
            }
        }
        //public static bool Delete(string fullpath)
        //{
        //    if (File.Exists(fullpath))
        //    {
        //        File.Delete(fullpath);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
