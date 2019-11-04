using System;

public class Transaction : BaseEntity
{
    public double Value { get; set; }
    public string Description { get; set; }
    public DateTime Created_at { get; set; }
}