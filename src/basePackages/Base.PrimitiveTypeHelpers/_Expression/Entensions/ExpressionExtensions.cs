using System.Linq.Expressions;

namespace Base.PrimitiveTypeHelpers._Expression.Entensions
{
	public static class ExpressionExtensions
	{

		public static Expression<Func<TInput, bool>> CombineWithOr<TInput>(this Expression<Func<TInput, bool>> func1, Expression<Func<TInput, bool>> func2)
		{
			return Expression.Lambda<Func<TInput, bool>>(
				Expression.Or(
					func1.Body,
					new ExpressionParameterReplacer(func2.Parameters, func1.Parameters).Visit(func2.Body)
					),
				func1.Parameters
				);
		}

		public static Expression<Func<TInput, bool>> CombineWithAndAlso<TInput>(this Expression<Func<TInput, bool>> func1, Expression<Func<TInput, bool>> func2)
		{
			return Expression.Lambda<Func<TInput, bool>>(
				Expression.AndAlso(
					func1.Body,
					new ExpressionParameterReplacer(func2.Parameters, func1.Parameters).Visit(func2.Body)
					),
				func1.Parameters
				);
		}

		public static List<BinaryExpression> ExtractParts(this Expression expression)
		{
			List<BinaryExpression> comparisons = new List<BinaryExpression>();

			Visit(expression, comparisons);

			return comparisons;
		}

		#region Behind the Scenes

		private static void Visit(Expression node, List<BinaryExpression> comparisons)
		{
			if (node is BinaryExpression binaryExpression)
			{
				VisitBinary(binaryExpression, comparisons);
			}
			else if (node is LambdaExpression lambdaExpression)
			{
				Visit(lambdaExpression.Body, comparisons);
			}
			else if (node is UnaryExpression unaryExpression)
			{
				Visit(unaryExpression.Operand, comparisons);
			}
			// Add more cases if needed
		}

		private static void VisitBinary(BinaryExpression node, List<BinaryExpression> comparisons)
		{
			if (
				node.NodeType == ExpressionType.Equal ||
				node.NodeType == ExpressionType.NotEqual ||
				node.NodeType == ExpressionType.GreaterThan ||
				node.NodeType == ExpressionType.GreaterThanOrEqual ||
				node.NodeType == ExpressionType.LessThan ||
				node.NodeType == ExpressionType.LessThanOrEqual
				)
			{
				comparisons.Add(node);
			}

			Visit(node.Left, comparisons);
			Visit(node.Right, comparisons);
		}

		private class ExpressionParameterReplacer : ExpressionVisitor
		{
			public ExpressionParameterReplacer(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
			{
				ParameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();

				for (int i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
					ParameterReplacements.Add(fromParameters[i], toParameters[i]);
			}


			private IDictionary<ParameterExpression, ParameterExpression> ParameterReplacements { get; set; }

			protected override Expression VisitParameter(ParameterExpression node)
			{
				ParameterExpression replacement;

				if (ParameterReplacements.TryGetValue(node, out replacement))
					node = replacement;

				return base.VisitParameter(node);
			}
		}

		#endregion

	}
}
