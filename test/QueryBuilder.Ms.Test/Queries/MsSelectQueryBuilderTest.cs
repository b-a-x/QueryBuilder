using QueryBuilder.Core.Queries;
using QueryBuilder.Ms.Queries;

namespace QueryBuilder.Ms.Test.Queries;

public class MsSelectQueryBuilderTest
{
    [Theory]
    [InlineData("\r\nselect tc.* ,tc.Id as qwe \r\nfrom dbo.TestClass as tc")]
    public void Select_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source).Select(x => x.All().Field(x => x.Id).As("qwe"));
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
    [InlineData("\r\nselect tc.* ,mtc.* \r\nfrom dbo.TestClass as tc\r\njoin dbo.MoreTestClass as mtc on tc.Id = mtc.Id\r\nwhere tc.Id = @0 and mtc.Age = @1")]
    public void SelectJoin_TwoType_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source)
            .Select(x => { 
                x.All();
                x.Bind<MoreTestClass>().All();
            })
            .Join<MoreTestClass>(x => x.EqualTo(x => x.Id, x => x.Id))
            .Where(x =>
            {
                x.EqualTo(y => y.Id, Guid.Empty).And();
                x.Bind<MoreTestClass>().EqualTo(y => y.Age, 1);
            });

        Assert.Equal(expected, source.Query.ToString());
    }
}