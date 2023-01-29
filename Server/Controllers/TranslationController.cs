using Microsoft.AspNetCore.Mvc;
using DeepL;
using DeepL.Model;
using WDeff.Server.Data;
using NPOI.SS.Formula.Functions;
using System.Net;

namespace WDeff.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : Controller
    { 
        private readonly DataContext _db;
        private readonly IWebHostEnvironment _env;

        public TranslationController(DataContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        
        public class reqf
        {

            public string text { get; set; }
            public string lengFrom { get; set; }
            public string lengTo { get; set; }
        }

        [HttpPost]
        public async Task<string> PostText(reqf tr)
        {
            Translation translation = new Translation();
            var authKey = "..."; // Replace with your key
            var translator = new Translator(authKey);
            var translatedText = await translator.TranslateTextAsync(
              tr.text,
              tr.lengFrom,
              tr.lengTo);
            translation.OutputText = translatedText.Text;

            _db.Translations.Add(translation);
            await _db.SaveChangesAsync();

            return translation.OutputText;
        }

        [HttpPost("fileName")]
        public async Task<ActionResult<List<Translation>>> UploadFile(List<IFormFile> files)
        {
            List<Translation> uploadResults = new List<Translation>();

            foreach (var file in files)
            {
                var uploadResult = new Translation();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForFileDisplay = WebUtility.HtmlEncode(untrustedFileName);

                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "Assets", trustedFileNameForFileStorage);

                await using FileStream fs = new(path, FileMode.Create); 
                await file.CopyToAsync(fs);

                uploadResult.StoredFileName = trustedFileNameForFileStorage;
                uploadResults.Add(uploadResult);
            }

            return Ok(uploadResults);
        }
    }
}
