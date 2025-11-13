using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace GymSystemBLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        // function to upload photo and return photoname

        string? Upload(string folderName, IFormFile file);
        // function to delete photo

        bool Delete(string fileName , string folderName);
    }
}