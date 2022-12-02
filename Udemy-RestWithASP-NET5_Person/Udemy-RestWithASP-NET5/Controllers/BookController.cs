using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Business;
using Udemy_RestWithASP_NET5.Data.VO;
using Udemy_RestWithASP_NET5.Hypermedia.Filters;

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
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetFindAll() {
            return Ok(_bookBusiness.FindAll());
        }
        #endregion

        #region GetFindByID

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] BookVO book) {

            if (book == null) {
                return BadRequest();
            }

            return Ok(_bookBusiness.Create(book));
        }
        #endregion

        #region Put

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book) {

            if (book == null) {
                return BadRequest();
            }

            return Ok(_bookBusiness.Update(book));
        }
#endregion

        #region Delete

        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType((400))]
        [ProducesResponseType((401))]
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
