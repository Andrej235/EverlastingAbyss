using UnityEngine;

public class WanderSteeringBehavior : MonoBehaviour
{
    [SerializeField] private float maxVelocity = 3;
    [SerializeField] private float maxForce = 15;
    [SerializeField] private float circleDistance = 1;
    [SerializeField] private float angleChange = 5;
    [SerializeField] private float wanderAngle = 0;

    private new Rigidbody rigidbody;
    private void Start() => rigidbody = GetComponent<Rigidbody>();

    private void Update()
    {
        Vector3 velocity = rigidbody.velocity;
        Vector3 circleCenter = velocity.normalized * circleDistance; //Center of an imaginary circle in front of the entity which is used to steer the entity

        Vector3 displacement = new Vector3(Mathf.Cos(wanderAngle), 0, Mathf.Sin(wanderAngle)) * circleDistance;
        wanderAngle += (Random.value * angleChange) - (angleChange / 2f); //'- (angleChange / 2f)' is used to map the new angle change from [0, angleChange] to [-angleChange / 2f, angleChange / 2f]

        Vector3 wanderForce = circleCenter + displacement;
        wanderForce = Vector3.ClampMagnitude(wanderForce, maxForce); //Limit influence of the wanderForce

        velocity = Vector3.ClampMagnitude(velocity + wanderForce, maxVelocity); //Limit velocity so that the entity can't move faster than maxVelocity
        rigidbody.velocity = velocity;

        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, wanderForce * 2, Color.magenta);
    }
}
