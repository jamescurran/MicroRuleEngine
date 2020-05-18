using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroRuleEngine.Tests
{
	[TestClass]
	public class BugReportTests
	{
		[TestMethod]
		public void ToExpressionTest()
		{
			var records = new HasDescription[]
				{new HasDescription {Description = "Cat"}, new HasDescription {Description = "Dog"}};

			//var rule = Rule.Create("Description","Contains" , "Do");
			var rule = new Rule {MemberName = "Description", Operator = "Contains", Inputs = new List<object> {"Do"}};
			var expression = MRE.ToExpression<HasDescription>(rule);
			var retVal = records.AsQueryable()
				.Where(expression)
				.ToList();
			Assert.AreEqual(1, retVal.Count);
			Assert.AreEqual("Dog", retVal[0].Description);
		}

		class HasDescription
		{
			public string Description { get; set; }
		}
	}
}
