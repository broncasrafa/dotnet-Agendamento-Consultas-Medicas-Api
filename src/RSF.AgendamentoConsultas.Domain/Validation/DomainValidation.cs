using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RSF.AgendamentoConsultas.Domain.Validation;

public static class DomainValidation
{
    public static void NotNullOrEmpty(string? property, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(property))
        {
            throw new DomainException($"{fieldName} não pode ser nulo ou vazio.");
        }
    }
}