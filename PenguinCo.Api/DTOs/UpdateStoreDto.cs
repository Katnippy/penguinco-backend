using System.ComponentModel.DataAnnotations;

namespace PenguinCo.Api.DTOs;

public record class UpdateStoreDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(75)] string Address,
    [Required] List<Stock> Stock,
    [Required] DateOnly Updated
);
