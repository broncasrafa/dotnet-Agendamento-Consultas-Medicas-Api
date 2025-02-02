using Microsoft.AspNetCore.Identity;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Infra.Identity.Context;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await SeedRolesAsync(roleManager);
        await SeedUserAdminAsync(userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Enum.GetNames<ETipoPerfilAcesso>())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedUserAdminAsync(UserManager<ApplicationUser> userManager)
    {
        // Verifica se já existe um usuário com o e-mail especificado
        var existingUser = await userManager.FindByEmailAsync("rsfrancisco.applications@gmail.com");
        if (existingUser == null)
        {
            var defaultUserAdmin = new ApplicationUser(
                nomeCompleto: "Rafael Francisco",
                username: "rsfrancisco",
                documento: "22566719845",
                email: "rsfrancisco.applications@gmail.com",
                genero: "Masculino",
                telefone: "11960641048")
            {
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            // Crie o usuário administrador
            var createResult = await userManager.CreateAsync(defaultUserAdmin, "Admin@123");
            if (createResult.Succeeded)
            {
                // Adiciona o usuário ao perfil de Administrador
                await userManager.AddToRoleAsync(defaultUserAdmin, ETipoPerfilAcesso.Administrador.ToString());
            }
            else
            {
                throw new InvalidOperationException($"Erro ao criar o usuário administrador: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }
        }
    }
}