using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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

public class Loc: MonoBehaviour
{
	private static Dictionary<string, string> dictionary;
	private static string textNotFound = "Localized text not found";

	public static bool isReady = false;

	public static System.Globalization.CultureInfo CurrentCulture;

	public static Loc i;

	public delegate void LanguageLoaded ();
	public LanguageLoaded OnLanguageLoaded;

	void Start() {
		Loc.i = this;
	}

	public void LoadLanguage (string language, LanguageLoaded onLanguageLoaded) {
		this.OnLanguageLoaded = onLanguageLoaded;
		dictionary = new Dictionary<string, string>();
		Path.Combine(Application.streamingAssetsPath, "Localization/" + language + ".json");
		LoadFile("Localization/" + language + ".json");
	}

	private void LoadFile(string filename) {
		string pathName = Path.Combine(Application.streamingAssetsPath, filename);
		if (File.Exists(pathName)) {
			string dataAsJson = File.ReadAllText(pathName);
			LoadDictionary(dataAsJson);
		} else {
			// We are very likely running on Android or WEBGL, so attempt to fetch it
			StartCoroutine(FetchFile(pathName));
		}
	}

	private IEnumerator FetchFile(string path) {
		UnityWebRequest unityWebRequest = UnityWebRequest.Get(path);
		yield return unityWebRequest.Send();
		if (unityWebRequest.isDone) {
			LoadDictionary(unityWebRequest.downloadHandler.text);
		}
	}

	private void LoadDictionary(string dataAsJson) {
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
		Loc.CurrentCulture = new System.Globalization.CultureInfo(Loc.Localize("culture"));
		if (OnLanguageLoaded != null) {
			OnLanguageLoaded();
		}
		LocalizedText.RelocalizeAll();
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
