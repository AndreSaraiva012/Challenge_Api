using Desafio_Andre.Context;
using Desafio_Andre.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio_Andre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagonalSumController : ControllerBase
    {
        private readonly DataContext _context;

        public DiagonalSumController(DataContext context)
        {
            _context = context;
        }

        private static int Helper(List<List<int>> diagonalSum)
        {

            int SumRight = 0, SumLeft = 0;

            for (int i = 0; i < diagonalSum.Count; i++)
            {
                SumRight += diagonalSum[i][diagonalSum.Count - i - 1];
                SumLeft += diagonalSum[i][i];
            }

            return Math.Abs(SumRight - SumLeft);
        }
     
        [HttpPost]
        public IActionResult Post([FromBody] string input)
        {
            try
            {
                DiagonalSum diagonalSum = new();
                List<string> list = new();
                List<List<int>> listInList = new();

                if (diagonalSum.Id == System.Guid.Empty)
                    return BadRequest();

                list = input.TrimEnd().Split(',').ToList();
                list.ForEach(x => listInList.Add(x.TrimEnd().Split(' ').ToList().Select(b => Convert.ToInt32(b)).ToList()));

                diagonalSum.Input = input;
                diagonalSum.Output = Helper(listInList).ToString();

                _context.DiagonalSum.Add(diagonalSum);
                _context.SaveChanges();
                return Ok(diagonalSum);
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
                var result = _context.DiagonalSum.ToList().OrderByDescending(p => p.Date);
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
                var result = _context.DiagonalSum.FirstOrDefault(p => p.Id == id);

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
