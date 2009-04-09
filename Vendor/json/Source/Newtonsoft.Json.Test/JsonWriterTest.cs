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
	[TestFixture]
	public class JsonWriterTest
	{
		[Test]
		public void ValueFormatting()
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonWriter(sw))
			{
				jsonWriter.WriteStartArray();
				jsonWriter.WriteValue('@');
				jsonWriter.WriteValue("\r\n\t\f\b?{\\r\\n\"\'");
				jsonWriter.WriteValue(true);
				jsonWriter.WriteValue(10);
				jsonWriter.WriteValue(10.99);
				jsonWriter.WriteValue(0.99);
				jsonWriter.WriteValue(0.000000000000000001d);
				jsonWriter.WriteValue(0.000000000000000001m);
				jsonWriter.WriteValue(null);
				jsonWriter.WriteValue("This is a string.");
				jsonWriter.WriteNull();
				jsonWriter.WriteUndefined();
				jsonWriter.WriteEndArray();
			}

			string expected = @"[""@"",""\r\n\t\f\b?{\\r\\n\""'"",true,10,10.99,0.99,1E-18,0.000000000000000001,"""",""This is a string."",null,undefined]";
			string result = sb.ToString();

			Console.WriteLine("ValueFormatting");
			Console.WriteLine(result);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void StringEscaping()
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonWriter(sw))
			{
				jsonWriter.WriteStartArray();
				jsonWriter.WriteValue(@"""These pretzels are making me thirsty!""");
				jsonWriter.WriteValue("Jeff's house was burninated.");
				jsonWriter.WriteValue(@"1. You don't talk about fight club.
2. You don't talk about fight club.");
				jsonWriter.WriteValue("35% of\t statistics\n are made\r up.");
				jsonWriter.WriteEndArray();
			}

			string expected = @"[""\""These pretzels are making me thirsty!\"""",""Jeff's house was burninated."",""1. You don't talk about fight club.\r\n2. You don't talk about fight club."",""35% of\t statistics\n are made\r up.""]";
			string result = sb.ToString();

			Console.WriteLine("StringEscaping");
			Console.WriteLine(result);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Indenting()
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonWriter(sw))
			{
				jsonWriter.Formatting = Formatting.Indented;

				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("CPU");
				jsonWriter.WriteValue("Intel");
				jsonWriter.WritePropertyName("PSU");
				jsonWriter.WriteValue("500W");
				jsonWriter.WritePropertyName("Drives");
				jsonWriter.WriteStartArray();
				jsonWriter.WriteValue("DVD read/writer");
				jsonWriter.WriteComment("(broken)");
				jsonWriter.WriteValue("500 gigabyte hard drive");
				jsonWriter.WriteValue("200 gigabype hard drive");
				jsonWriter.WriteEnd();
				jsonWriter.WriteEndObject();
			}

			string expected = @"{
  ""CPU"": ""Intel"",
  ""PSU"": ""500W"",
  ""Drives"": [
    ""DVD read/writer""
    /*(broken)*/,
    ""500 gigabyte hard drive"",
    ""200 gigabype hard drive""
  ]
}";
			string result = sb.ToString();

			Console.WriteLine("Indenting");
			Console.WriteLine(result);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void State()
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonWriter(sw))
			{
				Assert.AreEqual(WriteState.Start, jsonWriter.WriteState);

				jsonWriter.WriteStartObject();
				Assert.AreEqual(WriteState.Object, jsonWriter.WriteState);
				
				jsonWriter.WritePropertyName("CPU");
				Assert.AreEqual(WriteState.Property, jsonWriter.WriteState);

				jsonWriter.WriteValue("Intel");
				Assert.AreEqual(WriteState.Object, jsonWriter.WriteState);

				jsonWriter.WritePropertyName("Drives");
				Assert.AreEqual(WriteState.Property, jsonWriter.WriteState);

				jsonWriter.WriteStartArray();
				Assert.AreEqual(WriteState.Array, jsonWriter.WriteState);

				jsonWriter.WriteValue("DVD read/writer");
				Assert.AreEqual(WriteState.Array, jsonWriter.WriteState);

				jsonWriter.WriteEnd();
				Assert.AreEqual(WriteState.Object, jsonWriter.WriteState);

				jsonWriter.WriteEndObject();
				Assert.AreEqual(WriteState.Start, jsonWriter.WriteState);
			}
		}
	}
}