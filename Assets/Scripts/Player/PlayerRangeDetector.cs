using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRangeDetector : MonoBehaviour
{
    [SerializeField] private float range = 1f;
    [SerializeField] private int maxTargetsPerHit = 3;
    [SerializeField] private Transform orientation;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(orientation.position + (orientation.forward.normalized * 0.5f), range);
    }

    public IEnumerable<Collider> GetCollidersInRange()
    {
        Collider[] hitColliders = new Collider[maxTargetsPerHit];
        int hitCount = Physics.OverlapSphereNonAlloc(orientation.position + (orientation.forward.normalized * 0.5f), range, hitColliders);
        return hitColliders.Take(hitCount);
    }
}
