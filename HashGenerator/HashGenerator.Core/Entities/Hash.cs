namespace HashGenerator.Core.Entities;

public class Hash
{
    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string Sha1 { get; set; }
}