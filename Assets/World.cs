using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject TreePrefab;
    public GameObject HousePrefab;
    public GameObject MilitiaPrefab;

    private float spawnNextCounter;
    public bool stopTime;

    void Start () {
        World.i = this;
        spawnNextCounter = Random.Range(0, 3);
    }

    public static World i;

    private bool militiaGenerated;

    void Update () {
        if (World.i.stopTime) {
            return;
        }
        spawnNextCounter -= Time.deltaTime;
        if (spawnNextCounter < 0) {
            int dice = Random.Range(1, 5);
            GameObject prefab = null;
            if (dice == 1 || dice == 2) {
                prefab = TreePrefab;
            } else if (dice == 3) {
                prefab = HousePrefab;
            } else {
                if (militiaGenerated) {
                    prefab = TreePrefab;
                } else {
                    prefab = MilitiaPrefab;
                    militiaGenerated = true;
                }
            }
            Instantiate(prefab, new Vector3(11, -1.18f, 0), Quaternion.identity, transform);
            spawnNextCounter = Random.Range(5, 10);
        }
    }

    public void StopTime () {
        stopTime = true;
    }

    public void ResumeTime () {
        stopTime = false;
    }
}
