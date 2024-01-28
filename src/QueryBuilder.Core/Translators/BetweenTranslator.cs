using QueryBuilder.Core.Context;
using QueryBuilder.Core.Entity;

namespace QueryBuilder.Core.Translators;

public readonly ref struct BetweenTranslator
{
    private readonly string _columnLeft;
    private readonly string _columnRigthOne;
    private readonly Table _tableLeft;
    private readonly Table _tableRigthOne;
    public BetweenTranslator(string columnLeft, string columnRigthOne, Table tableLeft, Table tableRigthOne)
    {
        _columnLeft = columnLeft;
        _columnRigthOne = columnRigthOne;
        _tableLeft = tableLeft;
        _tableRigthOne = tableRigthOne;
    }

    public void Run(QBContext source)
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
