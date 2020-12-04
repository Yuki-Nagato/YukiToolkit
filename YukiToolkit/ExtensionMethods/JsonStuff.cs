using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using YukiToolkit.UsefulConsts;

namespace YukiToolkit.ExtensionMethods {
	public enum DeserializeMissingKeyStrategy {
		Ignore,
		ThrowException
	}

	[SuppressMessage("ReSharper", "InconsistentNaming")] public enum NamingStrategy {
		camelCase,
		PascalCase,
		snake_case,
		kebab_case // kebab-case
	}

	public enum SerializeNullStrategy {
		Keep,
		Ignore
	}

	public enum SerializeWrapStrategy {
		NoWrap,
		Wrap
	}

	public class JsonSerializeSettings {
		public JsonSerializeSettings(bool wrap = false, bool keepNull = false) {
			if (wrap) {
				WrapStrategy = SerializeWrapStrategy.Wrap;
			}

			if (keepNull) {
				NullStrategy = SerializeNullStrategy.Keep;
			}
		}

		public NamingStrategy NamingStrategy {
			get;
			set;
		} = NamingStrategy.camelCase;

		public SerializeWrapStrategy WrapStrategy {
			get;
			set;
		} = SerializeWrapStrategy.NoWrap;

		public SerializeNullStrategy NullStrategy {
			get;
			set;
		} = SerializeNullStrategy.Ignore;
	}

	public class JsonDeserializeSettings {
		public NamingStrategy NamingStrategy {
			get;
			set;
		} = NamingStrategy.camelCase;

		public DeserializeMissingKeyStrategy MissingKeyStrategy {
			get;
			set;
		} = DeserializeMissingKeyStrategy.Ignore;
	}

	public static class JsonStuff {
		public static string ToJsonString<T>(this T obj, JsonSerializeSettings? serializeSettings = null) {
			serializeSettings ??= new JsonSerializeSettings();
			JsonSerializerSettings settings = new JsonSerializerSettings();
			switch (serializeSettings.NamingStrategy) {
				case NamingStrategy.camelCase:
					settings.ContractResolver = new DefaultContractResolver {
						NamingStrategy = new CamelCaseNamingStrategy(false, false)
					};
					break;
				case NamingStrategy.PascalCase:
					break;
				case NamingStrategy.snake_case:
					settings.ContractResolver = new DefaultContractResolver {
						NamingStrategy = new SnakeCaseNamingStrategy(false, false)
					};
					break;
				case NamingStrategy.kebab_case:
					settings.ContractResolver = new DefaultContractResolver {
						NamingStrategy = new KebabCaseNamingStrategy(false, false)
					};
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			settings.NullValueHandling = serializeSettings.NullStrategy switch {
				SerializeNullStrategy.Keep => NullValueHandling.Include,
				SerializeNullStrategy.Ignore => NullValueHandling.Ignore,
				_ => throw new ArgumentOutOfRangeException()
			};
			settings.Formatting = serializeSettings.WrapStrategy switch {
				SerializeWrapStrategy.NoWrap => Formatting.None,
				SerializeWrapStrategy.Wrap => Formatting.Indented,
				_ => throw new ArgumentOutOfRangeException()
			};
			return JsonConvert.SerializeObject(obj, settings);
		}

		public static T ToObjectFromJson<T>(this string json, JsonDeserializeSettings? deserializeSettings = null) {
			deserializeSettings ??= new JsonDeserializeSettings();
			JsonSerializerSettings settings = new JsonSerializerSettings();
			switch (deserializeSettings.NamingStrategy) {
				case NamingStrategy.camelCase:
					settings.ContractResolver = new DefaultContractResolver {
						NamingStrategy = new CamelCaseNamingStrategy(false, false)
					};
					break;
				case NamingStrategy.PascalCase:
					break;
				case NamingStrategy.snake_case:
					settings.ContractResolver = new DefaultContractResolver {
						NamingStrategy = new SnakeCaseNamingStrategy(false, false)
					};
					break;
				case NamingStrategy.kebab_case:
					settings.ContractResolver = new DefaultContractResolver {
						NamingStrategy = new KebabCaseNamingStrategy(false, false)
					};
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			settings.MissingMemberHandling = deserializeSettings.MissingKeyStrategy switch {
				DeserializeMissingKeyStrategy.Ignore => MissingMemberHandling.Ignore,
				DeserializeMissingKeyStrategy.ThrowException => MissingMemberHandling.Error,
				_ => throw new ArgumentOutOfRangeException()
			};
#pragma warning disable 8603
			return JsonConvert.DeserializeObject<T>(json, settings);
#pragma warning restore 8603
		}

		public static void DumpToFile<T>(this T obj, string filePath) {
			File.WriteAllText(obj.ToJsonString(), filePath, ConstStuff.UniversalUtf8Encoding);
		}

		public static T LoadFromFile<T>(string filePath) {
			return File.ReadAllText(filePath, ConstStuff.UniversalUtf8Encoding).ToObjectFromJson<T>();
		}
	}
}
