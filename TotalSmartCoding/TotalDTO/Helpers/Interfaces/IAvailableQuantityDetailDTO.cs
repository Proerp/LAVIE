using System;

namespace TotalDTO.Helpers.Interfaces
{
    public interface IAvailableQuantityDetailDTO : IQuantityDetailDTO
    {
        decimal QuantityAvailable { get; set; }
        decimal LineVolumeAvailable { get; set; }
    }
}
