using QueryBuilder.Core.Helpers;
using QueryBuilder.Core.Queries;

namespace QueryBuilder.Core.Translators;

public readonly ref struct BetweenTranslator
{
    private readonly string _columnLeft;
    private readonly string _columnRigthOne;
    private readonly TableBuilder _tableLeft;
    private readonly TableBuilder _tableRigthOne;
    public BetweenTranslator(string columnLeft, string columnRigthOne, TableBuilder tableLeft, TableBuilder tableRigthOne)
    {
        _columnLeft = columnLeft;
        _columnRigthOne = columnRigthOne;
        _tableLeft = tableLeft;
        _tableRigthOne = tableRigthOne;
    }

    public void Run(QueryBuilderSource source)
    {
        source.Query.Append(_tableLeft.Alias)
                    .Append(".")
                    .Append(_columnLeft)
                    .Append(" between ")
                    .Append(_tableRigthOne.Alias)
                    .Append(".")
                    .Append(_columnRigthOne)
                    .Append(" and ");
    }
}
