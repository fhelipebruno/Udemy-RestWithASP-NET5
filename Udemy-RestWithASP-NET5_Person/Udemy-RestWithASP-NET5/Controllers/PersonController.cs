using Microsoft.AspNetCore.Mvc;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Services;

namespace Udemy_RestWithASP_NET5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;
        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        #region GetFindAll

        [HttpGet]
        public IActionResult GetFindAll()
        {
            return Ok(_personService.FindAll());
        }
        #endregion

        #region GetFindByID

        [HttpGet("{id}")]
        public IActionResult GetFindByID(long id)
        {
            var person = _personService.FindByID(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        #endregion

        #region Create

        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }

            return Ok(_personService.Create(person));
        }
        #endregion

        #region Put

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {

            if (person == null)
            {
                return BadRequest();
            }

            return Ok(_personService.Update(person));
        }
        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
        #endregion

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber, 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, 
                out number);
            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

    }
}