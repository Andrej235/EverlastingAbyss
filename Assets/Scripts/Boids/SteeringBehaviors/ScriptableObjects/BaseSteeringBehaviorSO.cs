using UnityEngine;

public abstract class BaseSteeringBehaviorSO : ScriptableObject
{
    public abstract Vector3 GetSteeringDirection(SteeringBehaviorController steeringBehaviorController);
}
