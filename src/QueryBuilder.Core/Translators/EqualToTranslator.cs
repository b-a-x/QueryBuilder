using QueryBuilder.Core.Queris;


namespace QueryBuilder.Core.Translators
{
    public class EqualToTranslator<T> : Translator
    {
        private string _columnName;
        private object _value;

        public EqualToTranslator(string columnName, object value)
        {
            _value = value;
            _columnName = columnName;
        }

        public override void Run(QueryBuilderSource source)
        {
            source.Parameters.Add(_value, out string name);
            source.Query.Append(_columnName).Append(" = @").Append(name);
        }
    }
}