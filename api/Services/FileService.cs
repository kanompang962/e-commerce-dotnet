using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.File;
using api.Interfaces;

namespace api.Services
{
    public class FileService : IFileService
    {
        public string? UploadImage(FileDto fileDto, string folder)
        {
            if(fileDto.File == null && fileDto.File?.Length == 0)
                return null;
            
            var folderName = Path.Combine("Resources", folder);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if(!Directory.Exists(pathToSave))
                Directory.CreateDirectory(pathToSave);
            
            // var fileName = fileDto.File!.FileName;
            var fileExtension = Path.GetExtension(fileDto.File!.FileName);
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            if(System.IO.File.Exists(fullPath))
                return null;

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                fileDto.File.CopyTo(stream);
            }

            return dbPath;
        }
    }
}