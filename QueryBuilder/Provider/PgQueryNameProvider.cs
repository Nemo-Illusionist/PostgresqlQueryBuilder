using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using QueryBuilder.Contract;
using QueryBuilder.Entities;

namespace QueryBuilder.Provider
{
    internal class PgQueryNameProvider : IPgQueryNameProvider
    {
        private readonly IReadOnlyCollection<TableAlias> _aliases;
        private readonly IReadOnlyDictionary<string, string> _mapDict;

        public PgQueryNameProvider(
            IReadOnlyCollection<TableAlias> aliases,
            PgQueryNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            _aliases = aliases ?? throw new ArgumentNullException(nameof(aliases));
            _mapDict = GetMapDict(node);
        }

        private IReadOnlyDictionary<string, string> GetMapDict(PgQueryNode node)
        {
            var paramName = node.Expressions[0].Parameters[0].Name;
            return _aliases.Count switch
            {
                > 1 => _aliases
                    .Select((ta, i) => new
                    {
                        key = i != 0 ? $"{paramName}.Join{i}" : $"{paramName}.From",
                        value = ta.Alias
                    })
                    .ToDictionary(x => x.key, x => x.value),
                1 => new Dictionary<string, string> {{paramName, _aliases.Single().Alias}},
                _ => throw new NotImplementedException()
            };
        }

        public string GetTableName(Expression expression)
        {
            string key;
            if (_aliases.Count > 1)
            {
                key = expression switch
                {
                    MemberExpression me when me.Expression is MemberExpression => me.Expression.ToString(),
                    _ => throw new ArgumentOutOfRangeException(nameof(expression), expression, null)
                };
            }
            else
            {
                key = expression switch
                {
                    ParameterExpression pe => pe.ToString(),
                    MemberExpression me when me.Expression is ParameterExpression => me.Expression.ToString(), 
                    _ => throw new ArgumentOutOfRangeException(nameof(expression), expression, null)
                };
            }

            return GetTableName(key);
        }

        public string GetTableName(string key)
        {
            return _mapDict[key];
        }
    }
}