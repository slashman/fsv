using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject[] BackgroundPrefabs;
    public GameObject[] PathPrefabs;
    public GameObject[] ForegroundPrefabs;
    public GameObject HousePrefab;
    public GameObject BurningHousePrefab;
    public GameObject River;
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

    void Awake () {
        World.i = this;
    }

    void Start () {
        stopTime = true;
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
        Instantiate(FincaPrefab, new Vector3(0, -1.18f, 1.9f), Quaternion.identity, transform);

        StartBGSFX();
    }

    public static World i;

    private bool militiaGenerated;
    private bool burningHouseGenerated;
    private bool dabeibaGenerated;
    private bool uramitaGenerated;
    private bool riverGenerated;

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
        if (progress > 20 && progress < 49 && !burningHouseGenerated) {
            plotPrefab = BurningHousePrefab;
            burningHouseGenerated = true;
        } else if (progress > 105 && progress < 135 && !riverGenerated) {
            plotPrefab = River;
            riverGenerated = true;
        }


        if (progress > 50 && progress < 70 && !dabeibaGenerated) {
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
            Instantiate(plotPrefab, new Vector3(10, -1.18f, 1.9f), Quaternion.identity, transform);
        }
        if (spawnNextCounter < 0 && plotPrefab == null) {
            GameObject prefab = HousePrefab;
            Instantiate(prefab, new Vector3(11, -1.18f, 1.9f), Quaternion.identity, transform);
            spawnNextCounter = Random.Range(20, 30);
        }
    }

    public void StartBGSFX() {
        this.GetComponent<AudioSource>().Play();
    }
    public void PauseBGSFX() {
        this.GetComponent<AudioSource>().Pause();
    }

     public void UnPauseBGSFX() {
        this.GetComponent<AudioSource>().UnPause();
    }

    public void StopTime () {
        GameHUD.i.MovementUI.SetActive(false);
        stopTime = true;
    }

    public void ResumeTime () {
        GameHUD.i.MovementUI.SetActive(true);
        stopTime = false;
    }
}
