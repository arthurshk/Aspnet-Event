using ASP_proj.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ASP_proj.Services
{
    public class ImageStorage
    {
        private readonly IWebHostEnvironment _environment;
        public readonly SiteContext _siteContext;
        public string _uploadFolder = "uploads";
        public ImageStorage(IWebHostEnvironment environment, SiteContext context)
        {
            _siteContext = context;
            _environment = environment;
        }
        public string UploadFolder
        {
            get { return _uploadFolder; }
            set
            {
                var path = Path.Combine(_environment.WebRootPath, value);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                _uploadFolder = value;
            }
        }
        public void Remove(Models.Image model)
        {
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, UploadFolder, model.Filename));
            _siteContext.Images.Remove(model);
        }
        public async Task<Models.Image> UploadAsync(IFormFile file)
        {

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using (var writer = new FileStream(Path.Combine(_environment.WebRootPath, UploadFolder, filename), FileMode.Create))
            {
                await file.CopyToAsync(writer);
            }
            var image = new Models.Image()
            {
                Filename = filename
            };
            await _siteContext.Images.AddAsync(image);
            return image;
        }
    }

}
