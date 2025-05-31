using Soat.Eleven.FastFood.Application.DTOs.Common;
using Soat.Eleven.FastFood.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Soat.Eleven.FastFood.Application.Services
{
    public class ImagemService : IImagemService
    {
        private readonly IArmazenamentoArquivoService _armazenamentoArquivo;
        private readonly ILogger<ImagemService> _logger;

        public ImagemService(
            IArmazenamentoArquivoService armazenamentoArquivo, 
            ILogger<ImagemService> logger)
        {
            _armazenamentoArquivo = armazenamentoArquivo;
            _logger = logger;
        }

        public async Task<string> UploadImagemAsync(string diretorio, string identificador, ArquivoUploadDTO imagem)
        {
            if (imagem == null || imagem.Conteudo == null)
            {
                _logger.LogError("Tentativa de upload de imagem inválida para o diretório {Diretorio}", diretorio);
                throw new ArgumentException("Nenhuma imagem foi enviada.");
            }

            var nomeArquivo = await _armazenamentoArquivo.SalvarArquivoAsync(diretorio, identificador, imagem);
            return nomeArquivo;
        }

        public async Task RemoverImagemAsync(string diretorio, string identificador)
        {
            await _armazenamentoArquivo.RemoverArquivoAsync(diretorio, identificador);
        }

        public async Task<string?> ObterUrlImagemAsync(string diretorio, string nomeArquivo)
        {
            if (string.IsNullOrEmpty(nomeArquivo))
                return null;

            return await _armazenamentoArquivo.ObterUrlArquivoAsync(diretorio, nomeArquivo);
        }

        public string GerarNomeArquivo(string base64)
        {
            if (string.IsNullOrEmpty(base64)) return string.Empty;

            var extensao = ObterExtensaoBase64(base64);
            if (extensao == null)
                throw new ArgumentException("Formato de imagem inválido. Apenas imagens JPG e PNG são permitidas.");

            return $"{Guid.NewGuid()}{extensao}";
        }

        public string? ObterExtensaoBase64(string? base64)
        {
            if (string.IsNullOrEmpty(base64))
                return null;

            if (base64.StartsWith("data:image/jpeg;base64,"))
                return ".jpg";
            if (base64.StartsWith("data:image/png;base64,"))
                return ".png";

            return null;
        }
    }
} 