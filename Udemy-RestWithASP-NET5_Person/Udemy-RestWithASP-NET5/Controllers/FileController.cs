using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Udemy_RestWithASP_NET5.Business;
using Udemy_RestWithASP_NET5.Data.Converter.VO;

namespace Udemy_RestWithASP_NET5.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);

            return new OkObjectResult(detail);
        }
        
        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType(200, Type = typeof(List<FileDetailVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            List<FileDetailVO> details = await _fileBusiness.SaveFilesToDisk(files);

            return new OkObjectResult(details);
        }

        [HttpGet("downloadFile")]
        [ProducesResponseType(200, Type = typeof(byte[]))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octect-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] file = _fileBusiness.GetFile(fileName);

            if(file != null)
            {
                HttpContext.Response.ContentType = $"applcation/{Path.GetExtension(fileName).Replace(".","")}";
                HttpContext.Response.Headers.Add("content-length", file.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(file, 0, file.Length);
            }

            return new OkObjectResult(file);
        }

    }
}
