using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public EventsDialog EventsDialog;
    public InventoryDialog InventoryDialog;
    public TradeDialog TransferDialog;
    public TMP_Text DateText;
    public TMP_Text FoodText;

    public static GameUI i;

    public GameObject TitlePanel;
    public GameObject GameHUD;
    public GameObject Main2DScene;
    public GameOverPanel GameOverPanel;
    public GameObject VictoryPanel;
    public Transform ProgressIndicator;

    public void ShowPersonEvent (GameEvent gameEvent, string memberName) {
        EventsDialog.ShowPersonEvent(gameEvent, memberName);
    }

    public void ShowEvent (GameEvent gameEvent) {
        EventsDialog.ShowEvent(gameEvent);
    }

    public void UpdateDate(System.DateTime date) {
        DateText.text = date.ToString("dddd, MMMM dd, yyyy");
    }

    public void UpdateStatus () {
        FoodText.text = Expedition.i.GetTotalFood().ToString();
    }

    public void ShowGameOver (string gameOverDescription) {

        GameOverPanel.Show(gameOverDescription);
    }

    public void ShowVictory () {
        VictoryPanel.SetActive(true);
    }

    public void RestartGame () {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void UpdateProgress () {
        ProgressIndicator.localPosition = new Vector3(0 + Expedition.i.Progress * 5.0f - 700, 0, 0);
    }

    public void ShowInventory () {
        World.i.StopTime();
        InventoryDialog.Show();
    }

    public void ShowTransfer (string id) {
        World.i.StopTime();
        TransferDialog.Show(id);
    }

    public void StartGame () {
        TitlePanel.SetActive(false);
        GameHUD.SetActive(true);
        Main2DScene.SetActive(true);
    }

    void Start() {
        GameUI.i = this;
        TitlePanel.SetActive(true);
    }
}
