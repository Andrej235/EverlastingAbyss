using UnityEngine;

[CreateAssetMenu(fileName = "ArrivalSteeringBehaviorSO", menuName = "Steering Behaviors/Arrival")]
public class ArrivalSteeringBehaviorSO : BaseSteeringBehaviorSO
{
    public override Vector3 GetDestination(SteeringBehaviorController steeringBehaviorController) => steeringBehaviorController.TargetPosition;
}
