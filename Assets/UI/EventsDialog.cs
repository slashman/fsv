using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventsDialog : MonoBehaviour {
    public GameObject RootPanel;
    public TMP_Text PromptText;
    public GameObject[] Buttons;

    private GameEvent currentEvent;

    public void ShowEvent(GameEvent e) {
        currentEvent = e;
        RootPanel.SetActive(true);
        foreach (GameObject button in Buttons) {
            button.SetActive(false);
        }
        for (int i = 0; i < e.options.Length; i++) {
            Buttons[i].SetActive(true);
            Buttons[i].GetComponentInChildren<TMP_Text>().text = e.options[i].description;
        }
        PromptText.text = e.prompt;
    }

    public void Hide () {
        RootPanel.SetActive(false);
    }

    public void OptionSelected (int index) {
        GameEvents.OptionSelected(currentEvent, currentEvent.options[index]);
    }
}
