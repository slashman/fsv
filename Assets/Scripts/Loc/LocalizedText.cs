using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
	private string originalKey;
	private string[] originalKeys;
    void Start() {
		TryLocalizeText();
		TryLocalizeDropDown();
    }

	public static void RelocalizeAll () {
		LocalizedText[] allLocs = FindObjectsOfType<LocalizedText>();
		foreach (LocalizedText text in allLocs) {
			text.TryLocalizeText();
			text.TryLocalizeDropDown();
		}
	}

	private void TryLocalizeText() {
		Text text = GetComponent<Text>();
		if (!text) { 
			TryLocalizeTMPText();
			return;
		}

		if (originalKey == null) {
			originalKey = text.text;
		}

		text.text = Loc.Localize(originalKey);
	}

	private void TryLocalizeTMPText() {
		TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
		if (!text) { 
			return;
		}

		if (originalKey == null) {
			originalKey = text.text;
		}

		text.text = Loc.Localize(originalKey);
	}

	private void TryLocalizeDropDown() {
		Dropdown dd = GetComponent<Dropdown>();
		if (!dd) { return; }

		if (originalKeys == null) {
			originalKeys = new string [dd.options.Count];
			for (int i = 0; i < dd.options.Count; i++) {
				originalKeys[i] = dd.options[i].text;
			}
		}
		for (int i = 0; i < dd.options.Count; i++) {
			dd.options[i].text = Loc.Localize(originalKeys[i]);
		}

		dd.transform.Find("Label").GetComponent<Text>().text = dd.options[dd.value].text;
	}
}
