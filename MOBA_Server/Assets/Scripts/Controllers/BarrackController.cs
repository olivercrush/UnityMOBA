using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarrackState { IDLE, SPAWNING, DESTROYED }

public class BarrackController : EntityController {

    // PUBLIC VARIABLES
    [Header("General settings")]
    public BarrackState state;
    public Material red;
    public Material blue;

    [Header("Path settings")]
    public GameObject spawnPoint;
    public GameObject[] waypoints;

    // COMPONENTS
    [Header("Components")]
    public LifeComponent lifeComponent;
    public DeathNotifierComponent deathNotifierComponent;

    // UTILS
    [Header("Utils")]
    public WaveDescriber waveDescriber;

    private int waveSpawned = 0;

    void Start()
    {
        if (this.color == EntityColor.RED)
        {
            GetComponent<Renderer>().material = red;
        }
        else
        {
            GetComponent<Renderer>().material = blue;
        }

        InvokeRepeating("SpawnWave", 0f, 50f);
        notificationCenter = new BarrackNotificationCenter(this);
    }

    // METHODS

    public void TakeHit(int damage)
    {
        // TODO : Use Attack object
        lifeComponent.TakeHit(damage);
    }

    public void DestroyBarrack()
    {
        state = BarrackState.DESTROYED;
        deathNotifierComponent.NotifyDeath();

        Destroy(this.gameObject);
    }

    public void SubscribeDeathNotifier(GameObject observer)
    {
        deathNotifierComponent.AddObserver(observer);
    }

    public void UnsubscribeDeathNotifier(GameObject observer)
    {
        deathNotifierComponent.RemoveObserver(observer);
    }

    // SPAWNING METHODS
    private void SpawnWave()
    {
        StartCoroutine(SpawnUnits());
    }

    IEnumerator SpawnUnits()
    {
        GameObject wavePrefab = new GameObject();

        GameObject wave = Instantiate(wavePrefab, transform);
        wave.name = "Wave " + (waveSpawned + 1);
        waveSpawned++;

        Destroy(wavePrefab);

        for (int i = 0; i < waveDescriber.NORMAL_WAVE.Length; i++)
        {
            GameObject unit = null;
            unit = Instantiate(waveDescriber.NORMAL_WAVE[i], wave.transform);

            UnitController unitController = unit.GetComponent<UnitController>();
            unitController.InitializeUnit(color, spawnPoint.transform, waypoints);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // STATES
    // BarrackStates = different types of wave
}
