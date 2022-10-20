using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Business;

namespace Udemy_RestWithASP_NET5.Controllers {

    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase {

        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;
        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness) {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        #region GetFindAll

        [HttpGet]
        public IActionResult GetFindAll() {
            return Ok(_bookBusiness.FindAll());
        }
        #endregion

        #region GetFindByID

        [HttpGet("{id}")]
        public IActionResult GetFindByID(long id) {
            var book = _bookBusiness.FindByID(id);

            if (book == null) {
                return NotFound();
            }

            return Ok(book);
        }
        #endregion

        #region Create

        [HttpPost]
        public IActionResult Create([FromBody] Book book) {

            if (book == null) {
                return BadRequest();
            }

            return Ok(_bookBusiness.Create(book));
        }
        #endregion

        #region Put

        [HttpPut]
        public IActionResult Put([FromBody] Book book) {

            if (book == null) {
                return BadRequest();
            }

            return Ok(_bookBusiness.Update(book));
        }
#endregion

        #region Delete

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            _bookBusiness.Delete(id);
            return NoContent();
        }
        #endregion

        private bool IsNumeric(string strNumber) {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber) {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue)) {
                return decimalValue;
            }
            return 0;
        }
    }
}
