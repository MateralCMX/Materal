﻿using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Materal.Logger
{
    public partial class Log
	{
		/// <summary>
		/// 获得进程ID
		/// </summary>
		/// <returns></returns>
		public static string GetProgressID()
		{
			Process processes = Process.GetCurrentProcess();
			return processes.Id.ToString();
		}
		/// <summary>
		/// 计算机名称
		/// </summary>
		public static string MachineName => Environment.MachineName;
		private static string? _rootPath;
		/// <summary>
		/// 根路径
		/// </summary>
		public static string RootPath
		{
			get
			{
				if (_rootPath is null || string.IsNullOrWhiteSpace(_rootPath))
				{
					_rootPath = typeof(Logger).Assembly.GetDirectoryPath();
					if (_rootPath.EndsWith('\\') || _rootPath.EndsWith('/'))
					{
						_rootPath = _rootPath[..^1];
					}
				}
				return _rootPath;
			}
		}
        /// <summary>
        /// 构造方法
        /// </summary>
        [SuppressMessage("Style", "IDE0290:使用主构造函数", Justification = "<挂起>")]
        public Log(LogLevel level, EventId eventID, string categoryName, string message, Exception? exception, int threadID, LoggerScope scope)
        {
			Level = level;
			EventID = eventID;
			CategoryName = categoryName;
			Message = message;
			Exception = exception;
			ThreadID = threadID;
			ScopeName = scope.ScopeName;
			ScopeData = scope.ScopeData;
		}
		/// <summary>
		/// 应用域
		/// </summary>
		/// <param name="messages"></param>
		/// <param name="options"></param>
		/// <param name="data"></param>
		public string ApplyText(string messages, LoggerOptions options, Dictionary<string, object?>? data = null)
		{
			string result = messages;
			data ??= [];
			AddData(data, "Application", options.Application);
			AddData(data, "LogID", ID);
			AddData(data, "Time", CreateTime.ToString("HH:mm:ss"));
			AddData(data, "DateTime", CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
			AddData(data, "RootPath", RootPath);
			AddData(data, "Date", CreateTime.ToString("yyyyMMdd"));
			AddData(data, "Year", CreateTime.Year);
			AddData(data, "Month", CreateTime.Month);
			AddData(data, "Day", CreateTime.Day);
			AddData(data, "Hour", CreateTime.Hour);
			AddData(data, "Minute", CreateTime.Minute);
			AddData(data, "Second", CreateTime.Second);
			AddData(data, "Level", Level.ToString());
			if (!string.IsNullOrWhiteSpace(CategoryName))
			{
				AddData(data, "CategoryName", CategoryName);
			}
			string progressID = GetProgressID();
			AddData(data, "ProgressID", progressID);
			AddData(data, "ThreadID", ThreadID);
			AddData(data, "MachineName", MachineName);
			data = data.Concat(options.CustomData).ToDictionary(k => k.Key, v => v.Value);
			AddData(data, "Scope", ScopeName);
			data = data.Concat(ScopeData).ToDictionary(k => k.Key, v => v.Value);
			result = ApplyText(result, data);
			return result;
		}
#if NET8_0_OR_GREATER
        /// <summary>
        /// 模版表达式
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"\$\{[^\}]+\}")]
        private static partial Regex ExpressionRegex();
#endif
		/// <summary>
		/// 应用文本
		/// </summary>
		/// <param name="text"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		private static string ApplyText(string text, Dictionary<string, object?> data)
		{
			if (data.Count <= 0) return text;
			string result = text;
#if NETSTANDARD
            Regex regex = new(@"\$\{[^\}]+\}");
#else
			Regex regex = ExpressionRegex();
#endif
			MatchCollection matchCollection = regex.Matches(result);
			foreach (object? matchItem in matchCollection)
			{
				if (matchItem is not Match match) continue;
				string valueName = match.Value[2..^1];
				object? value = data.GetObjectValue(valueName);
				string stringValue = string.Empty;
				if (value is string str)
				{
					stringValue = str;
				}
				else if (value is not null)
				{
					stringValue = value.ToJson();
				}
				result = result.Replace(match.Value, stringValue);
			}
			return result;
		}
		private static void AddData(Dictionary<string, object?> data, string key, object? value)
		{
			if (!data.TryAdd(key, value))
			{
				data[key] = value;
			}
		}
	}
}