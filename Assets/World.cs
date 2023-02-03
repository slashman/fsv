using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject[] BackgroundPrefabs;
    public GameObject[] PathPrefabs;
    public GameObject[] ForegroundPrefabs;
    public GameObject HousePrefab;
    public GameObject MilitiaPrefab;

    private float spawnForegroundCounter;
    private float spawnBackgroundCounter;
    private float spawnPathCounter;
    private float spawnNextCounter;
    public bool stopTime;

    public System.DateTime currentTime;

    void Start () {
        World.i = this;
        spawnNextCounter = Random.Range(0, 3);
        currentTime = new System.DateTime(1952, 04, 12);
        GameUI.i.UpdateDate(currentTime);
        GameUI.i.UpdateStatus();

        for (int i = 0; i < 5; i++) {
            Instantiate(BackgroundPrefabs[Random.Range(0, BackgroundPrefabs.Length)], new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 5.0f), 5.4f), Quaternion.identity, transform);
        }
        for (int i = 0; i < 4; i++) {
            Instantiate(PathPrefabs[Random.Range(0, PathPrefabs.Length)], new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-1.9f, -1.2f), 2f), Quaternion.identity, transform);
        }
    }

    public static World i;

    private bool militiaGenerated;

    void Update () {
        if (World.i.stopTime) {
            return;
        }
        spawnNextCounter -= Time.deltaTime;
        spawnBackgroundCounter -= Time.deltaTime;
        spawnPathCounter -= Time.deltaTime;
        spawnForegroundCounter -= Time.deltaTime;
        int currentDay = currentTime.Day;
        currentTime = currentTime.AddHours(2 * Time.deltaTime);
        if (currentTime.Day != currentDay) {
            GameUI.i.UpdateDate(currentTime);
            Expedition.i.ConsumeFood();
        }
        if (spawnBackgroundCounter < 0) {
            Instantiate(BackgroundPrefabs[Random.Range(0, BackgroundPrefabs.Length)], new Vector3(15, Random.Range(2.0f, 5.0f), 5.4f), Quaternion.identity, transform);
            spawnBackgroundCounter = Random.Range(30, 50);
        }
        if (spawnPathCounter < 0) {
            Instantiate(PathPrefabs[Random.Range(0, PathPrefabs.Length)], new Vector3(15, Random.Range(-1.9f, -1.2f), 2f), Quaternion.identity, transform);
            spawnPathCounter = Random.Range(3, 5);
        }
        if (spawnForegroundCounter < 0) {
            Instantiate(ForegroundPrefabs[Random.Range(0, ForegroundPrefabs.Length)], new Vector3(15, 0, 0), Quaternion.identity, transform);
            spawnForegroundCounter = Random.Range(9, 15);
        }
        if (spawnNextCounter < 0) {
            int dice = Random.Range(1, 5);
            GameObject prefab = null;
            if (dice == 1) {
                if (militiaGenerated) {
                    prefab = HousePrefab;
                } else {
                    prefab = MilitiaPrefab;
                    militiaGenerated = true;
                }
            } else {
                prefab = HousePrefab;
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
