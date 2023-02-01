using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    void Update() {
        transform.Translate(-1 * Time.deltaTime, 0, 0);
        if (transform.position.x == -10) {
            Destroy(this);
        }
    }
}
