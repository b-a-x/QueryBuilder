using QueryBuilder.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Ms.Translators;

public readonly ref struct ThenTranslator
{
    public void Run(QBContext source)
    {
        source.Query.Append(" then ");
    }
}
