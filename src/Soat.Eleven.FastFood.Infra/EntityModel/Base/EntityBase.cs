﻿namespace Soat.Eleven.FastFood.Adapter.Infra.EntityModel.Base;

public class EntityBase: TEntity, TAuditable
{
    public Guid Id { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
}
