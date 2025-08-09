using System.Collections.Generic;
using UnityEngine;

public class MusicBeatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private float _bpm = 87f;
    [SerializeField] private int _beatsVisible = 4;
    [SerializeField] private int _beatStep = 2;
    [SerializeField] private float _randomOffsetMax = 0.15f;

    private float _beatInterval;
    private float _nextBeatTime;

    private void Start()
    {
        _beatInterval = 60f / _bpm;
        _nextBeatTime = 0f;

        _musicSource.pitch = 1f;
    }

    private void Update()
    {
        if (!_musicSource.isPlaying)
            return;

        float musicTime = _musicSource.time;

        if (musicTime >= _nextBeatTime)
        {
            SpawnEnemy();

            _nextBeatTime += _beatInterval * _beatStep;
            _nextBeatTime += Random.Range(-_randomOffsetMax, _randomOffsetMax);
        }
    }

    private void SpawnEnemy()
    {
        if (_spawnPoints.Count == 0)
        {
            Debug.LogWarning("Spawn points list is empty!");
            return;
        }

        int index = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[index];

        GameObject enemy = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);

        Destroy(enemy, _beatInterval * _beatsVisible);
    }
}