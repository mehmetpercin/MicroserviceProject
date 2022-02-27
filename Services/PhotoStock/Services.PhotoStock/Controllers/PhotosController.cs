using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.PhotoStock.Dtos;
using SharedLibrary.Controllers;
using SharedLibrary.Dtos;

namespace Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> SavePhoto(IFormFile photo, CancellationToken cancellationToken = default)
        {
            if (photo == null || photo.Length <= 0)
            {
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo cannot be empty", 400));
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);
            var photoDto = new PhotoDto
            {
                Url = photo.FileName
            };
            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePhoto(string photoUrl)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<object>.Fail("Photo not found", 404));
            }

            System.IO.File.Delete(path);
            await Task.CompletedTask;

            return CreateActionResultInstance(Response<object>.Success(200));
        }
    }
}
