using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync( IFormFile file);
        void Delete(string fileName);
    }
}
