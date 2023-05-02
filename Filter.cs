
namespace LogAnalysisConsole
{

    /// <summary>
    /// Operators for filters
    /// </summary>
    internal enum FilterOperator
    {

        /// <summary>
        /// Operator is not set
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// "="
        /// </summary>
        Equals = 1,

        /// <summary>
        /// "&lt;&gt;"
        /// </summary>
        NotEquals,

        /// <summary>
        /// "&gt;"
        /// </summary>
        GreaterThan,

        /// <summary>
        /// "&lt;"
        /// </summary>
        LesserThan,

        /// <summary>
        /// "&gt;="
        /// </summary>
        GreaterThanOrEqualTo,

        /// <summary>
        /// "&lt;="
        /// </summary>
        LesserThanOrEqualTo,

        /// <summary>
        /// In the set
        /// </summary>
        In,

        /// <summary>
        /// Not in the set
        /// </summary>
        NotIn,

        /// <summary>
        /// Like given phrase
        /// </summary>
        Like,

        /// <summary>
        /// Not like given phrase
        /// </summary>
        NotLike
    }

    internal class Filter
    {
        private string _name = null;
        private FilterOperator _operator = FilterOperator.Unknown;
        private string _value = null;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public FilterOperator Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                _operator = value;
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// Return the value of the operator (to be used in queries) from the Enum
        /// </summary>
        /// <param name="op">Enum operator</param>
        /// <returns>Actual operator</returns>
        public static string GetOperatorValue(FilterOperator op)
        {
            string _operator = null;
            switch (op)
            {
                case FilterOperator.Equals: _operator = "="; break;
                case FilterOperator.NotEquals: _operator = "<>"; break;
                case FilterOperator.GreaterThan: _operator = ">"; break;
                case FilterOperator.GreaterThanOrEqualTo: _operator = ">="; break;
                case FilterOperator.LesserThan: _operator = "<"; break;
                case FilterOperator.LesserThanOrEqualTo: _operator = "<="; break;
                case FilterOperator.In: _operator = " IN "; break;
                case FilterOperator.NotIn: _operator = " NOT IN "; break;
                case FilterOperator.Like: _operator = " LIKE "; break;
                case FilterOperator.NotLike: _operator = " NOT LIKE "; break;
            }

            return _operator;
        }

        /// <summary>
        /// Returns the Enum operator value given the string equivalent
        /// </summary>
        /// <param name="op">String equivalent</param>
        /// <returns>Enum representation</returns>
        public static FilterOperator GetOperatorName(string op)
        {
            FilterOperator _operator = FilterOperator.Unknown;
            switch (op.ToLower())
            {
                case "=": _operator = FilterOperator.Equals; break;
                case "<>": _operator = FilterOperator.NotEquals; break;
                case ">": _operator = FilterOperator.GreaterThan; break;
                case ">=": _operator = FilterOperator.GreaterThanOrEqualTo; break;
                case "<": _operator = FilterOperator.LesserThan; break;
                case "<=": _operator = FilterOperator.LesserThanOrEqualTo; break;
                case "in": _operator = FilterOperator.In; break;
                case "not in": _operator = FilterOperator.NotIn; break;
                case "like": _operator = FilterOperator.Like; break;
                case "not like": _operator = FilterOperator.NotLike; break;
            }

            return _operator;
        }
    }
}
