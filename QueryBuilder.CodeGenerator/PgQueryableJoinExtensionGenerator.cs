using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace QueryBuilder.CodeGenerator
{
    [Generator]
    public class PgQueryableJoinExtensionGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = Gen();
            context.AddSource("PgQueryableJoinExtensionGenerator", SourceText.From(sourceBuilder, Encoding.UTF8));
        }

        private string Gen()
        {
            const string classTemplate = @"
using System;
using System.Linq.Expressions;
using QueryBuilder.Contract;
using QueryBuilder.Helpers;

namespace QueryBuilder.Extension.Queryable
{{
    public static partial class PgQueryableJoinExtension
    {{
{0}
    }}
}}";
            var sourceBuilder = new StringBuilder();
            AddCrossJoin(sourceBuilder);
            AddJoinMethodWithExpression(sourceBuilder, "Join");
            AddJoinMethodWithExpression(sourceBuilder, "LeftJoin");
            AddJoinMethodWithExpression(sourceBuilder, "RightJoin");
            AddJoinMethodWithExpression(sourceBuilder, "FullJoin");

            return string.Format(CultureInfo.InvariantCulture, classTemplate, sourceBuilder);
        }


        private void AddJoinMethodWithExpression(StringBuilder sourceBuilder, string name)
        {
            const string joinTemplate = @"
        public static IPgFromQueryable<IPgJoin<{0}, TAdd>> {1}<{0}, TAdd>(
            this IPgFromQueryable<IPgJoin<{0}>> queryable,
            Expression<Func<{0}, TAdd, bool>> expression,
            IEmptyGeneric<TAdd> _ = null)
        {{
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof({1}), typeof(IPgJoin<{0}, TAdd>), queryable.Node, expression);
            return (IPgFromQueryable<IPgJoin<{0}, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<{0}, TAdd>>(node);
        }}
";
            AddJoinMethod(sourceBuilder, joinTemplate, name);
        }

        private void AddCrossJoin(StringBuilder sourceBuilder)
        {
            const string joinTemplate = @"
        public static IPgFromQueryable<IPgJoin<{0}, TAdd>> {1}<{0}, TAdd>(
            this IPgFromQueryable<IPgJoin<{0}>> queryable,
            IEmptyGeneric<TAdd> _ = null)
        {{
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            var node = new PgQueryNode(nameof({1}), typeof(IPgJoin<{0}, TAdd>), queryable.Node);
            return (IPgFromQueryable<IPgJoin<{0}, TAdd>>) queryable.Provider.CreateQuery<IPgJoin<{0}, TAdd>>(node);
        }}
";
            AddJoinMethod(sourceBuilder, joinTemplate, "CrossJoin");
        }

        private void AddJoinMethod(StringBuilder sourceBuilder, string joinTemplate, string name)
        {
            var maxT = 15;
            sourceBuilder.Append($"\t\t#region {name}\n");

            for (int i = 1; i < maxT; i++)
            {
                var ts = string.Join(", ", Enumerable.Range(1, i + 1).Select(x => $"T{x}"));
                var joinTn = string.Format(CultureInfo.InvariantCulture, joinTemplate, ts, name);
                sourceBuilder.Append(joinTn);
            }

            sourceBuilder.Append("\t\t#endregion\n");
        }
    }
}