using Microsoft.AspNetCore.Mvc;
using NLanguage.Tools;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly ITranslator _translator;

        public HelloController(ITranslator translator)
        {
            _translator = translator;
        }

        [HttpGet]
        public string Get()
        {
            return _translator.Translate("hello");
        }
    }
}