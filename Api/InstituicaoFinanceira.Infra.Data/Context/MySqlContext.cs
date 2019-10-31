using Microsoft.EntityFrameworkCore;

namespace InstituicaoFinanceira.Infra.Data.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=instituicaofinanceira;Uid=root;Pwd=123456");
        }

    }
}
