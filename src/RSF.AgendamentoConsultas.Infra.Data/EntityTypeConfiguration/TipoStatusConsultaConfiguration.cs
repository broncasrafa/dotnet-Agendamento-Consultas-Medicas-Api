﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Infra.Data.EntityTypeConfiguration;

public class TipoStatusConsultaConfiguration : IEntityTypeConfiguration<TipoStatusConsulta>
{
    public void Configure(EntityTypeBuilder<TipoStatusConsulta> builder)
    {
        builder.ToTable("TipoStatusConsulta");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Descricao).IsRequired().HasMaxLength(255);
    }
}