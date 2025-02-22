using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damageAmount;
    public float destroyTime = 2f;
    private float speed;
    private float travelDistance;
    private float xStartPos;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;
    private Rigidbody2D rb;
    private bool isGravityOn;
    private bool hasHitGround;
    [SerializeField]
    private LayerMask whatIsPlayer;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private Transform damagePosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;
        isGravityOn = false;
        xStartPos = transform.position.x;
        Destroy(gameObject, destroyTime); // Tự hủy sau một thời gian

    }

    private void Update()
    {
        if (!hasHitGround)
        {
            //attackDetails.possition = transform.position;
            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            //if (damageHit)
            //{
            //    Debug.Log("Đã va chạm với: " + damageHit.name);
            //    IDamageable damageable = damageHit.GetComponent<IDamageable>();
            //    if (damageable != null)
            //    {
            //        damageable.Damage(damageAmount);
            //        Destroy(gameObject);
            //    }
            //    else
            //    {
            //        Debug.Log("Không tìm thấy IDamageable trên " + damageHit.name);
            //    }
            //}
            if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHitGround) // Kiểm tra nếu chưa chạm đất thì mới gây sát thương
        {
            Debug.Log("trúng người chơi");
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(damageAmount);
                Debug.Log("Mũi tên trúng: " + collision.name);
                Destroy(gameObject);
            }
        }
    }
    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        damageAmount = damage;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
