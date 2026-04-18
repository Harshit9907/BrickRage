using UnityEngine;

namespace BrickRage.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float lifeSeconds = 8f;

        private Rigidbody2D rb;
        private float bounceSpeedMultiplier = 0.95f;
        private float damage;
        private float aliveTimer;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector2 direction, float speed, float bounceMultiplier, float projectileDamage)
        {
            bounceSpeedMultiplier = bounceMultiplier;
            damage = projectileDamage;
            rb.linearVelocity = direction.normalized * speed;
        }

        private void Update()
        {
            aliveTimer += Time.deltaTime;
            if (aliveTimer >= lifeSeconds)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Brick brick = collision.collider.GetComponent<Brick>();
            if (brick != null)
            {
                brick.ApplyDamage(damage);
            }

            Vector2 reflected = Vector2.Reflect(rb.linearVelocity.normalized, collision.contacts[0].normal);
            float newSpeed = rb.linearVelocity.magnitude * bounceSpeedMultiplier;
            rb.linearVelocity = reflected * newSpeed;
        }
    }
}
