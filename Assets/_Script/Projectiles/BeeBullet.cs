using UnityEngine;

public class BeeBullet : ProjectileBase
{
    public override void Initialize(float damage, float projectileSpeed, int facingDirection)
    {
        base.Initialize(damage, projectileSpeed, facingDirection);
        rb.velocity = Vector2.down * speed; // Đặt vận tốc ngay khi bắn
    }

    private void FixedUpdate()
    {
        if (rb.velocity == Vector2.zero)
        {
            rb.velocity = Vector2.down * speed;
        }
    }
}
