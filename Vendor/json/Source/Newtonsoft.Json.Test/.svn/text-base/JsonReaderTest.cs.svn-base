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
	public class JsonReaderTest
	{
		[Test]
		public void ReadingIndented()
		{
			string input = @"{
  CPU: 'Intel',
  Drives: [
    'DVD read/writer',
    ""500 gigabyte hard drive""
  ]
}";

			StringReader sr = new StringReader(input);

			using (JsonReader jsonReader = new JsonReader(sr))
			{
				Assert.AreEqual(jsonReader.TokenType, JsonToken.None);

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.StartObject);

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.PropertyName);
				Assert.AreEqual(jsonReader.Value, "CPU");

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
				Assert.AreEqual(jsonReader.Value, "Intel");

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.PropertyName);
				Assert.AreEqual(jsonReader.Value, "Drives");

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.StartArray);

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
				Assert.AreEqual(jsonReader.Value, "DVD read/writer");
				Assert.AreEqual(jsonReader.QuoteChar, '\'');

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
				Assert.AreEqual(jsonReader.Value, "500 gigabyte hard drive");
				Assert.AreEqual(jsonReader.QuoteChar, '"');

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.EndArray);

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.EndObject);
			}
		}

		[Test]
		public void ReadingEscapedStrings()
		{
			string input = "{value:'Purple\\r \\n monkey\\'s:\\tdishwasher'}";

			StringReader sr = new StringReader(input);

			using (JsonReader jsonReader = new JsonReader(sr))
			{
				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.StartObject);

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.PropertyName);

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.String);
				Assert.AreEqual(jsonReader.Value, "Purple\r \n monkey's:\tdishwasher");
				Assert.AreEqual(jsonReader.QuoteChar, '\'');

				jsonReader.Read();
				Assert.AreEqual(jsonReader.TokenType, JsonToken.EndObject);
			}
		}

		[Test]
		public void WriteReadWrite()
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonWriter(sw))
			{
				jsonWriter.WriteStartArray();
				jsonWriter.WriteValue(true);

				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("integer");
				jsonWriter.WriteValue(99);
				jsonWriter.WritePropertyName("string");
				jsonWriter.WriteValue("how now brown cow?");
				jsonWriter.WritePropertyName("array");

				jsonWriter.WriteStartArray();
				for (int i = 0; i < 5; i++)
				{
					jsonWriter.WriteValue(i);
				}

				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("decimal");
				jsonWriter.WriteValue(990.00990099m);
				jsonWriter.WriteEndObject();

				jsonWriter.WriteValue(5);
				jsonWriter.WriteEndArray();

				jsonWriter.WriteEndObject();

				jsonWriter.WriteValue("This is a string.");
				jsonWriter.WriteNull();
				jsonWriter.WriteNull();
				jsonWriter.WriteEndArray();
			}

			string json = sb.ToString();

			JsonSerializer serializer = new JsonSerializer();

			object jsonObject = serializer.Deserialize(new JsonReader(new StringReader(json)));

			sb = new StringBuilder();
			sw = new StringWriter(sb);

			using (JsonWriter jsonWriter = new JsonWriter(sw))
			{
				serializer.Serialize(sw, jsonObject);
			}

			Assert.AreEqual(json, sb.ToString());
		}
	}
}