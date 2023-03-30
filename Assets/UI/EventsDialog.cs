using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventsDialog : MonoBehaviour {
    public GameObject RootPanel;
    public TMP_Text PromptText;
    public GameObject[] Buttons;

    public AudioClip eventSFX;
    public AudioClip selectSFX;

    private GameEvent currentEvent;

    public void ShowPersonEvent(GameEvent e, string memberName) {
        DoShowEvent(e, memberName);
    }

    public void ShowEvent(GameEvent e) {
        DoShowEvent(e, null);
    }

    private void DoShowEvent(GameEvent e, string? memberName) {
        GetComponent<AudioSource>().PlayOneShot(eventSFX);
        currentEvent = e;
        RootPanel.SetActive(true);
        foreach (GameObject button in Buttons) {
            button.SetActive(false);
        }
        for (int i = 0; i < e.options.Length; i++) {
            Buttons[i].SetActive(true);
            Buttons[i].GetComponentInChildren<TMP_Text>().text = Loc.Localize(e.id+"."+(i+1));
        }
        PromptText.text = Loc.Localize(e.id + ".prompt");
        if (memberName != null) {
            PromptText.text = PromptText.text.Replace("XXX", memberName);
        }
    }

    public void Hide () {
        RootPanel.SetActive(false);
    }

    public void OptionSelected (int index) {
        GameEvents.OptionSelected(currentEvent, currentEvent.options[index]);
        GetComponent<AudioSource>().PlayOneShot(selectSFX);
    }
}
