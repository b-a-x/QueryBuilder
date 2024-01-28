namespace QueryBuilder.Core.Entity;

public interface IHasTable
{
    static abstract Table GetTable();
}

public readonly record struct Table(string Schema, string Name, string Alias);
