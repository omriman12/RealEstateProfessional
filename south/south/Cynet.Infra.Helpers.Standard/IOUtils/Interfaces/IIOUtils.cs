using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.IOUtils.Interfaces
{
    public interface IIOUtils
    {
        #region << Files >>
        bool DoesFileExists(string filePath);
        void DeleteFile(string filePath);

        void FileCopy(string source, string destination);
        #endregion

        #region Directories
        bool DoesDirectoryExists(string dirPath);
        void CreateDirectory(string dirPath);
        void DeleteDirectory(string dirPath);
        string[] GetFilesInDirectory(string dirPath);
        Task WriteFileAsync(string fullPathName, string content, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, int bufferSize);
        #endregion

        DriveInfo[] GetAllDrives();
    }
}
