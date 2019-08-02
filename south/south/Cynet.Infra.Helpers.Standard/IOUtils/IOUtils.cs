using Cynet.Infra.Helpers.IOUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers.IOUtils
{
    public class IOUtils : IIOUtils
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(IOUtils));

        #region << Files >>
        public bool DoesFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public void FileCopy(string source, string destination)
        {
            File.Copy(source, destination);
        }
        #endregion

        #region Directories
        public bool DoesDirectoryExists(string dirPath)
        {
            return Directory.Exists(dirPath);
        }
        public void CreateDirectory(string dirPath)
        {
            Directory.CreateDirectory(dirPath);
        }
        public void DeleteDirectory(string dirPath)
        {
            Directory.Delete(dirPath, true);
        }
        public string[] GetFilesInDirectory(string dirPath)
        {
            return Directory.GetFiles(dirPath);
        }
        public async Task WriteFileAsync(string fullPathName, string content, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, int bufferSize )
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(content);

            using (var sourceStream = new FileStream(fullPathName, fileMode, fileAccess, fileShare, bufferSize: bufferSize, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
        #endregion

        public DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }
    }
}
