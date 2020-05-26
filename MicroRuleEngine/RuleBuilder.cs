using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MicroRuleEngine
{
	public class RuleBuilder<TSource>
	{
		private Rule _current;
		public List<Rule> List  {get; } =new List<Rule>();

		public RuleBuilder<TSource> IsInteger<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.IsInteger, null));
		public RuleBuilder<TSource> IsSingle<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.IsSingle, null));
		public RuleBuilder<TSource> IsDouble<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.IsDouble, null));
		public RuleBuilder<TSource> IsDecimal<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.IsDecimal, null));
		public RuleBuilder<TSource> IsNull<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.Equal, null));
		public RuleBuilder<TSource> IsNotNull<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.NotEqual, null));
		public RuleBuilder<TSource> IsNullOrEmpty<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.IsNullOrEmpty, null));
		public RuleBuilder<TSource> HasValue<TTarget>(Expression<Func<TSource, TTarget>> selector) => Add(Rule.Create(GetName(selector), mreOperator.HasValue, null));


		public RuleBuilder<TSource> Equals<TTarget>(Expression<Func<TSource, TTarget>> selector, TTarget target) => Add(Rule.Create(GetName(selector), mreOperator.Equal, target));
		public RuleBuilder<TSource> GreaterThan<TTarget>(Expression<Func<TSource, TTarget>> selector,TTarget target) => Add(Rule.Create(GetName(selector), mreOperator.GreaterThan, target));
		public RuleBuilder<TSource> GreaterThanOrEqual<TTarget>(Expression<Func<TSource, TTarget>> selector, TTarget target) => Add(Rule.Create(GetName(selector), mreOperator.GreaterThanOrEqual, target));
		public RuleBuilder<TSource> LessThan<TTarget>(Expression<Func<TSource, TTarget>> selector,  TTarget target) => Add(Rule.Create(GetName(selector), mreOperator.LessThan, target));
		public RuleBuilder<TSource> NotEqual<TTarget>(Expression<Func<TSource, TTarget>> selector, TTarget target) => Add(Rule.Create(GetName(selector), mreOperator.NotEqual, target));
		public RuleBuilder<TSource> IsMatch<TTarget>(Expression<Func<TSource, TTarget>> selector,TTarget target) => Add(Rule.Create(GetName(selector), mreOperator.IsMatch, target));

		private static string GetName<TTarget>(Expression<Func<TSource, TTarget>> selector) =>
			((MemberExpression)selector.Body).Member.Name;

		private RuleBuilder<TSource> Add(Rule rule)
		{
			_current = rule;
			List.Add(rule);
			return this;
		}

		public RuleBuilder<TSource> Error(string message)
		{
			_current.IsWarning = false;
			_current.Message = message;
			return this;
		}
		public RuleBuilder<TSource> Warning(string message)
		{
			_current.IsWarning = true;
			_current.Message = message;
			return this;
		}

		public List<Func<TSource, bool>> CompileAll()
		{
			var mre = new MRE();
			return List.Select(r => mre.CompileRule<TSource>(r)).ToList();
		}
	}

}
