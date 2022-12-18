using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState
    {
        Spawning,
        Waiting,
        Counting
    };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemyloc;
        public int count;
        public float spawnrate;
    }

    public Wave[] waves;
    private GameManager gm;
    private SpawnState state = SpawnState.Counting;
    private int nextWave = 0;

    public float timebetween = 5f;
    public float wavecountdown;

    private float SearchCountdown = 1f;

    private void Start()
    {
        wavecountdown = timebetween;
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(state == SpawnState.Waiting)
        {
            //Check if enemies are alive
            if (!EnemyisAlive())
            {
                //Begin new wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(wavecountdown <= 0)
        {
            //Spawn Start
            if(state != SpawnState.Spawning)
            {
                //Start Spawning enemy
                StartCoroutine(SpawnWaves(waves[nextWave]));
            }
        }
        else
        {
            wavecountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave is Completed");
        state = SpawnState.Counting;
        wavecountdown = timebetween;

        if (nextWave + 1 > waves.Length - 1)
        {
            gm.succeed = true;
            Debug.Log("All Waves Completed!");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyisAlive()
    {
        SearchCountdown -= Time.deltaTime;

        if(SearchCountdown <= 0)
        {
            SearchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWaves(Wave _wave)
    {
        Debug.Log("Spawning Wave" + _wave.name);
        gm.wavecount = _wave.name;
        state = SpawnState.Spawning;
        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyloc);
            yield return new WaitForSeconds(1f / _wave.spawnrate);
        }
        state = SpawnState.Waiting;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning Wave" + _enemy.name);
        Instantiate(_enemy, transform.position, transform.rotation);
    }
}
