using UnityEngine;

public class BounceGhost : Enemy
{
    private Vector3 moveDirection;
   

    void Start()
    {
        
        // Pick a random starting direction
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector3(
            Mathf.Cos(randomAngle),
            0f,
            Mathf.Sin(randomAngle)
        ).normalized;
    }

    void Update()
    {
        transform.position += moveDirection * GhostSpeed * Time.deltaTime;
        transform.LookAt(transform.position + moveDirection);
    }

    void OnCollisionEnter(Collision collision)
    {
        // When it hits a wall, reflect its direction off the wall's surface
        // Like a billiard ball bouncing
        Vector3 normal = collision.contacts[0].normal;
        moveDirection = Vector3.Reflect(moveDirection, normal);
        moveDirection.y = 0f; // keep it flat, no flying
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
