using Soat.Eleven.FastFood.Application.DTOs.Usuario.Request;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IRepository<Cliente> _clienteRepositorio;
    private readonly IRepository<Usuario> _usuarioRepositorio;

    public UsuarioService(IRepository<Cliente> clienteRepositorio,
                                 IRepository<Usuario> usuarioRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }
    public async Task<Usuario> InserirCliente(CriarClienteDto request)
    {
        var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();
        var existeCpf = (await _clienteRepositorio.FindAsync(x => x.Cpf == request.Cpf)).Any();

        if (existeEmail || existeCpf)
        {
            throw new ArgumentException("Usuário já existe");
        }

        var usuario = new Usuario(request.Nome, request.Email, request.Senha, request.Telefone, PerfilUsuario.Cliente);
        usuario.CriarCliente(request.Cpf, request.DataDeNascimento);

        return await _usuarioRepositorio.AddAsync(usuario);
    }

    public async Task<Usuario> AtualizarCliente(Guid usuarioId, AtualizarClienteDto request)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId);

        if (usuario is null)
            throw new ArgumentNullException(nameof(usuario));

        var cliente = (await _clienteRepositorio.FindAsync(c => c.Usuario == usuario)).First();

        if (request.Email != usuario.Email)
        {
            var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();
            
            if (existeEmail)
            {
                throw new ArgumentException("Usuário já existe");
            }
        }

        if (request.Cpf != cliente.Cpf)
        {
            var existeCpf = (await _clienteRepositorio.FindAsync(x => x.Cpf == request.Cpf)).Any();

            if (existeCpf)
            {
                throw new ArgumentException("Usuário já existe");
            }
        }

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Senha = request.Senha;
        usuario.Telefone = request.Telefone;
        usuario.Cliente.Cpf = request.Cpf;
        usuario.Cliente.DataDeNascimento = request.DataDeNascimento;

        await _usuarioRepositorio.UpdateAsync(usuario);

        return usuario;
    }
}
