using Microsoft.AspNetCore.Mvc;
using DeepL;
using DeepL.Model;
using WDeff.Server.Data;
using NPOI.SS.Formula.Functions;

namespace WDeff.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : Controller
    { 
        private readonly DataContext _db;

        public TranslationController(DataContext db)
        {
            _db = db;
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
            var authKey = "6bc6855b-580d-5db2-5bce-4ba6b7b40267:fx"; // Replace with your key
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
    }
}
