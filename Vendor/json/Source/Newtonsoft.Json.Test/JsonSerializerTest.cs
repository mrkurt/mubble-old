#region License
// Copyright 2006 James Newton-King
// http://www.newtonsoft.com
//
// This work is licensed under the Creative Commons Attribution 2.5 License
// http://creativecommons.org/licenses/by/2.5/
//
// You are free:
//    * to copy, distribute, display, and perform the work
//    * to make derivative works
//    * to make commercial use of the work
//
// Under the following conditions:
//    * You must attribute the work in the manner specified by the author or licensor:
//          - If you find this component useful a link to http://www.newtonsoft.com would be appreciated.
//    * For any reuse or distribution, you must make clear to others the license terms of this work.
//    * Any of these conditions can be waived if you get permission from the copyright holder.
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using System.IO;

namespace Newtonsoft.Json.Test
{
    public class Product
    {
        public string Name;
        public DateTime Expiry;
        public decimal Price;
    }

	public class Store
	{
		public StoreColor Color = StoreColor.Yellow;
		public DateTime Establised = new DateTime(2010, 1, 22);
		public double Width = 1.1;
		public int Employees = 999;
		public int[] RoomsPerFloor = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		public bool Open = false;
		public char Symbol = '@';
		public List<string> Mottos = new List<string>();
		public decimal Cost = 100980.1M;
		public string Escape = "\r\n\t\f\b?{\\r\\n\"\'";
		public List<Product> product = new List<Product>();

		public Store()
		{
			Mottos.Add("Hello World");
			Mottos.Add("öäüÖÄÜ\\'{new Date(12345);}[222]_µ@²³~");
			Mottos.Add(null);
			Mottos.Add(" ");

			Product rocket = new Product();
			rocket.Name = "Rocket";
			rocket.Expiry = new DateTime(2000, 2, 2, 23, 1, 30);
			Product alien = new Product();
			alien.Name = "Alien";

			product.Add(rocket);
			product.Add(alien);
		}
	}

	public enum StoreColor
	{
		Black,
		Red,
		Yellow,
		White
	}

	[TestFixture]
	public class JsonSerializerTest
	{
		[Test]
		public void PersonTypedObjectDeserialization()
		{
			Store store = new Store();

			string jsonText = JavaScriptConvert.SerializeObject(store);

			Store deserializedStore = (Store)JavaScriptConvert.DeserializeObject(jsonText, typeof(Store));

			Console.WriteLine(jsonText);
		}

		[Test]
		public void TypedObjectDeserialization()
		{
			Product p = new Product();

			p.Name = "Apple";
			p.Expiry = new DateTime(2010, 1, 24, 12, 0, 0);
			p.Price = 3.99M;

            JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            using (JsonWriter jsonWriter = new JsonWriter(sw))
            {
                serializer.Serialize(sw, p);
            }
            string output = sw.ToString();

            using (StringReader sr = new StringReader(output))
            {
                using (JsonReader jr = new JsonReader(sr))
                {
					p = serializer.Deserialize(jr, typeof(Product)) as Product;
				}
            }

			Assert.AreEqual("Apple", p.Name);
			Assert.AreEqual(new DateTime(2010, 1, 24, 12, 0, 0), p.Expiry);
			Assert.AreEqual(3.99, p.Price);
		}

		[Test]
		public void JavaScriptConvertSerializer()
		{
			string value = @"{""Name"":""Orange"", ""Price"":3.99, ""Expiry"":""01/24/2010 12:00:00""}";

			Product p = JavaScriptConvert.DeserializeObject(value, typeof(Product)) as Product;

			Assert.AreEqual("Orange", p.Name);
			Assert.AreEqual(new DateTime(2010, 1, 24, 12, 0, 0), p.Expiry);
			Assert.AreEqual(3.99, p.Price);
		}

		[Test]
		public void DeserializeJavaScriptDate()
		{
			DateTime dateValue = new DateTime(2000, 3, 30);
			Dictionary<string, object> testDictionary = new Dictionary<string, object>();
			testDictionary["date"] = dateValue;

			string jsonText = JavaScriptConvert.SerializeObject(testDictionary);

			Dictionary<string, object> deserializedDictionary = (Dictionary<string, object>)JavaScriptConvert.DeserializeObject(jsonText, typeof(Dictionary<string, object>));
			DateTime deserializedDate = (DateTime)deserializedDictionary["date"];

			Assert.AreEqual(dateValue, deserializedDate);

			Console.WriteLine("JavaScriptDateConverterTest");
			Console.WriteLine(jsonText);
			Console.WriteLine();
			Console.WriteLine(jsonText);
		}
	}
}