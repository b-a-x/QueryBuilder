using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core.Test.Translators;

public class TableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest test.MoreTestClass")]
    public void Table_Build(string expected)
    {
        var source = new QueryBuilderSource();
        TableTranslator.Make("test", new TableBuilder("test", "MoreTestClass", null)).Run(source);
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ntest test.TestClass as tc")]
    public void Table_BuildTableAndSchemaAndAlias(string expected)
    {
        var source = new QueryBuilderSource();
        TableTranslator.Make("test", new TableBuilder("test", "TestClass", "tc")).Run(source);
        Assert.Equal(expected, source.Query.ToString());
    }
}
