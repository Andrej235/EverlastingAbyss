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
    public Vector3 Velocity
    {
        get; set;
    }
    public Vector3 TargetVelocity => targetVelocity;

    public Vector3 GetNewDestination() => steeringBehavior.GetDestination(this);

    private Vector3 targetVelocity;
    private Rigidbody targetRigidbody;
    private void Update() => targetVelocity = targetRigidbody.velocity.normalized;
    private void Start() => targetRigidbody = targetTransform.GetComponent<Rigidbody>();
}
