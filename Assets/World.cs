using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject TreePrefab;

    private float spawnTreeCounter;

    void Update () {
        spawnTreeCounter -= Time.deltaTime;
        if (spawnTreeCounter < 0) {
            Instantiate(TreePrefab, new Vector3(11, -1.18f, 3), Quaternion.identity, transform);
            spawnTreeCounter = 20;
        }
    }
}
