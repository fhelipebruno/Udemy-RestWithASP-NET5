using Udemy_RestWithASP_NET5.Data.Converter.VO;

namespace Udemy_RestWithASP_NET5.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;
            return File.ReadAllBytes(filePath); 
        }
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new();

            
            var fileType = Path.GetExtension(file.FileName); //Recupera extensão do arquivo
            var baseUrl = _context.HttpContext.Request.Host; //Recupera o baseUrl (link) de onde está hospedada a API

            if(fileType.ToLower().Equals(".pdf") || fileType.ToLower().Equals(".jpg") || fileType.ToLower().Equals(".png") || fileType.ToLower().Equals(".jpeg"))
            {
                var docName = Path.GetFileName(file.FileName); //Recupera o nome do arquivo

                if(file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create); //Instancia classe para criar o arquivo

                    await file.CopyToAsync(stream); //Cria o arquivo no diretório especificado
                }
            }

            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> fileDetails = new();

            foreach (var file in files)
            {
                fileDetails.Add(await SaveFileToDisk(file));
            }           

            return fileDetails;
        }

    }
}
