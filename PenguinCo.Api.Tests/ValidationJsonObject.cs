using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PenguinCo.Api.Tests;

public class ValidationJsonObject
{
    [Required]
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [Required]
    [JsonPropertyName("errors")]
    public Dictionary<string, List<string>> Errors { get; set; } = new();
}
