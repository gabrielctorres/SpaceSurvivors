using System.Collections;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private float waveDuration = 30f; // Duração de cada wave em segundos
    [SerializeField] private float timeBetweenWaves = 5f; // Tempo entre waves

    private int currentWave = 0;
    private bool waveActive = false;

    // Eventos personalizados para finalizar a wave
    public event Action OnWaveCompleted;
    public event Action OnPlayerLevelUp;

    private void Start()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            StartWave();
            yield return new WaitForSeconds(waveDuration);
            EndWave();
        }
    }

    private void StartWave()
    {
        currentWave++;
        waveActive = true;
        spawner.StartSpawning();
        Debug.Log($"Wave {currentWave} started!");
    }

    private void EndWave()
    {
        waveActive = false;
        spawner.StopSpawning();

        if (spawner.AllEnemiesDestroyed())
        {
            OnWaveCompleted?.Invoke();
        }
        else
        {
            OnPlayerLevelUp?.Invoke();
        }
    }

    public void PlayerLeveledUp()
    {
        spawner.DestroyAllEnemies();
        StartCoroutine(StartNextWaveAfterUpgrade());
    }

    private IEnumerator StartNextWaveAfterUpgrade()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartWave();
    }
}
