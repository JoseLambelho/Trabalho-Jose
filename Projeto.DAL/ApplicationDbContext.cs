// DbContext
using Microsoft.EntityFrameworkCore;
using Projeto.Modelo;
namespace Projeto.DAL;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Utilizador> Utilizadores { get; set; }
    public DbSet<Receita> Receitas { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
    public DbSet<ReceitaIngrediente> ReceitaIngredientes { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Classificacao> Classificacoes { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar chave composta em ReceitaIngrediente
        modelBuilder.Entity<ReceitaIngrediente>()
            .HasKey(ri => new { ri.ReceitaId, ri.IngredienteId });

        modelBuilder.Entity<ReceitaIngrediente>()
            .HasOne(ri => ri.Receita)
            .WithMany(r => r.Ingredientes)
            .HasForeignKey(ri => ri.ReceitaId);

        modelBuilder.Entity<ReceitaIngrediente>()
            .HasOne(ri => ri.Ingrediente)
            .WithMany(i => i.ReceitaIngredientes)
            .HasForeignKey(ri => ri.IngredienteId);

        // Configurar chave composta em Favorito
        modelBuilder.Entity<Favorito>()
            .HasKey(f => new { f.UtilizadorId, f.ReceitaId });

        modelBuilder.Entity<Favorito>()
            .HasOne(f => f.Utilizador)
            .WithMany(u => u.Favoritos)
            .HasForeignKey(f => f.UtilizadorId);

        modelBuilder.Entity<Favorito>()
            .HasOne(f => f.Receita)
            .WithMany(r => r.Favoritos)
            .HasForeignKey(f => f.ReceitaId);

        // Relacionamento de Receita -> Utilizador
        modelBuilder.Entity<Receita>()
            .HasOne(r => r.Utilizador)
            .WithMany(u => u.Receitas)
            .HasForeignKey(r => r.UtilizadorId);

        // Comentário -> Receita
        modelBuilder.Entity<Comentario>()
            .HasOne(c => c.Receita)
            .WithMany(r => r.Comentarios)
            .HasForeignKey(c => c.ReceitaId);

        // Comentário -> Utilizador
        modelBuilder.Entity<Comentario>()
            .HasOne(c => c.Utilizador)
            .WithMany(u => u.Comentarios)
            .HasForeignKey(c => c.UtilizadorId);

        // Classificação -> Receita
        modelBuilder.Entity<Classificacao>()
            .HasOne(c => c.Receita)
            .WithMany(r => r.Classificacoes)
            .HasForeignKey(c => c.ReceitaId);

        // Classificação -> Utilizador
        modelBuilder.Entity<Classificacao>()
            .HasOne(c => c.Utilizador)
            .WithMany(u => u.Classificacoes)
            .HasForeignKey(c => c.UtilizadorId);
    }
}