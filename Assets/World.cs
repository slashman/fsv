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

    public GameObject FincaPrefab;
    public GameObject DabeibaPrefab;
    public GameObject UramitaPrefab;

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
    private bool fincaGenerated;
    private bool dabeibaGenerated;
    private bool uramitaGenerated;

    void Update () {
        if (World.i.stopTime) {
            return;
        }
        Expedition.i.TimePassed();
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
        GameObject plotPrefab = null;
        // Check based on progress
        float progress = Expedition.i.Progress;
        // 280 victory
        if (progress < 10 && !fincaGenerated) {
            plotPrefab = FincaPrefab;
            fincaGenerated = true;
        } else if (progress > 50 && progress < 70 && !dabeibaGenerated) {
            plotPrefab = DabeibaPrefab;
            dabeibaGenerated = true;
        } else if (progress > 100 && progress < 120 && !militiaGenerated) {
            plotPrefab = MilitiaPrefab;
            militiaGenerated = true;
        } else if (progress > 160 && progress < 180 && !uramitaGenerated) {
            plotPrefab = UramitaPrefab;
            uramitaGenerated = true;
        }
        if (plotPrefab != null) {
            Instantiate(plotPrefab, new Vector3(21, -1.18f, 1.9f), Quaternion.identity, transform);
        }
        if (spawnNextCounter < 0 && plotPrefab == null && false) {
            // int dice = Random.Range(1, 5);
            GameObject prefab = HousePrefab;
            Instantiate(prefab, new Vector3(11, -1.18f, 1.9f), Quaternion.identity, transform);
            spawnNextCounter = Random.Range(20, 30);
        }
    }

    public void StopTime () {
        stopTime = true;
    }

    public void ResumeTime () {
        stopTime = false;
    }
}
