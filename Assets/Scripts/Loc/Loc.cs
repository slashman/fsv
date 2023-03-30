using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Language {
	ENGLISH,
	SPANISH
}

[System.Serializable]
public class LocalizationData {
	public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem {
	public string key;
	public string value;
}

public static class Loc
{
	private static Dictionary<string, string> dictionary;
	private static string textNotFound = "Localized text not found";

	public static bool isReady = false;

	public static System.Globalization.CultureInfo CurrentCulture;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void LoadLanguageFirst() {
		LoadLanguage("spanish");
	}

	public static void LoadLanguage (string language) {
		dictionary = new Dictionary<string, string>();
		Path.Combine(Application.streamingAssetsPath, "Localization/" + language + ".json");
		LoadFile("Localization/" + language + ".json");
		Loc.CurrentCulture = new System.Globalization.CultureInfo(Loc.Localize("culture"));
	}

	private static void LoadFile(string filename) {
		string pathName = Path.Combine(Application.streamingAssetsPath, filename);
		if (File.Exists(pathName)) {
			string dataAsJson = File.ReadAllText(pathName);
			LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

			for (int i = 0; i < loadedData.items.Length; i++) {
				try {
					string value = loadedData.items[i].value;
					value = value.Replace("$NL", "\n");
					dictionary.Add(loadedData.items[i].key, value);
				} catch (System.ArgumentException ae) {
					Debug.LogError("Duplicate Key [" + loadedData.items[i].key + "].");
				}
			}

			isReady = true;
		} else {
			Debug.LogError("File [" + pathName + "] not found.");
		}
	}

	public static bool HasKey(string key) {
		return dictionary.ContainsKey(key);
	}

	public static string Localize(string key, params object[] replacements) {
		if (!isReady) { return key; }
		string result = "{" + key + "}";
		if (dictionary.ContainsKey(key)) {
			result = dictionary[key];
			if (replacements.GetLength(0) > 0) {
				return string.Format(result, replacements);
			} else {
				return result;
			}
		}
		return result;
	}
}
