using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using FileUpload.Configuration;
using System.Linq;

namespace FileUpload
{

    /// <summary>
    /// 上传成功后返回的文件信息
    /// </summary>
    public class FileInfo
    {
        public string FileName { get; set; }
        public string PhysicalDir { get; set; }
        public string RelativeDir { get; set; }
        public string Ext { get; set; }
        public string RelativePath { get; set; }
        public string PhysicalPath { get; set; }
        public string ThumbName { get; set; }
    }


    /// <summary>
    /// 文件上传异常类
    /// </summary>
    public class FileUploadException : Exception
    {
        public FileUploadException(string msg)
            : base(msg)
        {

        }
    }

    /// <summary>
    /// 图片类型枚举（可添加，严禁修改）
    /// </summary>


    /// <summary>
    /// 文件上传类
    /// </summary>
    public class FileUploader
    {
        /// <summary>
        /// 缩略图配置
        /// </summary>
        private bool _isThumb;

        private int _width;
        private int _height;

        /// <summary>
        /// 上传文件的物理根目录，如：C:\\web\\file\\，由外部传进来
        /// </summary>
        protected string Root;


        private string _fileTypeName;
        private string _folderName;

        private FileUploaderSection _config;

        public FileUploader(string root, string fileTypeName, string folderName)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new FileUploadException("上传文件的真实路径不能为空");
            }
            if (string.IsNullOrEmpty(fileTypeName))
            {
                throw new FileUploadException("上传文件的类型不能为空");
            }
            if (string.IsNullOrEmpty(folderName))
            {
                throw new FileUploadException("上传文件的要保存的文件夹不能为空");
            }

            _config = (FileUploaderSection)ConfigurationManager.GetSection(FileUploaderSection.SectionName);
            Root = root;
            _fileTypeName = fileTypeName;
            _folderName = folderName;
        }

        /// <summary>
        /// 异步上传文件，虚方法，子类功能满足不了可以重写此方法
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public virtual async Task<FileInfo> UploadAsync(string fileName, Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return await UploadAsync(fileName, ms.ToArray());
            }

        }

        /// <summary>
        /// 异步上传文件，虚方法，子类功能满足不了可以重写此方法
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="bytes">文件的字节数据</param>
        /// <returns></returns>
        public virtual async Task<FileInfo> UploadAsync(string fileName, byte[] bytes)
        {
            Check(fileName, bytes.Length);
            var fileInfo = CreatePath(fileName);

            await Task.Run(() =>
            {
                File.WriteAllBytes(fileInfo.PhysicalPath, bytes);
            });

            if (_isThumb) //如果允许生成缩略图就上传
            {
                if (_width > 0 && _height > 0)
                {
                    var stream = new MemoryStream(bytes);
                    var img = Image.FromStream(stream);
                    string thumbName = fileInfo.FileName + "_" + _width + "_" + _height + fileInfo.Ext;
                    string thumbPath = Path.Combine(fileInfo.PhysicalDir, thumbName);
                    var image = img.GetThumbnailImage(_width, _height, null, IntPtr.Zero);

                    await Task.Run(() =>
                    {
                        image.Save(thumbPath);
                    });

                    fileInfo.ThumbName = thumbName;
                }
            }

            return fileInfo;
        }


        /// <summary>
        /// 生成文件夹路径和文件路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        protected FileInfo CreatePath(string fileName)
        {
            string fileExtension = Path.GetExtension(fileName);
            string builderDiretory = "/" + _fileTypeName + "s" + "/" + _folderName + "/" + DateTime.Now.ToString("yyyy/MM/dd/");
            string dir = Root + builderDiretory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string fileFullName = DateTime.Now.ToFileTime() + fileExtension;
            string relativePath = Path.Combine(builderDiretory, fileFullName);
            string physicalPath = Path.Combine(dir, fileFullName);

            return new FileInfo()
            {
                PhysicalPath = physicalPath,
                RelativePath = relativePath,
                FileName = fileFullName,
                Ext = fileExtension,
                RelativeDir = builderDiretory,
                PhysicalDir = dir
            };
        }

        /// <summary>
        /// 检查文件是否合法并返回一个文件对象
        /// </summary>
        /// <param name="fileName">文件名，用于检查后缀名</param>
        /// <param name="size">大小，用于检查文件大小</param>
        private void Check(string fileName, long size)
        {
            string fileExtension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new FileUploadException("文件类型不合法");
            }

            var fileType = _config.FileTypes.OfType<FileTypeElement>().FirstOrDefault(a => a.TypeName == _fileTypeName);
            if (fileType == null || !fileType.AllowFileExtensions.Contains(fileExtension))
            {
                throw new FileUploadException("文件类型不合法");
            }

            if (size > fileType.MaxSize)
            {
                throw new FileUploadException("文件大小超出了限制");
            }

            var folder = fileType.Folders.OfType<FolderElement>().FirstOrDefault(a => a.FolderName == _folderName);
            if (folder == null)
            {
                throw new FileUploadException("未配置的文件夹");
            }

        }

        /// <summary>
        /// 上传文件时，是否附带生成缩略图(图片文件有效)，调用一下此方法就行
        /// </summary>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <returns></returns>
        public FileUploader WithThumbImg(int width, int height)
        {
            _width = width;
            _height = height;
            _isThumb = true;
            return this;
        }

    }

}
