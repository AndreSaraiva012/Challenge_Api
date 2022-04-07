namespace Desafio_Andre.Models
{
    public class RatioProblem
    {
        public Guid Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
