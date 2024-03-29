using UnityEngine;


public class GameHUD: MonoBehaviour {
	public GameObject IntroLayer;
	public GameObject PartyStatus;
	public GameObject Map;
	public GameObject MovementUI;

	public static GameHUD i;

	void Awake () {
		GameHUD.i = this;
	} 

	public void HideIntroText () {
		IntroLayer.SetActive(false);
		PartyStatus.SetActive(true);
		Map.SetActive(true);
		World.i.ResumeTime();
	}
}