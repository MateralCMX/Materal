using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Example
{
    public class Program
    {
        public static void Main()
        {
            int age = 3;
            Expression<Func<User, bool>> expression = m => m.Name == "Materal" && m.Age > age && m.Age < 1 && "123" == m.Name;
            //Expression<Func<User, bool>> expression = m => m.Age > age;
            Test(expression);
        }

        public static void PrintValue(ExpressionType expressionType, object value1, object value2, bool? isLeft = null)
        {
            if (isLeft != null && !isLeft.Value)
            {
                object temp = value1;
                value1 = value2;
                value2 = temp;
            }
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    Console.WriteLine($"{value1}等于{value2}");
                    break;
                case ExpressionType.GreaterThan:
                    Console.WriteLine($"{value1}大于{value2}");
                    break;
                case ExpressionType.LessThan:
                    Console.WriteLine($"{value1}小于{value2}");
                    break;
            }
        }
        public static void Test(Expression expression, ExpressionType? nodeTypeValue = null, Expression otherExpression = null, bool? isLeft = null)
        {
            Type expressionType = expression.GetType();
            PropertyInfo bodyPropertyInfo = expressionType.GetProperty("Body");
            object bodyValue = bodyPropertyInfo == null ? expression : bodyPropertyInfo.GetValue(expression);
            Type bodyType = bodyValue.GetType();
            if (bodyValue is ConstantExpression)
            {
                if (nodeTypeValue == null) throw new ArgumentNullException();
                PropertyInfo bodyExpressionType = bodyType.GetProperty("Value");
                if (bodyExpressionType == null) throw new ArgumentNullException();
                bodyValue = bodyExpressionType.GetValue(bodyValue);
                FieldInfo[] fields = bodyValue.GetType().GetFields();
                if(fields.Length != 1) throw new ArgumentException();
                bodyValue = fields[0].GetValue(bodyValue);
                PrintValue(nodeTypeValue.Value, bodyValue, otherExpression, isLeft);
                return;
            }
            PropertyInfo leftPropertyInfo = bodyType.GetProperty("Left");
            PropertyInfo rightPropertyInfo = bodyType.GetProperty("Right");
            PropertyInfo nodeTypePropertyInfo = bodyType.GetProperty("NodeType");
            if (leftPropertyInfo == null || rightPropertyInfo == null || nodeTypePropertyInfo == null) throw new ArgumentNullException();
            object leftValue = leftPropertyInfo.GetValue(bodyValue);
            object rightValue = rightPropertyInfo.GetValue(bodyValue);
            nodeTypeValue = nodeTypeValue ?? (ExpressionType)nodeTypePropertyInfo.GetValue(bodyValue);
            if (rightValue is ConstantExpression || leftValue is ConstantExpression)
            {
                PrintValue(nodeTypeValue.Value, leftValue, rightValue, isLeft);
            }
            else if (rightValue is MemberExpression rightMember)
            {
                if (leftValue is Expression leftExpression)
                {
                    Test(rightMember.Expression, nodeTypeValue, leftExpression, false);
                }
            }
            else if (leftValue is MemberExpression leftMember)
            {
                if (rightValue is Expression rightExpression)
                {
                    Test(leftMember.Expression, nodeTypeValue, rightExpression, true);
                }
            }
            else
            {
                if (leftValue is Expression leftExpression)
                {
                    Test(leftExpression);
                }
                if (rightValue is Expression rightExpression)
                {
                    Test(rightExpression);
                }
            }
        }
    }

    public class User
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
