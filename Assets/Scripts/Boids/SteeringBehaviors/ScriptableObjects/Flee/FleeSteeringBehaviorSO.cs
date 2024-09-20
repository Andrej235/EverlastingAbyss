using UnityEngine;

[CreateAssetMenu(fileName = "FleeSteeringBehaviorSO", menuName = "Steering Behaviors/Flee")]
public class FleeSteeringBehaviorSO : BaseSteeringBehaviorSO
{
    public override Vector3 GetSteeringDirection(SteeringBehaviorController steeringBehaviorController)
    {
        Vector3 target = steeringBehaviorController.TargetPosition;
        Vector3 velocity = steeringBehaviorController.Velocity;
        Vector3 position = steeringBehaviorController.Position;

        Vector3 desiredVelocity = Vector3.Normalize(position - target) * steeringBehaviorController.MaxVelocity;
        Vector3 steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, steeringBehaviorController.MaxForce);

        return steering;
    }
}
