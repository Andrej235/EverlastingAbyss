using UnityEngine;

[CreateAssetMenu(fileName = "ArrivalSteeringBehaviorSO", menuName = "Steering Behaviors/Arrival")]
public class ArrivalSteeringBehaviorSO : BaseSteeringBehaviorSO
{
    public override Vector3 GetSteeringDirection(SteeringBehaviorController steeringBehaviorController)
    {
        Vector3 target = steeringBehaviorController.TargetPosition;
        Vector3 velocity = steeringBehaviorController.Velocity;
        Vector3 position = steeringBehaviorController.Position;

        Vector3 desiredVelocity = target - position;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = distance < steeringBehaviorController.SlowingRadius
            ? distance / steeringBehaviorController.SlowingRadius * steeringBehaviorController.MaxVelocity * Vector3.Normalize(desiredVelocity)
            : Vector3.Normalize(desiredVelocity) * steeringBehaviorController.MaxVelocity;

        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, steeringBehaviorController.MaxForce);

        return steering;
    }
}
