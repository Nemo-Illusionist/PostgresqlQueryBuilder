using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace QueryBuilder.CodeGenerator
{
    [Generator]
    public class PgJoinGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = Gen();
            context.AddSource("PgJoinGenerator", SourceText.From(sourceBuilder, Encoding.UTF8));
        }

        private string Gen()
        {
            const string template = @"
    public interface IPgJoin<{0}> : IPgJoin<{1}>
    {{
        T{2} Join{3} {{ get; }}
    }}
";
            var sourceBuilder = new StringBuilder(@"
namespace QueryBuilder.Contract
{
");
            var ts = new StringBuilder("T1, T2");
            for (int i = 3; i < 17; i++)
            {
                var outTs = string.Join(", ", Enumerable.Range(1, i).Select(x => $"out T{x}"));
                var func = string.Format(CultureInfo.InvariantCulture, template, outTs, ts.ToString(), i, i - 1);
                sourceBuilder.Append(func);
                ts.Append($", T{i}");
            }

            sourceBuilder.Append(@"
}
");
            return sourceBuilder.ToString();
        }
    }
}