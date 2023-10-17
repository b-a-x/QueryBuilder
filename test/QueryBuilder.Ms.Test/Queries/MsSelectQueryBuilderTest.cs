using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms.Test.Queries;

public class MsSelectQueryBuilderTest
{
    [Theory]
    [InlineData("\r\nselect tc.* \r\nfrom dbo.TestClass as tc")]
    public void Select_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source).Select(x => x.All());
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\nselect tc.* \r\nfrom dbo.TestClass as tc\r\nselect tc.* \r\nfrom dbo.TestClass as tc")]
    public void DoubleSelect_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source).Select(x => x.All()).Select(x => x.All());
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\nselect tc.* \r\nfrom dbo.TestClass as tc\r\nwhere tc.Id = @0 and tc.Name = @1 and tc.Age = @2 and tc.Timespan = @3")]
    public void SelectWhere_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source)
           .Select(x => x.All())
           .Where(x => x.EqualTo(y => y.Id, Guid.Empty).And()
                        .EqualTo(y => y.Name, null).And()
                        .EqualTo(y => y.Age, 10).And()
                        .EqualTo(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\nselect tc.* ,mtc.* \r\nfrom dbo.TestClass as tc\r\njoin dbo.MoreTestClass as mtc on tc.Id = mtc.Id")]
    public void SelectJoin_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass, MoreTestClass>(source)
            .Select(x => x.All(), 
                    y => y.All())
            .Join(x => x.EqualTo(x => x.Id, x => x.Id));
        Assert.Equal(expected, source.Query.ToString());
    }
}