using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;
using QueryBuilder.Core.Translators;

namespace QueryBuilder.Core.Test.Translators;

public class TableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest test.TestClass as tc")]
    public void Table_BuildTableAndSchemaAndAlias(string expected)
    {
        var source = new QBContext();
        new TableTranslator("test", new Table("test", "TestClass", "tc")).Run(source);
        Assert.Equal(expected, source.Query.ToString());
    }
}
