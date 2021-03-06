﻿using UnityEditor;
using UnityEngine;

namespace Popcron.Console
{
    [CustomEditor(typeof(Settings))]
    public class SettingsInspector : Editor
    {
        private static bool ShowAllowanceFilter
        {
            get => EditorPrefs.GetBool("Popcron.Console.ShowAllowanceFilter");
            set => EditorPrefs.SetBool("Popcron.Console.ShowAllowanceFilter", value);
        }

        public static void Show(SerializedObject serializedObject)
        {
            Settings settings = serializedObject.targetObject as Settings;
            SerializedProperty userColor = serializedObject.FindProperty("userColor");
            SerializedProperty logColor = serializedObject.FindProperty("logColor");
            SerializedProperty warnColor = serializedObject.FindProperty("warnColor");
            SerializedProperty errorColor = serializedObject.FindProperty("errorColor");
            SerializedProperty exceptionColor = serializedObject.FindProperty("exceptionColor");
            SerializedProperty consoleStyle = serializedObject.FindProperty("consoleStyle");
            SerializedProperty startupCommands = serializedObject.FindProperty("startupCommands");
            SerializedProperty scrollAmount = serializedObject.FindProperty("scrollAmount");
            SerializedProperty historySize = serializedObject.FindProperty("historySize");
            SerializedProperty logToFile = serializedObject.FindProperty("logToFile");
            SerializedProperty reportUnknownCommand = serializedObject.FindProperty("reportUnknownCommand");
            SerializedProperty checkForOpenInput = serializedObject.FindProperty("checkForOpenInput");
            SerializedProperty blacklistedScenes = serializedObject.FindProperty("blacklistedScenes");

            //show the colours first
            EditorGUILayout.PropertyField(userColor, new GUIContent("User input"));
            EditorGUILayout.PropertyField(logColor, new GUIContent("Log and prints"));
            EditorGUILayout.PropertyField(warnColor, new GUIContent("Warnings"));
            EditorGUILayout.PropertyField(errorColor, new GUIContent("Errors"));
            EditorGUILayout.PropertyField(exceptionColor, new GUIContent("Exceptions"));

            //show the other garbage
            EditorGUILayout.PropertyField(consoleStyle, new GUIContent("Style"));
            EditorGUILayout.PropertyField(startupCommands, new GUIContent("Commands at startup"), true);
            EditorGUILayout.PropertyField(blacklistedScenes, new GUIContent("Blacklisted scenes"), true);

            //allowance filter
            ShowAllowanceFilter = EditorGUILayout.Foldout(ShowAllowanceFilter, "Filter");
            if (ShowAllowanceFilter)
            {
                SerializedProperty allowAsserts = serializedObject.FindProperty("allowAsserts");
                SerializedProperty allowErrors = serializedObject.FindProperty("allowErrors");
                SerializedProperty allowExceptions = serializedObject.FindProperty("allowExceptions");
                SerializedProperty allowLogs = serializedObject.FindProperty("allowLogs");
                SerializedProperty allowWarnings = serializedObject.FindProperty("allowWarnings");

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(allowAsserts, new GUIContent("Asserts"));
                EditorGUILayout.PropertyField(allowErrors, new GUIContent("Errors"));
                EditorGUILayout.PropertyField(allowExceptions, new GUIContent("Exceptions"));
                EditorGUILayout.PropertyField(allowLogs, new GUIContent("Logs"));
                EditorGUILayout.PropertyField(allowWarnings, new GUIContent("Warnings"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(reportUnknownCommand, new GUIContent("Report unknown command"));
            EditorGUILayout.PropertyField(scrollAmount, new GUIContent("Scroll amount"));
            EditorGUILayout.PropertyField(historySize, new GUIContent("History size"));

            //show the keys that gon be used
            EditorGUILayout.PropertyField(checkForOpenInput, new GUIContent("Built-in open check"));
            if (checkForOpenInput.boolValue)
            {
                SerializedProperty consoleChararacters = serializedObject.FindProperty("consoleChararacters");

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(consoleChararacters, new GUIContent("Console open keys"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(logToFile, new GUIContent("Log to file"));
            if (logToFile.boolValue)
            {
                SerializedProperty logFilePathPlayer = serializedObject.FindProperty("logFilePathPlayer");
                SerializedProperty logFilePathEditor = serializedObject.FindProperty("logFilePathEditor");

                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(logFilePathPlayer, new GUIContent("Log path for player"));
                EditorGUILayout.PropertyField(logFilePathEditor, new GUIContent("Log path for editor"));
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            Show(serializedObject);
        }
    }
}
