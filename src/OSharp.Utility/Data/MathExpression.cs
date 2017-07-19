using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.CSharp;


namespace OSharp.Utility.Data
{
    /// <summary>
    /// 数学计算表达式
    /// </summary>
    public class MathExpression
    {
        private readonly object _instance;
        private readonly MethodInfo _method;

        /// <summary>
        /// 初始化一个<see cref="MathExpression"/>类型的新实例
        /// </summary>
        public MathExpression(string expression)
        {
            if (expression.IndexOf("return", StringComparison.Ordinal) < 0)
            {
                expression = "return " + expression;
            }
            if (!expression.EndsWith(";"))
            {
                expression += ";";
            }
            string className = "Expression";
            string methodName = "Compute";
            CompilerParameters parameters = new CompilerParameters()
            {
                GenerateInMemory = true
            };
            CompilerResults results = new CSharpCodeProvider().CompileAssemblyFromSource(parameters,
                string.Format("using System;sealed class {0}{{public double {1}(){{{2}}}}}", className, methodName, expression));
            if (results.Errors.Count > 0)
            {
                string msg = "Expression(\"" + expression + "\"): \n";
                msg = results.Errors.Cast<CompilerError>().Aggregate(msg, (current, err) => current + (err.ToString() + "\n"));
                throw new Exception(msg);
            }
            _instance = results.CompiledAssembly.CreateInstance(className);
            if (_instance != null)
            {
                _method = _instance.GetType().GetMethod(methodName);
            }
        }

        /// <summary>
        /// 计算表达式结果
        /// </summary>
        /// <returns></returns>
        public double Compute()
        {
            _method.CheckNotNull("_method");

            return (double)_method.Invoke(_instance, new object[0]);
        }
    }
}
