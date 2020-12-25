using System;
using System.Collections.Generic;
using NUnit.Framework;
using YukiToolkit.DataStructures;
using YukiToolkit.ExtensionMethods;

namespace YukiToolkit.Test {
	public class JsonTest {
		private string _tempJson = "";

		[SetUp] public void Setup() {
		}

		[Test] public void TestToJsonString() {
			Book book1 = new() {
				Name = "LearningPython",
				Price = 0.123456789123456789123456789,
				Positions = new int[2, 3] {{1, 2, 3}, {4, 5, 6}}
			};
			Book book2 = new() {
				Name = "中文测试",
				Price = 123456789123456789123456789.0,
				Positions = null
			};
			string result = (book1, book2).ToJsonString();
			Console.WriteLine(result);
			_tempJson = result;
			result = (book1, book2).ToJsonString(new JsonSerializeSettings(true));
			Console.WriteLine(result);
			result = (book1, book2).ToJsonString(new JsonSerializeSettings(false, true));
			Console.WriteLine(result);
		}

		[Test] public void TestToObjectFromJson() {
			var (book1, book2) = _tempJson.ToObjectFromJson<(Book, Book)>();
			Console.WriteLine(book1.Name);
			Console.WriteLine(book2.Name);
			Assert.AreEqual(book1.Price, 0.123456789123456789123456789);
			Assert.AreEqual(book2.Price, 123456789123456789123456789.0);
		}

		[Test] public void TestGetOrDefaultDictionary() {
			var dict = new GetOrDefaultDictionary<SortedDictionary<int, int>, int, int>(new SortedDictionary<int, int>(), 0);
			Console.WriteLine(dict[1]);
			dict[2]++;
			Console.WriteLine(dict[2]);
		}

		public class Book {
			public string Name {
				get;
				set;
			} = "";

			public double Price {
				get;
				set;
			}

			public int[,]? Positions {
				get;
				set;
			}
		}
	}
}
