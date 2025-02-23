﻿using Microsoft.Extensions.DependencyModel;

using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class PacienteDependentePlanoMedico
{
    public int PlanoMedicoId { get; private set; }
    public string NomePlano { get; private set; }
    public string NumCartao { get; private set; }
    public int DependenteId { get; private set; }
    public int ConvenioMedicoId { get; private set; }
    public bool? Ativo { get; set; }

    public PacienteDependente Dependente { get; set; }
    public ConvenioMedico ConvenioMedico { get; set; }


    protected PacienteDependentePlanoMedico()
    {
    }

    public PacienteDependentePlanoMedico(string nomePlano, string numCartao, int dependenteId, int convenioMedicoId)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;
        DependenteId = dependenteId;
        ConvenioMedicoId = convenioMedicoId;
        Ativo = true;

        Validate();
    }

    public void Update(string nomePlano, string numCartao, int convenioMedicoId)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;
        ConvenioMedicoId = convenioMedicoId;
        Dependente = null;
        ConvenioMedico = null;

        Validate();
    }

    public void ChangeStatus(bool status)
    {
        Ativo = status;
        Dependente = null;
        ConvenioMedico = null;
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(NomePlano, nameof(NomePlano));
        DomainValidation.NotNullOrEmpty(NumCartao, nameof(NumCartao));
        DomainValidation.IdentifierGreaterThanZero(DependenteId, nameof(DependenteId));
        DomainValidation.IdentifierGreaterThanZero(ConvenioMedicoId, nameof(ConvenioMedicoId));
    }    
}