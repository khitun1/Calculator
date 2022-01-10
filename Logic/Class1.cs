using System;
using System.Data;

namespace Logic
{
    public class Calc
    {
        private static int ReadOperand(string expression, int n)
        {
            bool flag = false;
            if (expression[n] == '-')
            {
                n++;
            }
            while (expression[n] is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' or '.' or '(' or ')')
            {
                if (expression[n] is '.')
                {
                    if (flag is false)
                    {
                        flag = true;
                    }
                    else
                    {
                        return -1;
                    }
                }
                n++;
            }
            return n;
        }

        public static string Calculate(string expression)
        {
            string oper1, oper2, expr = expression;
            char op = '\0';
            int i, n;
            double num1, num2, result = 0;
            expression += '\0';
            while (true)
            {
                i = 0;
                n = 0;
                i = ReadOperand(expression, i);
                if (i == -1)
                {
                    throw new Exception("Áîëüøå îäíîé çàïÿòîé!");
                }
                if (expression[i] is '\0')
                {
                    break;
                }
                oper1 = expression[0..i];
                if (string.IsNullOrEmpty(oper1))
                {
                    throw new Exception("Ââåäèòå êîððåêòíûé ïåðâûé îïåðàíä!");
                }
                op = expression[i++];
                if (op is not '+' and not '-' and not '*' and not '/')
                {
                    throw new Exception("Ââåäèòå êîððåêòíóþ îïåðàöèþ!");
                }
                n = i;
                i = ReadOperand(expression, i);
                if (i == -1)
                {
                    throw new Exception("Áîëüøå îäíîé çàïÿòîé!");
                }
                oper2 = expression[(oper1.Length + 1)..i];
                if (string.IsNullOrEmpty(oper2))
                {
                    throw new Exception("Ââåäèòå êîððåêòíûé âòîðîé îïåðàíä!");
                }
                if (expression[i] is '/' or '*')
                {
                    op = expression[i++];
                    n = i;
                    oper1 = oper2;
                    i = ReadOperand(expression, i);
                    if (i == -1)
                    {
                        throw new Exception("Áîëüøå îäíîé çàïÿòîé!");
                    }
                    oper2 = expression[n..i];
                    if (string.IsNullOrEmpty(oper2))
                    {
                        throw new Exception("Ââåäèòå êîððåêòíûé âòîðîé îïåðàíä!");
                    }
                }
                double.TryParse(oper1, out num1);
                double.TryParse(oper2, out num2);
                expression = expression.Replace(expression[(n - oper1.Length - 1)..i], result.ToString());
            }
            expression = new DataTable().Compute(expr, null).ToString();
            return expression;
        }

    }
}