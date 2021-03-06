using System.Collections.Generic;
using System.Linq.Expressions;

namespace QueryBuilder.Helpers
{
    public static class BinaryOperatorMapper
    {
        private static readonly Dictionary<ExpressionType, string> _map = new()
        {
            // {ExpressionType.Add, ""},
            // {ExpressionType.AddChecked, ""},
            {ExpressionType.And, "&"},
            {ExpressionType.AndAlso, "AND"},
            // {ExpressionType.ArrayLength, ""},
            // {ExpressionType.ArrayIndex, ""},
            // {ExpressionType.Call, ""},
            // {ExpressionType.Coalesce, ""},
            // {ExpressionType.Conditional, ""},
            // {ExpressionType.Constant, ""},
            // {ExpressionType.Convert, ""},
            // {ExpressionType.ConvertChecked, ""},
            {ExpressionType.Divide, "/"},
            {ExpressionType.Equal, "="},
            {ExpressionType.ExclusiveOr, "#"},
            {ExpressionType.GreaterThan, ">"},
            {ExpressionType.GreaterThanOrEqual, ">="},
            // {ExpressionType.Invoke, ""},
            // {ExpressionType.Lambda, ""},
            {ExpressionType.LeftShift, "<<"},
            {ExpressionType.LessThan, "<"},
            {ExpressionType.LessThanOrEqual, "<="},
            // {ExpressionType.ListInit, ""},
            // {ExpressionType.MemberAccess, ""},
            // {ExpressionType.MemberInit, ""},
            {ExpressionType.Modulo, "%"},
            {ExpressionType.Multiply, "*"},
            {ExpressionType.MultiplyChecked, "*"},
            {ExpressionType.Negate, "-"},
            {ExpressionType.UnaryPlus, "+"},
            {ExpressionType.NegateChecked, "-"},
            // {ExpressionType.New, ""},
            // {ExpressionType.NewArrayInit, ""},
            // {ExpressionType.NewArrayBounds, ""},
            {ExpressionType.Not, "NOT"},
            {ExpressionType.NotEqual, "NOT"},
            {ExpressionType.Or, "|"},
            {ExpressionType.OrElse, "OR"},
            // {ExpressionType.Parameter, ""},
            // {ExpressionType.Power, ""},
            // {ExpressionType.Quote, ""},
            {ExpressionType.RightShift, ">>"},
            {ExpressionType.Subtract, "-"},
            {ExpressionType.SubtractChecked, "-"},
            // {ExpressionType.TypeAs, ""},
            // {ExpressionType.TypeIs, ""},
            // {ExpressionType.Assign, ""},
            // {ExpressionType.Block, ""},
            // {ExpressionType.DebugInfo, ""},
            {ExpressionType.Decrement, "--"},
            // {ExpressionType.Dynamic, ""},
            // {ExpressionType.Default, ""},
            // {ExpressionType.Extension, ""},
            // {ExpressionType.Goto, ""},
            {ExpressionType.Increment, "++"},
            // {ExpressionType.Index, ""},
            // {ExpressionType.Label, ""},
            // {ExpressionType.RuntimeVariables, ""},
            // {ExpressionType.Loop, ""},
            // {ExpressionType.Switch, ""},
            // {ExpressionType.Throw, ""},
            // {ExpressionType.Try, ""},
            // {ExpressionType.Unbox, ""},
            // {ExpressionType.AddAssign, ""},
            // {ExpressionType.AndAssign, ""},
            // {ExpressionType.DivideAssign, ""},
            // {ExpressionType.ExclusiveOrAssign, ""},
            // {ExpressionType.LeftShiftAssign, ""},
            // {ExpressionType.ModuloAssign, ""},
            // {ExpressionType.MultiplyAssign, ""},
            // {ExpressionType.OrAssign, ""},
            // {ExpressionType.PowerAssign, ""},
            // {ExpressionType.RightShiftAssign, ""},
            // {ExpressionType.SubtractAssign, ""},
            // {ExpressionType.AddAssignChecked, ""},
            // {ExpressionType.MultiplyAssignChecked, ""},
            // {ExpressionType.SubtractAssignChecked, ""},
            // {ExpressionType.PreIncrementAssign, ""},
            // {ExpressionType.PreDecrementAssign, ""},
            // {ExpressionType.PostIncrementAssign, ""},
            // {ExpressionType.PostDecrementAssign, ""},
            // {ExpressionType.TypeEqual, ""},
            {ExpressionType.OnesComplement, "~"},
            {ExpressionType.IsTrue, "IS TRUE"},
            {ExpressionType.IsFalse, "IS FALSE"},
        };


        public static string Map(ExpressionType type)
        {
            return _map[type];
        }
    }
}