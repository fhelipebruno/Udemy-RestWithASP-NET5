using Udemy_RestWithASP_NET5.Data.Converter.VO;

namespace Udemy_RestWithASP_NET5.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
