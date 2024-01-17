using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms.Test.Queris;

public class MsDeleteQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ndelete dbo.TestClass as tc")]
    public void Delete_BuildSql(string expected)
    {
        var source = new QueryBuilderContext();
        new MsDeleteQueryBuilder<TestClass>(source).Delete();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete dbo.TestClass as tc\r\ndelete dbo.TestClass as tc")]
    public void DoubleDelete_BuildSql(string expected)
    {
        var source = new QueryBuilderContext();
        new MsDeleteQueryBuilder<TestClass>(source).Delete().Delete();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\ndelete dbo.TestClass as tc\r\nwhere tc.Id = @0 and tc.Name = @1 and tc.Age = @2 and tc.Timespan = @3")]
    public void DeleteWhere_BuildSql(string expected)
    {
        var source = new QueryBuilderContext();
        new MsDeleteQueryBuilder<TestClass>(source)
           .Delete()
           .Where(x => x.EqualTo(y => y.Id, Guid.Empty).And()
                        .EqualTo(y => y.Name, null).And()
                        .EqualTo(y => y.Age, 10).And()
                        .EqualTo(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, source.Query.ToString());
    }
}
