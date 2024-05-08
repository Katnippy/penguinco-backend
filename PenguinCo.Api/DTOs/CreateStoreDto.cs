using System.ComponentModel.DataAnnotations;

namespace PenguinCo.Api.DTOs;

public record CreateStoreDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(75)] string Address,
    [Required] List<Stock> Stock,
    [Required] DateOnly Updated
);
