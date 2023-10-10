using System.Collections;

namespace QueryBuilder.Core.Helpers
{
    public class Parameter
    {
        public Parameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }

    public class Parameters : IEnumerable<KeyValuePair<string, object>>
    {
        private int _offset = 0;
        private readonly List<KeyValuePair<string, object>> _parameters = new List<KeyValuePair<string, object>>();

        public Parameters Add(object value, out string name)
        {
            name = _offset++.ToString();
            _parameters.Add(KeyValuePair.Create(name, value));
            return this;
        }

        public void Add(Parameters parameters)
        {
            if (parameters != null && parameters._parameters.Any())
            {
                _parameters.AddRange(parameters._parameters);
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var spryParameter in _parameters)
            {
                yield return spryParameter;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
