using Scripts;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerRangeDetector))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockback = 1f;
    [SerializeField] private float attackRate = 1f;

    private bool isAttackAvailable = true;

    private PlayerRangeDetector rangeDetector;
    private void Start() => rangeDetector = GetComponent<PlayerRangeDetector>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    private void Attack()
    {
        if (!isAttackAvailable)
            return;

        isAttackAvailable = false;
        Invoke(nameof(ResetAttack), 1 / attackRate);

        IEnumerable<Collider> hit = rangeDetector.GetCollidersInRange();

        foreach (Collider collider in hit)
        {
            if (!collider.TryGetComponent(out IDamageable damageable))
                continue;

            //Vector3 direction = orientation.forward; //Add knockback based on the direction player is looking in
            Vector3 direction = collider.transform.position - transform.position; //Add knockback based on player's position at the time of the attack
            damageable.TakeDamage(new()
            {
                Value = damage,
                KnockbackIntensity = knockback,
                KnockbackDirection = direction
            });
        }
    }

    private void ResetAttack() => isAttackAvailable = true;
}
