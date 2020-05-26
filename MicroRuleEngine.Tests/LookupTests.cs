using System;
using MicroRuleEngine.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroRuleEngine.Tests
{
	[TestClass]
	public class LookupTests
	{
		[TestMethod]
		public void LookupFound()
		{
			var cities = new string[] {"New York", "London", "Paris", "Munich"};

			MRE.Lookup.Clear();
			MRE.Lookup.Add("Cities", cities);		// must be done before compile.

			Order order = ExampleUsage.GetOrder();

			var rule = Rule.Create("Customer.City", mreOperator.Among, "_.Cities");
			var mre = new MRE();
			var compliedRule = mre.CompileRule<Order>(rule);

			var result = compliedRule(order);
			Assert.IsTrue(result);
		}
		[TestMethod]
		public void LookupNotFound()
		{
			var cities = new string[] { "New York", "London", "Paris", "Munich" };

			MRE.Lookup.Clear();
			MRE.Lookup.Add("Cities", cities);       // must be done before compile.

			Order order = ExampleUsage.GetOrder();
			order.Customer.City = "Moscow";

			var rule = Rule.Create("Customer.City", mreOperator.Among, "_.Cities");
			var mre = new MRE();
			var compliedRule = mre.CompileRule<Order>(rule);

			var result = compliedRule(order);
			Assert.IsFalse(result);
		}

		[TestMethod]
		[ExpectedException(typeof(RulesException))]
		public void LookupNotTable()
		{
			var cities = new string[] { "New York", "London", "Paris", "Munich" };

			MRE.Lookup.Clear();
			MRE.Lookup.Add("Cities", cities);       // must be done before compile.

			Order order = ExampleUsage.GetOrder();

			var rule = Rule.Create("Customer.City", mreOperator.Among, "_.Towns");
			var mre = new MRE();
			var compliedRule = mre.CompileRule<Order>(rule);

			var result = compliedRule(order);
			Assert.IsTrue(result);
		}
	}
}
