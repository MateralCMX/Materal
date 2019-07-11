using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MateralMapper.Core
{
    /// <summary>
    /// 生成表达式目录树  泛型缓存
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public static class ExpressionGenericMapper<TSource, TDestination>
    {
        private static readonly Func<TSource, TDestination> ConvertFunc;
        private static readonly Func<TSource, TDestination, TDestination> ConvertFunc2;
        static ExpressionGenericMapper()
        {
            ConvertFunc = CompileFunc();
            ConvertFunc2 = CompileFunc2();
        }
        private static Func<TSource, TDestination> CompileFunc()
        {
            Type tinType = typeof(TSource);
            Type toutType = typeof(TDestination);
            ParameterExpression parameterExpression = Expression.Parameter(tinType, "m");
            List<MemberBinding> memberBindingList = (from toutPropertyInfo in toutType.GetProperties()
                                                     let tinPropertyInfo = tinType.GetProperty(toutPropertyInfo.Name)
                                                     where tinPropertyInfo != null
                                                     let property = Expression.Property(parameterExpression, tinPropertyInfo)
                                                     select Expression.Bind(toutPropertyInfo, property)
                ).Cast<MemberBinding>().ToList();
            memberBindingList.AddRange(from toutFieldInfo in toutType.GetFields()
                                       let tinFieldInfo = tinType.GetField(toutFieldInfo.Name)
                                       where tinFieldInfo != null
                                       let property = Expression.Field(parameterExpression, tinFieldInfo)
                                       select Expression.Bind(toutFieldInfo, property));
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(toutType), memberBindingList);
            Expression<Func<TSource, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, parameterExpression);
            Func<TSource, TDestination> result = lambda.Compile();
            return result;
        }
        private static Func<TSource, TDestination, TDestination> CompileFunc2()
        {
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);
            ParameterExpression mParameterExpression = Expression.Parameter(sourceType, "m");
            ParameterExpression nParameterExpression = Expression.Parameter(destinationType, "n");
            ParameterExpression resultParameterExpression = Expression.Parameter(destinationType, "result");
            List<Expression> expressions = (from destinationPropertyInfo in destinationType.GetProperties()
                                            let sourceTypePropertyInfo = sourceType.GetProperty(destinationPropertyInfo.Name)
                                            where sourceTypePropertyInfo != null && sourceTypePropertyInfo.PropertyType == destinationPropertyInfo.PropertyType
                                            let mMemberExpression = Expression.Property(mParameterExpression, sourceTypePropertyInfo)
                                            let nMemberExpression = Expression.Property(nParameterExpression, destinationPropertyInfo)
                                            select Expression.Assign(nMemberExpression, mMemberExpression)
                                            ).Cast<Expression>().ToList();
            expressions.AddRange(from destinationFieldInfo in destinationType.GetFields()
                                 let sourceTypeFieldInfo = sourceType.GetField(destinationFieldInfo.Name)
                                 where sourceTypeFieldInfo != null && sourceTypeFieldInfo.FieldType == destinationFieldInfo.FieldType
                                 let mMemberExpression = Expression.Field(mParameterExpression, sourceTypeFieldInfo)
                                 let nMemberExpression = Expression.Field(nParameterExpression, destinationFieldInfo)
                                 select Expression.Assign(nMemberExpression, mMemberExpression));
            BinaryExpression resultExpression = Expression.Assign(resultParameterExpression, nParameterExpression);
            expressions.Add(resultExpression);
            BlockExpression blockExpression = Expression.Block(new[] { resultParameterExpression },expressions);
            Expression<Func<TSource, TDestination, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination, TDestination>>(blockExpression, mParameterExpression, nParameterExpression);
            return lambda.Compile();
        }
        public static TDestination ConvertTo(TSource source)
        {
            return ConvertFunc(source);
        }
        public static TDestination ConvertTo(TSource source, TDestination destination)
        {
            return ConvertFunc2(source, destination);
        }
    }
}
