using QueryBuilder.Core.Helpers;
using System.Text;

namespace QueryBuilder.Core.Context;

public class QBContext
{
    public readonly StringBuilder Query = new StringBuilder();
    public readonly Parameters Parameters = new Parameters();
}
