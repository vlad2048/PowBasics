using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace PowBasics.QueryExpr_;

public static class QueryExprUtils
{
	public static (Func<T, V>, Action<T, V>, string) RetrieveGetSetName<T, V>(Expression<Func<T, V>> expr) => (
		GetGetter(expr),
		GetSetter(expr),
		GetName(expr)
	);


	public static (Func<T, V>, Action<T, V>) RetrieveGetSet<T, V>(Expression<Func<T, V>> expr) => (
		GetGetter(expr),
		GetSetter(expr)
	);


	private static Func<T, V> GetGetter<T, V>(Expression<Func<T, V>> expr) =>
		expr.Compile();

	private static Action<T, V> GetSetter<T, V>(Expression<Func<T, V>> getter)
	{
		var propInfo = GetPropertyInfo(getter);
		var member = (MemberExpression)getter.Body;
		var param = Expression.Parameter(propInfo.PropertyType, propInfo.Name);
		var setter = Expression.Lambda<Action<T, V>>(Expression.Assign(member, param), getter.Parameters[0], param);
		var result = setter.Compile();
		return result;
	}

	private static string GetName<T, V>(Expression<Func<T, V>> getter) => GetPropertyInfo(getter).Name;

	private static PropertyInfo GetPropertyInfo<T, V>(Expression<Func<T, V>> getter)
	{
		var propExp = getter.Body as MemberExpression;
		Debug.Assert(propExp != null);
		var propInfo = propExp.Member as PropertyInfo;
		Debug.Assert(propInfo != null);
		return propInfo;
	}
}