namespace QueryBuilder.Core.Helpers;

public interface ITableBuilder
{
    static abstract TableBuilder GetTable();
}

public readonly record struct TableBuilder(string Schema, string TableName, string Alias);
