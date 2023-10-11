using QueryBuilder.Core.Queries;
using QueryBuilder.Core.Translators;
using QueryBuilder.Test;

namespace QueryBuilder.Core.Test.Translators;

public class TableTranslatorTest
{
    [Theory]
    [InlineData("\r\ntest dbo.TestClass")]
    public void Table_Build(string expected)
    {
        var source = new QueryBuilderSource();
        TableTranslator<TestClass>.Make("test", "dbo", null, null).Run(source);
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ntest test.Test as test")]
    public void Table_BuildTableAndSchemaAndAlias(string expected)
    {
        var source = new QueryBuilderSource();
        TableTranslator<TestClass>.Make("test", "test", "Test", "test").Run(source);
        Assert.Equal(expected, source.Query.ToString());
    }
}
