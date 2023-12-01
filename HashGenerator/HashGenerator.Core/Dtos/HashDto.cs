using System.ComponentModel.DataAnnotations;

namespace HashGenerator.Core.Dtos;

public class HashDto : CreateHashDto
{
    public int Id { get; set; }
}

public class CreateHashDto
{
    [Required]
    public DateTimeOffset Date { get; set; }

    [Required]
    public string Sha1 { get; set; }
}

public class GroupHashDto
{
    public long Count { get; set; }

    public DateTimeOffset Date { get; set; }
}

