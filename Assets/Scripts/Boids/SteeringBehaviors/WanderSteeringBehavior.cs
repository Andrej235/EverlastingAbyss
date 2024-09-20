using UnityEngine;

public class WanderSteeringBehavior : MonoBehaviour
{
    [SerializeField] private float maxVelocity = 3;
    [SerializeField] private float maxForce = 15;
    [SerializeField] private float mass = 15;
    [SerializeField] private float circleDistance = 1;
    [SerializeField] private float angleChange = 5;
    private float wanderAngle = 0;

    private new Rigidbody rigidbody;
    private void Start() => rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        Vector3 velocity = rigidbody.velocity;
        Vector3 position = transform.position;

        Vector3 circleCenter = velocity.normalized * 5;

        float displacementMultiplier = new Vector3(0, 0, circleDistance).magnitude;
        Vector3 displacement = new Vector3(Mathf.Cos(wanderAngle), 0, Mathf.Sin(wanderAngle)) * circleDistance;
        wanderAngle += (Random.Range(0f, 1f) * angleChange) - (angleChange / 2f);

        Vector3 wanderForce = circleCenter + displacement;
        wanderForce = Vector3.ClampMagnitude(wanderForce, maxForce);
        wanderForce /= mass;

        velocity = Vector3.ClampMagnitude(velocity + wanderForce, maxVelocity);
        rigidbody.velocity = velocity;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, wanderForce * 2, Color.magenta);
    }
}
