namespace Desafio_Andre.Models
{
    public class DiagonalSum
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string Input { get; set; }
        public string Output { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
