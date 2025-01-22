using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;


namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class EspecialistaLocalAtendimentoConfiguration : IEntityTypeConfiguration<EspecialistaLocalAtendimento>
{
    public void Configure(EntityTypeBuilder<EspecialistaLocalAtendimento> builder)
    {
        builder.ToTable("EspecialistaLocalAtendimento");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.EspecialistaId).IsRequired();
        builder.Property(c => c.Nome).HasColumnType("varchar(255)");
        builder.Property(c => c.Logradouro).HasColumnType("varchar(455)");
        builder.Property(c => c.Complemento).HasColumnType("varchar(455)");
        builder.Property(c => c.Bairro).HasColumnType("varchar(255)");
        builder.Property(c => c.Cep).HasColumnType("varchar(8)");
        builder.Property(c => c.Cidade).HasColumnType("varchar(255)");
        builder.Property(c => c.Estado).HasColumnType("varchar(45)");
        builder.Property(c => c.Preco).HasColumnType("decimal(18,2)"); ;
        builder.Property(c => c.PrecoDescricao).HasColumnType("varchar(255)");
        builder.Property(c => c.TipoAtendimento).HasColumnType("varchar(255)");
        builder.Property(c => c.Telefone).HasColumnType("varchar(455)");
        builder.Property(c => c.Whatsapp).HasColumnType("varchar(455)");

        builder.HasOne(e => e.Especialista)
            .WithMany(l => l.LocaisAtendimento)
            .HasForeignKey(e => e.EspecialistaId);
    }
}
