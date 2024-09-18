using Scripts;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockback = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private int maxTargetsPerHit = 3;
    [SerializeField] private Transform orientation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(orientation.position + (orientation.forward.normalized * 0.5f), range);
    }

    private void Attack()
    {
        Collider[] hitColliders = new Collider[maxTargetsPerHit];
        int hitCount = Physics.OverlapSphereNonAlloc(orientation.position + (orientation.forward.normalized * 0.5f), range, hitColliders);
        //print(hitCount);

        if (hitCount == 0)
        {
            return;
        }

        foreach (Collider collider in hitColliders.Take(hitCount))
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                //Vector3 direction = orientation.forward; //Add knockback based on the direction player is looking in
                Vector3 direction = collider.transform.position - transform.position; //Add knockback based on player's position at the time of the attack
                damageable.DealDamage(new()
                {
                    Value = damage,
                    KnockbackIntensity = knockback,
                    KnockbackDirection = direction
                });
            }
        }
    }
}
