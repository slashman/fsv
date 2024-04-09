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

	public void SelectTurkish () {
		SelectLanguage("turkish");
	}

	public void SelectGerman () {
		SelectLanguage("german");
	}

	public void SelectCatalan () {
		SelectLanguage("catalan");
	}

	private void SelectLanguage (string language) {
		Loc.i.LoadLanguage(language, () => {
			MainUI.SetActive(true);
			State = 1;
			LanguageSelection.SetActive(false);
		});
	}

	public void ClickToStart () {
		if (State == 0) {
			return;
		}
		GameUI.i.StartGame();
	}
}