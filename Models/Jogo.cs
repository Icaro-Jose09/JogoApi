using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JogoApi.Models;

public class Jogo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required]
    [BsonElement("titulo")]
    public string Titulo { get; set; } = null!;

    [Required]
    [BsonElement("plataforma")]
    public string Plataforma { get; set; } = null!;

    [Required]
    [BsonElement("genero")]
    public string Genero { get; set; } = null!;

    [Range(0, 100000)]
    [BsonElement("preco")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Preco { get; set; }

    [Range(1950, 2100)]
    [BsonElement("anoLancamento")]
    public int AnoLancamento { get; set; }

    [Range(0, int.MaxValue)]
    [BsonElement("estoque")]
    public int Estoque { get; set; }
}