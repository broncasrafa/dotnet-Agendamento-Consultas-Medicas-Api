using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Data.EntityTypeConfiguration;

public class PerguntaRespostaReacaoConfiguration : IEntityTypeConfiguration<PerguntaRespostaReacao>
{
    public void Configure(EntityTypeBuilder<PerguntaRespostaReacao> builder)
    {
        builder.ToTable("PerguntaRespostaReacao");

        builder.HasKey(c => c.PerguntaRespostaReacaoId);

        builder.Property(c => c.RespostaId).IsRequired();
        builder.Property(c => c.PacienteId).IsRequired();
        builder.Property(e => e.Reacao).HasConversion<string>().HasColumnType("varchar(10)").HasColumnName("TipoReacao").IsRequired();        
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("datetime").HasDefaultValueSql("(getdate())");


        builder.HasIndex(e => new { e.RespostaId, e.PacienteId })
            .IsUnique()
            .HasDatabaseName("IX_Reacao_Resposta_Paciente");

        builder.HasOne(resp => resp.Resposta)
            .WithMany(reac => reac.Reacoes)
            .HasForeignKey(reac => reac.RespostaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}