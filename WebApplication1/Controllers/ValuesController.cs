using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {

        [HttpPost]
        public List<string> GetPDFs([FromBody] CreateListModel m)
        {
            var targLoc = m.targetLocation;
            List<string> rejectedFiles = new List<string>();
            using (WebClient client = new WebClient())
            {
                foreach (var file in m.Files)
                {
                    try
                    {
                        client.DownloadFile(file.name, targLoc + Path.GetFileName(file.name));
                        //rejectedFiles.Add(file.name); //just for example
                    }
                    catch (Exception ex)
                    {
                        var result = ex.Message;//Can get the file that error'd with file.name
                        rejectedFiles.Add(file.name);
                    }
                }
            }
            
            return rejectedFiles;
        }
    }

    public class CreateListModel
    {
        public string targetLocation { get; set; }
        public List<FileViewModel> Files { set; get; }
    }

    public class FileViewModel
    {
        public string name { set; get; }
    }
}
