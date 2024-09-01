using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IFileService
    {
        string UploadFile(IFormFile file, string destinationFolder);
    }
}
