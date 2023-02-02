using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject mirror;
    public float speed;

    void Update() {
        if (World.i.stopTime) {
            return;
        }
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= -19.2) {
            transform.Translate(19.2f * 2, 0, 0);
        }
    }
}
