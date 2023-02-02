using Microsoft.AspNetCore.Mvc;
using DeepL;
using DeepL.Model;
using WDeff.Server.Data;
using NPOI.SS.Formula.Functions;
using System.Net;
using System.Xml;
using WDeff.Shared;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
            public string? outText { get; set; }
            public string? text { get; set; }
            public string? lengFrom { get; set; }
            public string? lengTo { get; set; }
        }

        [HttpPost("Translate")]
        public async Task<string> PostText(reqf tr)
        {
            Translation translation = new Translation();
            var authKey = "..."; // Replace with your key
            var translator = new Translator(authKey);
            var translatedText = await translator.TranslateTextAsync(
              tr.text ?? "Hello",
              tr.lengFrom ?? "en",
              tr.lengTo ?? "pl");
            translation.OutputText = translatedText.Text;
            translation.InputText = tr.text;
            translation.LanguageFrom = tr.lengFrom;
            translation.LanguageTo = tr.lengTo;


            _db.Translations.Add(translation);
            await _db.SaveChangesAsync();

            return translation.OutputText;
        }

        [HttpPost("importXML")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            XmlDocument doc = new XmlDocument();


            using (var ms = file.OpenReadStream())
            {
                doc.Load(ms);

                var translate = doc.GetElementsByTagName("Translation");

                foreach (XmlNode w in translate)
                {
                    string? langFrom = w.Attributes.GetNamedItem("fromLanguage")?.Value;
                    string? langTo = w.Attributes.GetNamedItem("toLanguage")?.Value;
                    string? inputText = w.FirstChild.Attributes.GetNamedItem("InputText")?.Value;
                    string? outputText = w.FirstChild.Attributes.GetNamedItem("OutputText")?.Value;

                    _db.Translations.Add(new Translation
                    {
                        LanguageFrom = langFrom ??"",
                        LanguageTo = langTo ??"",
                        InputText = inputText ?? "",
                        OutputText = outputText ?? ""
                    });
                }
                await _db.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("exportJSON")]
        public async Task<ActionResult> ExportFile()
        {
            List<reqf> dataList = new List<reqf>();

          
            foreach (var item in _db.Translations)
            {
                dataList.Add(new reqf
                {
                    lengFrom = item.LanguageFrom,
                    lengTo = item.LanguageTo,
                    text = item.InputText,
                    outText = item.OutputText
                });
            }
            dataList.ToArray();
            var tempjson = JsonSerializer.Serialize(dataList);
            string jsonPath = Path.Combine("Assets", "data.json");
            System.IO.File.WriteAllText(jsonPath, tempjson);

            return Ok();
        }

        [HttpGet("getdb")]
        public async Task<List<Translation>> GetDb()
        {
            return await _db.Translations.OrderByDescending(x=>x.Id).ToListAsync();
        }
       
    }
}
