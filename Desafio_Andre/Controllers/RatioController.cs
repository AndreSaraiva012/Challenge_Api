using Desafio_Andre.Context;
using Desafio_Andre.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Andre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatioController : ControllerBase
    {
        private readonly DataContext _context;

        public RatioController(DataContext context)
        {
            _context = context;
        }

        private static string Helper(RatioProblem ratioProblem)
        {
            List<int> ListHelp = ratioProblem.Input.TrimEnd().Split(' ').ToList().Select(a => Convert.ToInt32(a)).ToList();

            decimal positiveNumber = 0, negativeNumber = 0, zeros = 0;

            foreach (var item in ListHelp)
            {
                if (item > 0)
                {
                    positiveNumber++;
                }
                else if (item == 0)
                {
                    zeros++;
                }
                else if (item < 0)
                {
                    negativeNumber++;
                }
            }

            return $"{(positiveNumber / ListHelp.Count).ToString("N6")} {(negativeNumber / ListHelp.Count).ToString("N6")} {(zeros / ListHelp.Count).ToString("N6")}";
        }

        [HttpPost]
        public IActionResult Post([FromBody] RatioProblem ratioProblem)
        {
            try
            {
                if (ratioProblem.Id == System.Guid.Empty)
                    return BadRequest();

                ratioProblem.Output = Helper(ratioProblem).ToString();
                _context.RatioProblem.Add(ratioProblem);
                _context.SaveChanges();

                return Ok(ratioProblem);
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
                var result = _context.RatioProblem.ToList().OrderByDescending(p => p.Date);
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
                var result = _context.RatioProblem.FirstOrDefault(p => p.Id == id);

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
