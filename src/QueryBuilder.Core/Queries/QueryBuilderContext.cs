using QueryBuilder.Core.Helpers;
using System.Text;

namespace QueryBuilder.Core.Queries;

public class QueryBuilderContext
{
    public readonly StringBuilder Query = new StringBuilder();
    public readonly Parameters Parameters = new Parameters();
}
