using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public string TriggerEventId;
    public float Speed;
    private bool Triggered;

    void Update() {
        if (World.i.stopTime) {
            return;
        }
        transform.Translate(-Speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= -60) {
            Destroy(gameObject);
            return;
        }
        if (!Triggered && TriggerEventId != null && TriggerEventId != "" && transform.position.x <= -5.5f) {
            Triggered = true;
            GameUI.i.ShowEvent(GameEvents.Get(TriggerEventId));
            World.i.StopTime();
        }
    }
}
