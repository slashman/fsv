using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public EventsDialog EventsDialog;

    public static GameUI i;

    public void ShowEvent (GameEvent gameEvent) {
        EventsDialog.ShowEvent(gameEvent);
    }

    void Start() {
        GameUI.i = this;
    }
}
