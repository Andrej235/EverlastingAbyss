using UnityEngine;

[CreateAssetMenu(fileName = "WanderSteeringBehaviorSO", menuName = "Steering Behaviors/Wander")]
public class WanderSteeringBehaviorSO : BaseSteeringBehaviorSO
{
    public override Vector3 GetSteeringDirection(SteeringBehaviorController steeringBehaviorController)
    {
        Vector3 velocity = steeringBehaviorController.Velocity;
        Vector3 circleCenter = velocity.normalized * steeringBehaviorController.CircleDistance; //Center of an imaginary circle in front of the entity which is used to steer the entity

        Vector3 displacement = new Vector3(Mathf.Cos(steeringBehaviorController.WanderAngle), 0, Mathf.Sin(steeringBehaviorController.WanderAngle)) * steeringBehaviorController.CircleDistance;
        steeringBehaviorController.WanderAngle += (Random.value * steeringBehaviorController.AngleChange) - (steeringBehaviorController.AngleChange / 2f); //'- (angleChange / 2f)' is used to map the new angle change from [0, angleChange] to [-angleChange / 2f, angleChange / 2f]

        Vector3 wanderForce = circleCenter + displacement;
        wanderForce = Vector3.ClampMagnitude(wanderForce, steeringBehaviorController.MaxForce); //Limit influence of the wanderForce

        return wanderForce;
    }
}
