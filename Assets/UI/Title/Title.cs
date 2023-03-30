using UnityEngine;

public class Title: MonoBehaviour {
	private int State;
	public GameObject LanguageSelection;
	public GameObject MainUI;

	void Start () {
		State = 0;
		MainUI.SetActive(false);
		LanguageSelection.SetActive(true);
	}

	public void SelectEnglish () {
		SelectLanguage("english");
	}

	public void SelectSpanish () {
		SelectLanguage("spanish");
	}

	private void SelectLanguage (string language) {
		MainUI.SetActive(true);
		Loc.LoadLanguage(language);
		State = 1;
		LanguageSelection.SetActive(false);
		LocalizedText.RelocalizeAll();
	}

	public void ClickToStart () {
		if (State == 0) {
			return;
		}
		GameUI.i.StartGame();
	}
}