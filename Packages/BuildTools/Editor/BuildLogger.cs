using System;
using System.Reflection;
using UnityEngine;

namespace BuildTools.Editor
{
    internal class BuildLogger
    {
        private const string MESSAGE_TEMPLATE = "[{0}] [{1}.{2}.{3}]: {4}";

        public static void Log(string message, string senderName, string methodName)
        {
            var msg = string.Format(MESSAGE_TEMPLATE,
                "INFO",
                AssemblyName,
                senderName,
                methodName,
                message
            );
            PrintLine(msg);
        }

        public static void LogWarning(string message, string senderName, string methodName)
        {
            var msg = string.Format(MESSAGE_TEMPLATE,
                "WARN",
                AssemblyName,
                senderName,
                methodName,
                message
            );
            PrintLine(msg);
        }

        public static void LogError(string message, string senderName, string methodName)
        {
            var msg = string.Format(MESSAGE_TEMPLATE,
                "ERROR",
                AssemblyName,
                senderName,
                methodName,
                message
            );
            PrintLine(msg);
        }

        private static void PrintLine(string message)
        {
            if (UnityEditorInternal.InternalEditorUtility.inBatchMode)
                Console.WriteLine(message);
            else
                Debug.Log(message);
        }

        private static string AssemblyName => Assembly.GetExecutingAssembly()
            .GetName().Name!
            .Split('.')[0];
    }
}