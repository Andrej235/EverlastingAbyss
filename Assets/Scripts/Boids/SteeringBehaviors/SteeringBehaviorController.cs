using UnityEngine;

public class SteeringBehaviorController : MonoBehaviour
{
    [Header("Behavior")]
    [SerializeField] private BaseSteeringBehaviorSO steeringBehavior;

    [Header("General")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float maxVelocity = 3;
    [SerializeField] private float maxForce = 15;

    [Header("Flee")]
    [SerializeField] private float minFleeDistance = 10;

    [Header("Arrival")]
    [SerializeField] private float slowingRadius = 3;

    [Header("Wandering")]
    [SerializeField] private float circleDistance = 1;
    [SerializeField] private float angleChange = 0.1f;
    [SerializeField] private float wanderAngle = 0;

    public Vector3 TargetPosition => targetTransform.position;
    public Vector3 Position => transform.position;
    public float MaxVelocity => maxVelocity;
    public float MaxForce => maxForce;
    public float MinFleeDistance => minFleeDistance;
    public float SlowingRadius => slowingRadius;
    public float CircleDistance => circleDistance;
    public float AngleChange => angleChange;
    public float WanderAngle
    {
        get => wanderAngle;
        set => wanderAngle = value;
    }
    public Vector3 Velocity => velocity;



    private Vector3 velocity;

    public Vector3 GetNewVelocity()
    {
        Vector3 steering = steeringBehavior.GetSteeringDirection(this);

        velocity = Vector3.ClampMagnitude(velocity + steering, maxVelocity);
        Vector3 newPosition = transform.position + velocity;

        return newPosition;
    }
}
