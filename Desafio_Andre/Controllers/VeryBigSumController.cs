using Desafio_Andre.Context;
using Desafio_Andre.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Andre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeryBigSumController : ControllerBase
    {
        private readonly DataContext _context;

        public VeryBigSumController(DataContext context)
        {
            _context = context;
        }
        private static long Helper(VeryBigSum veryBig)
        {
            List<long> ListHelp = veryBig.Input.TrimEnd().Split(' ').ToList().Select(a => Convert.ToInt64(a)).ToList();

            long result = ListHelp.Sum();

            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody] VeryBigSum veryBigSum)
        {
            try
            {
                if (veryBigSum.Id == System.Guid.Empty)
                    return BadRequest();

                veryBigSum.Output = Helper(veryBigSum).ToString();
                _context.VeryBigSum.Add(veryBigSum);
                _context.SaveChanges();

                return Ok(veryBigSum);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                var result = _context.VeryBigSum.ToList().OrderByDescending(p => p.Date);

                if (result.Count() == 0)
                    return NotFound();
                else
                    return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)

        {
            try
            {
                var result = _context.VeryBigSum.FirstOrDefault(p => p.Id == id);

                if (result == null)
                    return NotFound();
                else
                    return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
