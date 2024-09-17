using Scripts;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float range = 1f;
    [SerializeField] private float attackRate = 1f;
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
        Gizmos.DrawLine(orientation.position, orientation.position + (orientation.forward * range));
    }

    private void Attack()
    {
        Debug.DrawLine(orientation.position, orientation.position + (orientation.forward * range), Color.red, 3f);

        if (!Physics.Raycast(transform.position, orientation.forward, out RaycastHit hit, range) || !hit.collider.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        damageable.DealDamage(damage);
    }
}
