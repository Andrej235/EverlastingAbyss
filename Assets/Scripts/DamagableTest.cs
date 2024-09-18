using Scripts;
using UnityEngine;

public class DamagableTest : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 10;
    private new Rigidbody rigidbody;
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;

    public float Health
    {
        get => health;
        set
        {
            health = value;

            if (value <= 0)
            {
                print("Object destroyed");
                //Destroy(gameObject);
            }
        }
    }

    private void Start() => rigidbody = GetComponent<Rigidbody>();

    public void DealDamage(Damage damage)
    {
        print("Damage dealt: " + damage.Value);
        Health -= damage.Value;

        rigidbody.AddForce(damage.KnockbackDirection * damage.KnockbackIntensity, forceMode);

        Debug.DrawLine(transform.position, transform.position + (damage.KnockbackDirection * damage.KnockbackIntensity), Color.red, 100);
    }
}
