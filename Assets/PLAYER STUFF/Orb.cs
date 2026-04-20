using UnityEngine;

public class Orb : MonoBehaviour
{
    public float SpeedIncrease = 0.75f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy.Score++;
            Enemy.GhostSpeed += SpeedIncrease;
            Debug.Log("Ghost speed is now: " + Enemy.GhostSpeed);
            Destroy(gameObject);
        }
    }
}
