using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class FileService : IFileService
    {
        public string UploadFile(IFormFile file, string destinationFolder)
        {
            string uniqueFileName = string.Empty;

            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(@"./wwwroot/", destinationFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
