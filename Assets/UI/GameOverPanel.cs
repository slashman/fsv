using UnityEngine;
using TMPro;

public class GameOverPanel: MonoBehaviour {
	public TMP_Text GameOverText;

	public void Show (string text) {
		gameObject.SetActive(true);
		GameOverText.text = text;
	}
}