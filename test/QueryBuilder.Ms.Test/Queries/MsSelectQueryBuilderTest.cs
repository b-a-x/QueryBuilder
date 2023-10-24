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
        new MsSelectQueryBuilder<TestClass>(source).Select(x => x.All().Column(x => x.Id).As("qwe")).From();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\nselect tc.* \r\nfrom dbo.TestClass as tc\r\nselect tc.* \r\nfrom dbo.TestClass as tc")]
    public void DoubleSelect_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source).Select(x => x.All()).From().Select(x => x.All()).From();
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\nselect tc.* \r\nfrom dbo.TestClass as tc\r\nwhere tc.Id = @0 and tc.Name = @1 and tc.Age = @2 and tc.Timespan = @3")]
    public void SelectWhere_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source)
           .Select(x => x.All())
           .From()
           .Where(x => x.EqualTo(y => y.Id, Guid.Empty).And()
                        .EqualTo(y => y.Name, null).And()
                        .EqualTo(y => y.Age, 10).And()
                        .EqualTo(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, source.Query.ToString());
    }

    [Theory]
    [InlineData("\r\nselect tc.* ,mtc.* \r\nfrom dbo.TestClass as tc\r\njoin dbo.MoreTestClass as mtc on tc.Id = mtc.Id\r\nwhere (tc.Id = @0 and mtc.Age >= @1 and mtc.Name is null)")]
    public void SelectJoin_TwoType_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsSelectQueryBuilder<TestClass>(source)
            .Select(x => { 
                x.All();
                x.Bind<MoreTestClass>().All();
            })
            .From()
            .Join<MoreTestClass>(x => x.EqualTo(x => x.Id, x => x.Id))
            .Where(x =>
            {
                x.Bracket(() =>
                {
                    x.EqualTo(x => x.Id, Guid.Empty).And();
                    x.Bind<MoreTestClass>().MoreEqualTo(x => x.Age, 1)
                                           .And()
                                           .IsNull(x => x.Name);
                });
                
            });

        Assert.Equal(expected, source.Query.ToString());
    }
}