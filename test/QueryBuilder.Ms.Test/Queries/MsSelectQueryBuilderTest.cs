using QueryBuilder.Ms.Helpers;

namespace QueryBuilder.Ms.Test.Queries;

public class MsSelectQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete dbo.TestClass")]
    public void Select_BuildSql(string expected)
    {
        TestMethod<TestClass>();
        TestMethod<MoreTestClass>();
        //var source = new QueryBuilderSource();
        //new MsSelectQueryBuilder<TestClass>(source).Select(null).From();
        //Assert.Equal(expected, source.Query.ToString());
    }

    private static void TestMethod<T>() where T : IMsTableTranslator
    {
        var schema = T.Schema;
        var table = T.Table;
        var alias = T.Alias;
    }
}
