using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.Services
{
    public class UploadService : IUploadService
    {
        public string UploadImage()
        {
            return "Upload Success";
        }
    }
}