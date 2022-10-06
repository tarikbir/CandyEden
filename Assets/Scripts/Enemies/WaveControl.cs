using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTimer;

public class WaveControl : SingletonMB<WaveControl>
{
    [Header("Monsters")]
    public List<Enemy> CurrentMonsters;
    public Enemy[] SpawnPrefabs;
    public Vector3 MinimumPoints;
    public Vector3 MaximumPoints;

    public Enemy FinalBoss;
    public Vector3 BossSpawnPoint;

    [Header("Time")]
    public float[] WaveTimes;

    private int _currentWave;
    private Timer _waveTimer;

    public void Start()
    {
        _currentWave = 0;
        NextTimer();
    }

    public Enemy GetRandomEnemy()
    {
        if (CurrentMonsters.Any())
        {
            return CurrentMonsters.OrderBy(e => Random.Range(0, CurrentMonsters.Count)).Take(1).FirstOrDefault();
        }

        return null;
    }

    private void NextTimer()
    {
        if (_currentWave >= WaveTimes.Length)
        {
            var boss = Instantiate(FinalBoss, BossSpawnPoint, Quaternion.identity, transform);
            boss.name = boss.Name;
            CurrentMonsters.Add(boss);
            UIControl.Instance.SetWaveText(++_currentWave);
            return;
        }

        float minimum = (_currentWave / 6f) + 1;
        float maximum = (_currentWave / 4f) + 1;
        int amount = (int) Random.Range(minimum, maximum);
        for (int i = 0; i < amount; i++)
        {
            SpawnMonsters();
        }

        _waveTimer = Timer.Register(WaveTimes[_currentWave++], NextTimer);
        UIControl.Instance.SetWaveText(_currentWave);
    }

    private void SpawnMonsters()
    {
        var randomX = Random.Range(MinimumPoints.x, MaximumPoints.x);
        var randomZ = Random.Range(MinimumPoints.z, MaximumPoints.z);
        var randomMonsterIndex = Random.Range(0, SpawnPrefabs.Length);
        var spawnPosition = new Vector3(randomX, 0, randomZ);

        var monster = Instantiate(SpawnPrefabs[randomMonsterIndex], spawnPosition, Quaternion.identity, transform);
        monster.name = SpawnPrefabs[randomMonsterIndex].Name;
        CurrentMonsters.Add(monster);
        //Debug.Log($"Spawning {monster.name} at {spawnPosition}");
    }

    internal void EnableRagingMonsters()
    {
        foreach (var monster in CurrentMonsters)
        {
            monster.Enrage();
        }
    }
}