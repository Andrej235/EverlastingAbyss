using Scripts;
using UnityEngine;

public class DamagableTest : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 10;

    public float Health
    {
        get => health;
        set
        {
            health = value;

            if (value <= 0)
            {
                print("Object destroyed");
                Destroy(gameObject);
            }
        }
    }

    public void DealDamage(float damage)
    {
        print("Damage dealt: " + damage);
        Health -= damage;
    }
}
