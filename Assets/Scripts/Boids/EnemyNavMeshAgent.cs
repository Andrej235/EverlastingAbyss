using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SteeringBehaviorController))]
public class EnemyNavMeshAgent : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    private SteeringBehaviorController steeringBehaviorController;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        steeringBehaviorController = GetComponent<SteeringBehaviorController>();
    }

    private void Update()
    {
        agent.destination = steeringBehaviorController.GetNewDestination();

        //Debug.DrawRay(transform.position, steeringBehaviorController.GetNewVelocity(), Color.green);
        //Debug.DrawRay(transform.position, agent.steeringTarget, Color.red);
    }
}
