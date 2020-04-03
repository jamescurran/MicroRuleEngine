using System;
using MicroRuleEngine.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroRuleEngine.Tests
{
	[TestClass]
	public class FluentInterfaceTests
	{
		[TestMethod]
		public void Fluent_GreaterThan()
		{
			Rule rule = Rule<Order>.Create(x => x.Total).GreaterThan(12.00m);

			MRE engine = new MRE();
			var compiledRule = engine.CompileRule<Order>(rule);


			Order order = ExampleUsage.GetOrder();
			bool passes = compiledRule(order);
			Assert.IsTrue(passes);

			order.Total = 9.99m;
			passes = compiledRule(order);
			Assert.IsFalse(passes);
		}

		[TestMethod]
		public void Fluent_IsInt_OK()
		{
			var target = new IsTypeClass { NumAsString = "1234", OtherField = "Hello, World" };

			Rule rule = Rule<IsTypeClass>.IsInteger(x => x.NumAsString);
			MRE engine = new MRE();
			var compiledRule = engine.CompileRule<IsTypeClass>(rule);
			bool passes = compiledRule(target);
			Assert.IsTrue(passes);
		}

	}
}
