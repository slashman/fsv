using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public EventsDialog EventsDialog;
    public TMP_Text DateText;

    public static GameUI i;

    public void ShowEvent (GameEvent gameEvent) {
        EventsDialog.ShowEvent(gameEvent);
    }

    public void UpdateDate(System.DateTime date) {
        DateText.text = date.ToString("dddd, MMMM dd, yyyy");
    }

    void Start() {
        GameUI.i = this;
    }
}
