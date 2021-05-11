using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FileUpload;

namespace WebApi.File.Controllers
{
  
    public class FileUploadController : ApiController
    {
        public async Task<IHttpActionResult> Upload()
        {

            try
            {
                var context = HttpContext.Current;
                var fileTypeName = context.Request.QueryString["t"];
                var folderName = context.Request.QueryString["f"];
                var files = context.Request.Files;
                if (files.Count > 0)
                {
                    var file = files[0];
                    string root = context.Server.MapPath("/");
                    var uploader = new FileUploader(root, fileTypeName, folderName);

                    var fileInfo = await uploader.UploadAsync(file.FileName, file.InputStream);
                    var domain = context.Request.Url.Scheme + "://" + context.Request.Url.Authority;

                    return Ok(new
                    {
                        ReCode = 200,
                        Data = new
                        {
                            Path = domain + fileInfo.RelativePath,
                            fileInfo.FileName,
                            fileInfo.Ext,
                            fileInfo.ThumbName,
                            ThumbPath = domain + fileInfo.RelativeDir + fileInfo.ThumbName,
                            domain
                        }
                    });
                }
                return Ok();
            }
            catch (FileUploadException ex)
            {
                return Ok(new { ReCode = 210, ErrorMsg = ex.Message });
            }
        }
    }
}
