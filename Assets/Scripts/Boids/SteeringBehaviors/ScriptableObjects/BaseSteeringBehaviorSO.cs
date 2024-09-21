using UnityEngine;

public abstract class BaseSteeringBehaviorSO : ScriptableObject
{
    public abstract Vector3 GetDestination(SteeringBehaviorController steeringBehaviorController);
}
