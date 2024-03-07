using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.File;

namespace api.Interfaces
{
    public interface IFileService
    {
        string? UploadImage(FileDto fileDto, string folder);
    }
}