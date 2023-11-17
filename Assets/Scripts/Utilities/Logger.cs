using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ECSExperiments.Utilities
{
    public static class Logger
    {
        public static void DebugLog<T>(T caller, string message)
        {
            if (!(caller is Object))
            {
                Debug.Log($"[{typeof(T)}] {message}");
            }
            else
            {
                Debug.Log($"[{typeof(T)}] {message}", caller as Object);
            }
        }

        public static void DebugLogWarning<T>(T caller, string message)
        {
            Debug.LogWarning($"[{typeof(T)}] {message}");
        }

        public static void DebugLogError<T>(T caller, string message)
        {
            Debug.LogError($"[{typeof(T)}] {message}");
        }

        public static void DebugLogException<T>(T caller, Exception exception)
        {
            Debug.LogError($"[{typeof(T)}] {exception.Message}");
            Debug.LogException(exception);
        }

        public static void DebugLog(string caller, string message)
        {
            Debug.Log($"[{caller}] {message}");
        }

        public static void DebugLogWarning(string caller, string message)
        {
            Debug.LogWarning($"[{caller}] {message}");
        }

        public static void DebugLogError(string caller, string message)
        {
            Debug.LogError($"[{caller}] {message}");
        }
    }
}