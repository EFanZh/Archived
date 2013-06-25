using Interpreter.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Semantics
{
    internal class Analyzer
    {
        private const string keyword_define = "define";
        private const string keyword_lambda = "lambda";
        private const string keyword_if = "if";
        private static string[] reservered_symbols = new[] { keyword_define, keyword_lambda, keyword_if };

        private static IReadOnlyList<IExpression> Analyze(IEnumerable<IExpression> expressions)
        {
            var expression_list = new List<IExpression>();

            foreach (var expression in expressions)
            {
                expression_list.Add(AnalyzeExpression(expression));
            }

            return expression_list;
        }

        private static IExpression AnalyzeExpression(IExpression expression)
        {
            if (expression is IAtom)
            {
                if (expression is SymbolAtom)
                {
                    if (!reservered_symbols.Contains((expression as SymbolAtom).Symbol))
                    {
                        return expression;
                    }
                }
            }
            else if (expression is ListExpression)
            {
                ListExpression list_expression = expression as ListExpression;

                if (list_expression.Items.Length == 0)
                {
                    return list_expression;
                }
                else
                {
                    var first_item = list_expression.Items[0];

                    if (first_item is IAtom)
                    {
                    }
                    else if (first_item is ListExpression)
                    {
                    }
                }
            }

            return null;
        }

        private static IExpression ValueExpression(IExpression expression)
        {
        }

        private static IExpression ConditionExpression(IExpression expression)
        {
        }

        private static bool IsIdentifier(IExpression expression)
        {
            SymbolAtom result = expression as SymbolAtom;

            if (result != null && !reservered_symbols.Contains(result.Symbol))
            {
                return true;
            }

            return false;
        }

        private static bool IsSymbol(IExpression expression, string symbol)
        {
            SymbolAtom result = expression as SymbolAtom;

            if (result != null && result.Symbol == symbol)
            {
                return true;
            }

            return false;
        }

        private static DefineExpression Define(ListExpression list_expression)
        {
            if (list_expression.Items.Length > 0)
            {
                if (IsSymbol(list_expression.Items[0], keyword_define))
                {
                    if (list_expression.Items.Length > 1)
                    {
                        if (IsIdentifier(list_expression.Items[1]))
                        {
                            if (list_expression.Items.Length == 3)
                            {
                                IExpression value_expression = ValueExpression(list_expression.Items[2]);

                                if (value_expression != null)
                                {
                                    return new DefineExpression((list_expression.Items[1] as SymbolAtom).Symbol, value_expression);
                                }
                            }
                        }
                        else
                        {
                            ListExpression function_signature = list_expression.Items[1] as ListExpression;

                            if (function_signature != null)
                            {
                                if (function_signature.Items.Length > 0)
                                {
                                    if (function_signature.Items.All(IsIdentifier))
                                    {
                                        if (list_expression.Items.Length > 2)
                                        {
                                            var item_list = new List<IExpression>();

                                            for (int i = 2; i < list_expression.Items.Length; i++)
                                            {
                                                item_list.Add(AnalyzeExpression(list_expression.Items[i]));
                                            }

                                            return new DefineExpression((function_signature.Items[0] as SymbolAtom).Symbol, new LambdaExpression(list_expression.Items.Skip(1).Select(exp => (exp as SymbolAtom).Symbol), item_list));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static LambdaExpression Lambda(ListExpression list_expression)
        {
            if (list_expression.Items.Length > 0)
            {
                if (IsSymbol(list_expression.Items[0], keyword_lambda))
                {
                    if (list_expression.Items.Length > 1)
                    {
                        var parameter_list = list_expression.Items[1] as ListExpression;

                        if (parameter_list != null)
                        {
                            if (parameter_list.Items.All(IsIdentifier))
                            {
                                if (list_expression.Items.Length > 2)
                                {
                                    var item_list = new List<IExpression>();

                                    for (int i = 2; i < list_expression.Items.Length; i++)
                                    {
                                        item_list.Add(AnalyzeExpression(list_expression.Items[i]));
                                    }

                                    return new LambdaExpression(parameter_list.Items.Select(exp => (exp as SymbolAtom).Symbol), item_list);
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static IfExpression If(ListExpression list_expression)
        {
            if (list_expression.Items.Length > 0)
            {
                if (IsSymbol(list_expression.Items[0], keyword_if))
                {
                    if (list_expression.Items.Length > 1)
                    {
                        IExpression condition = ConditionExpression(list_expression.Items[1]);

                        if (condition != null)
                        {
                            if (list_expression.Items.Length > 2)
                            {
                                IExpression consequent = AnalyzeExpression(list_expression.Items[2]);

                                if (consequent != null)
                                {
                                    if (list_expression.Items.Length > 3)
                                    {
                                        IExpression alternative = AnalyzeExpression(list_expression.Items[3]);
                                        if (alternative != null)
                                        {
                                            return new IfExpression(condition, consequent, alternative);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
