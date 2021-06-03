using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukiToolkit.Tools;

namespace YukiToolkit.Test {
	public class LoggerTest {
		[SetUp]
		public void Setup() {
		}

		[Test]
		public void TestLogger() {
			var logger = new Logger();
			logger.Log("hello {0} {1}", 123, 0.456);
			logger.Log("{\"a\": 1}");
		}
	}
}
