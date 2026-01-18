using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tools.PrepareBuild.Editor.Extensions
{
    public static class EditorScriptableObjectExtensions
    {
        public static T FindFirstScriptableObjectAsset<T>(bool optional = false)
            where T : ScriptableObject
        {
            return FindScriptableObjectAssets<T>(optional).FirstOrDefault();
        }

        public static List<T> FindScriptableObjectAssets<T>(bool optional = false)
            where T : ScriptableObject
        {
            var result = new List<T>();

            var assetPaths = FindScriptableObjectAssetPaths<T>(optional);
            foreach (var assetPath in assetPaths)
            {
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset)
                {
                    result.Add(asset);
                }
            }

            return result;
        }

        public static List<string> FindScriptableObjectAssetPaths<T>(bool optional = false)
            where T : ScriptableObject
        {
            var assetName = typeof(T).Name;
            var assetGUIDs = AssetDatabase.FindAssets($"t:{assetName}");

            if (assetGUIDs.Length != 0)
            {
                return assetGUIDs.Select(AssetDatabase.GUIDToAssetPath).ToList();
            }

            if (!optional)
            {
                Debug.LogError($"{assetName} not found!");
                return null;
            }
            
            return new List<string>();
        }
    }
}