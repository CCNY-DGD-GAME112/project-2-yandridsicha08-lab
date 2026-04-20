using System.Collections;
using UnityEngine;

public class ChaserGhost : Enemy
{
    public Transform Player;
    public float VisionRange = 3000f;
    public float TeleportInterval = 5f; // how many seconds between teleports

    void Start()
    {
        StartCoroutine(TeleportRoutine());
    }

    void Update()
    {
        Vector3 directionToPlayer = (Player.position - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, VisionRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                transform.position += directionToPlayer * GhostSpeed * Time.deltaTime;
                transform.LookAt(Player);
            }
        }
    }

    IEnumerator TeleportRoutine()
    {
        while (true)
    {
        yield return new WaitForSeconds(TeleportInterval);

        // Pick a random point inside a circle instead of a rectangle
        Vector2 randomCircle = Random.insideUnitCircle * 42f; // 20f is the safe radius
        transform.position = new Vector3(randomCircle.x, transform.position.y, randomCircle.y);
    }
}

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}