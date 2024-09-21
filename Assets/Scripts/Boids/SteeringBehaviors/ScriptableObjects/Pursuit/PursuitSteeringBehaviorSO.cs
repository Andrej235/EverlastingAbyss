using UnityEngine;

[CreateAssetMenu(fileName = "PursuitSteeringBehaviorSO", menuName = "Steering Behaviors/Pursuit")]
public class PursuitSteeringBehaviorSO : BaseSteeringBehaviorSO
{
    public override Vector3 GetDestination(SteeringBehaviorController steeringBehaviorController)
    {
        Vector3 target = steeringBehaviorController.TargetPosition;
        Vector3 targetVelocity = steeringBehaviorController.TargetVelocity;
        Vector3 position = steeringBehaviorController.Position;

        float distance = Vector3.Distance(position, target);
        int T = Mathf.FloorToInt(distance / steeringBehaviorController.MaxVelocity);

        Vector3 futurePosition = (targetVelocity * T) + target;
        return futurePosition;
    }
}
