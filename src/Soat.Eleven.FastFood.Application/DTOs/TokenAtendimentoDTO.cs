using Soat.Eleven.FastFood.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soat.Eleven.FastFood.Application.DTOs
{
    public class TokenAtendimentoDTO
    {
        public Guid TokenId { get; set; }
        public Guid? ClienteId { get; set; }
        public string? Cpf { get; set; }
        public DateTime CriadoEm { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
