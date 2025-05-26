using Soat.Eleven.FastFood.Domain.Interfaces;

namespace Soat.Eleven.FastFood.Domain.Entidades.Base
{
    public class EntityBase : IEntity, IAuditable
    {
        public Guid Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime ModificadoEm { get; set; }
    }
}
