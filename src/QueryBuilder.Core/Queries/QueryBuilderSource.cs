using QueryBuilder.Core.Helpers;
using System.Text;

namespace QueryBuilder.Core.Queries;

public class QueryBuilderSource
{
    public readonly StringBuilder Query = new StringBuilder();
    public readonly Parameters Parameters = new Parameters();
}
