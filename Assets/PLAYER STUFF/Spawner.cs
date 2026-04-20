using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ChaserGhostPrefab;
    public GameObject BounceGhostPrefab;
    public GameObject OrbPrefab;

    [Header("Starting amounts")]
    public int StartingChasers = 2;
    public int StartingOrbs = 15;

    [Header("Spawning settings")]
    public float OrbPassiveSpawnInterval = 0.5f;
    public float GhostSpawnInterval = 5f; // how often a new ghost spawns as score increases
    public int BounceGhostScoreThreshold = 4; // score needed for bounce ghosts to start spawning
    public float SafeSpawnRadius = 42f; // max distance from center to spawn things

    [Header("Player reference")]
    public Transform Player;

    private int lastScoreChecked = 0;

    void Start()
    {
        // Spawn starting ghosts and orbs
        for (int i = 0; i < StartingChasers; i++)
            SpawnGhost(false);

        for (int i = 0; i < StartingOrbs; i++)
            SpawnOrb();

        StartCoroutine(PassiveOrbSpawner());
        StartCoroutine(ScoreBasedGhostSpawner());
    }

    // Spawns a ghost at a random position in the arena
    void SpawnGhost(bool isBouncer)
    {
        Vector3 spawnPos = GetRandomPosition();
        if (isBouncer)
            Instantiate(BounceGhostPrefab, spawnPos, Quaternion.identity);
        else
        {
            GameObject chaser = Instantiate(ChaserGhostPrefab, spawnPos, Quaternion.identity);
            // Hook up the player reference automatically
            chaser.GetComponent<ChaserGhost>().Player = Player;
        }
    }

    void SpawnOrb()
    {
        Vector3 spawnPos = GetRandomPosition();
        Instantiate(OrbPrefab, spawnPos, Quaternion.identity);
    }

    // Gets a random position inside the arena circle
    Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * SafeSpawnRadius;
        return new Vector3(randomCircle.x, 2.45f, randomCircle.y);
    }

    // Passively spawns orbs every X seconds
    IEnumerator PassiveOrbSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(OrbPassiveSpawnInterval);
            if (Time.timeScale != 0f) // don't spawn if game is over
                SpawnOrb();
        }
    }

    // Spawns more ghosts as score increases
    IEnumerator ScoreBasedGhostSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(GhostSpawnInterval);
            if (Time.timeScale == 0f) continue; // don't spawn if game is over

            if (Enemy.Score > lastScoreChecked)
            {
                lastScoreChecked = Enemy.Score;
                bool spawnBouncer = Enemy.Score >= BounceGhostScoreThreshold;
                SpawnGhost(spawnBouncer);
            }
        }
    }
}
