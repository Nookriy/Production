using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawningState {SPAWNING, WAIT, COUNTDOWN };

    [System.Serializable]

    public class Wave
    {
        public string Name;
        public Transform TheEnemy;
        public int Count;
        public float Rate;
    }

    public Wave[] EnemyWaves;
    private int NextWave = 0;

    public Transform[] SpawningPoints;

    public float TimeBetweenWaves = 5f;
    private float WaveCountdown;

    private float SearchCountdown = 1f;

    private SpawningState StateofEnemy = SpawningState.COUNTDOWN;

    void Start()
    {

        if (SpawningPoints.Length == 0)
        {
            WaveCountdown = TimeBetweenWaves;
            Debug.LogError("No Spawn Points Referenced");
        }

        //WaveCountdown = TimeBetweenWaves;
    }

    void Update()
    {
        if(StateofEnemy == SpawningState.WAIT)
        {
            //checking if enemy is still alive
            if (!EnemyAlive())
            {
                WaveComplete();
            }
            else
            {
                return;
            }
        }

        if(WaveCountdown <= 0)
        {
            //Spawning Enemy
            if(StateofEnemy != SpawningState.SPAWNING)
            {
                StartCoroutine(SpawningWave(EnemyWaves[NextWave]));
            }
        }
        else
        {
            WaveCountdown -= Time.deltaTime;
        }
    }

    void WaveComplete()
    {
        Debug.Log("Wave Completed!");

        StateofEnemy = SpawningState.COUNTDOWN;
        WaveCountdown = TimeBetweenWaves;

        if(NextWave + 1 > EnemyWaves.Length - 1)
        {
            NextWave = 0;
            Debug.Log("All Waves Completed! ");
        }
        else
        {
            NextWave++;
        }
    }

    bool EnemyAlive()
    {
        SearchCountdown -= Time.deltaTime;
        if(SearchCountdown <= 0f)
        {
            SearchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawningWave(Wave _wave)
    {
        Debug.Log("Wave Spawned: " + _wave.Name);
        StateofEnemy = SpawningState.SPAWNING;

        for (int i = 0; i < _wave.Count; i++)
        {
            SpawnEnemy(_wave.TheEnemy);
            yield return new WaitForSeconds( 1f/_wave.Rate) ;
        }
        StateofEnemy = SpawningState.WAIT;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        Debug.Log("Enemy Spawn: " + _enemy.name);

        Transform _spawningpoint = SpawningPoints[Random.Range(0, SpawningPoints.Length)];
        Instantiate(_enemy, _spawningpoint.position, _spawningpoint.rotation);
    }
}
