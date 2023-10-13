using QueryBuilder.Core.Helpers;

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

    private static void TestMethod<T>() where T : ITableBuilder
    {
        var table = T.GetTable();
        var schema = table.Schema;
        var tableName = table.TableName;
        var alias = table.Alias;
    }
}
