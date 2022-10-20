using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Business;

namespace Udemy_RestWithASP_NET5.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        #region GetFindAll

        [HttpGet]
        public IActionResult GetFindAll()
        {
            return Ok(_personBusiness.FindAll());
        }
        #endregion

        #region GetFindByID

        [HttpGet("{id}")]
        public IActionResult GetFindByID(long id)
        {
            var person = _personBusiness.FindByID(id);

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

            return Ok(_personBusiness.Create(person));
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

            return Ok(_personBusiness.Update(person));
        }
        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
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