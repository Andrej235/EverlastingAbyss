using UnityEngine;

public class SeekSteeringBehavior : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float maxVelocity = 3;
    [SerializeField] private float maxForce = 15;

    private new Rigidbody rigidbody;
    private void Start() => rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        Vector3 target = targetTransform.position;
        Vector3 velocity = rigidbody.velocity;
        Vector3 position = transform.position;

        Vector3 desiredVelocity = Vector3.Normalize(target - position) * maxVelocity;
        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        velocity = Vector3.ClampMagnitude(velocity + steering, maxVelocity);
        rigidbody.velocity = velocity;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }
}
