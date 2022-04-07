using Desafio_Andre.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Andre.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) => Database.EnsureCreated();

        public DbSet<VeryBigSum> VeryBigSum { get; set; }
        public DbSet<DiagonalSum> DiagonalSum { get; set; }
        public DbSet<RatioProblem> RatioProblem { get; set; }

    }
}
