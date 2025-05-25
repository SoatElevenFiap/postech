using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Soat.Eleven.FastFood.Application.Configuration;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Request;
using Soat.Eleven.FastFood.Application.DTOs.Usuarios.Response;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Infra.Repositories;

namespace Soat.Eleven.FastFood.Application.Services;

public class UsuarioService : BaseService<Usuario>, IUsuarioService
{
    private readonly IRepository<Cliente> _clienteRepositorio;
    private readonly IRepository<Usuario> _usuarioRepositorio;

    public UsuarioService(IRepository<Cliente> clienteRepositorio,
                          IRepository<Usuario> usuarioRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
        _usuarioRepositorio = usuarioRepositorio;
    }
    public async Task<ResultResponse> InserirCliente(CriarClienteRequestDto request)
    {
        var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();
        var existeCpf = (await _clienteRepositorio.FindAsync(x => x.Cpf == request.Cpf)).Any();

        if (existeEmail || existeCpf)
        {
            AddError("Usuário já existe");
            return SendError();
        }

        if (ValideRequest(request))
            return SendError();

        var usuario = new Usuario(request.Nome, request.Email, request.Senha, request.Telefone, PerfilUsuario.Cliente);
        usuario.CriarCliente(request.Cpf, request.DataDeNascimento);

        return Send((UsuarioClienteDto)await _usuarioRepositorio.AddAsync(usuario));
    }

    public async Task<ResultResponse> AtualizarCliente(Guid usuarioId, AtualizarClienteDto request)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId, x => x.Cliente);

        if (usuario is null)
        {
            AddError("Usuário não encontrado");
            return SendError();
        }

        if (request.Email != usuario.Email)
        {
            var existeEmail = (await _usuarioRepositorio.FindAsync(x => x.Email == request.Email)).Any();
            
            if (existeEmail)
            {
                AddError("Usuário já existe");
                return SendError();
            }
        }

        if (request.Cpf != usuario.Cliente.Cpf)
        {
            var existeCpf = (await _clienteRepositorio.FindAsync(x => x.Cpf == request.Cpf)).Any();

            if (existeCpf)
            {
                AddError("Usuário já existe");
                return SendError();
            }
        }

        if (ValideRequest(request))
            return SendError();

        usuario.Nome = request.Nome;
        usuario.Email = request.Email;
        usuario.Senha = request.Senha;
        usuario.Telefone = request.Telefone;
        usuario.Cliente.Cpf = request.Cpf;
        usuario.Cliente.DataDeNascimento = request.DataDeNascimento;

        await _usuarioRepositorio.UpdateAsync(usuario);

        return Send((UsuarioClienteDto)usuario);
    }

    public async Task<ResultResponse> GetCliente(Guid usuarioId)
    {
        var usuario = await _usuarioRepositorio.GetByIdAsync(usuarioId, u => u.Cliente);

        if (usuario is null)
            return null;

        return Send((UsuarioClienteDto)usuario);
    }
}
