using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject mirror;
    public float speed;

    private float imageWidth;

    private const float HALF_SCREEN = 19.2f / 2.0f;

    void Start () {
        imageWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        transform.Translate(-HALF_SCREEN, 0, 0);
    }

    void Update() {
        if (World.i.stopTime) {
            return;
        }
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x <= -imageWidth - HALF_SCREEN) {
            transform.Translate(imageWidth * 2, 0, 0);
        }
    }
}
