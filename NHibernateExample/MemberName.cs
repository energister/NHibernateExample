using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NHibernateExample
{
    /// <summary>
    /// Provides strongly typed way to access to PropertyInfo or name of any property of given object or class
    /// </summary>
    public static class MemberName
    {
        public static string Of<T, TPropertyType>(Expression<Func<T, TPropertyType>> propertyLambda)
        {
            return FindProperty(propertyLambda.Body).Name;
        }

        public static string Of<TPropertyType>(Expression<Func<TPropertyType>> propertyLambda)
        {
            return FindProperty(propertyLambda.Body).Name;
        }

        private static PropertyInfo FindProperty(Expression propertyLambdaBody)
        {
            if (propertyLambdaBody.NodeType == ExpressionType.MemberAccess)  // access to member
            {
                var propertyInfo = (propertyLambdaBody as MemberExpression).Member as PropertyInfo;  // access to property
                if (propertyInfo != null)
                {
                    return propertyInfo;
                }
            }

            throw new ArgumentException("Not a property expression [" + propertyLambdaBody + "]", "propertyLambdaBody");
        }
    }
}