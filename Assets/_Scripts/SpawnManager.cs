using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] Transform allSpawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        
        GameEvents.OnWaveStart += Spawn;

        ResetSpawnPoints();
    }

    void Spawn() {
        int numToSpawn = Random.Range(1, SpawnPoints.Count);

        for (int i = 0; i < numToSpawn; i++) {
            Transform randPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
            GameObject s = Instantiate(Enemies[0], randPoint.position, Quaternion.identity);
            s.transform.SetParent(this.transform);
            SpawnPoints.Remove(randPoint);
        }
        ResetSpawnPoints();

    }

    void ResetSpawnPoints() {
        SpawnPoints.Clear();
        for (int i = 0; i < allSpawnpoints.childCount; i++) {
            SpawnPoints.Add(allSpawnpoints.GetChild(i));
        }
    }

    void StartOfWave() {

    }

    void EndOfWave() {

    }
}
