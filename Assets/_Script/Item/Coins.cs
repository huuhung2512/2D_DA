using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("cham dat");
            rb.gravityScale = 0; // Vô hiệu hóa trọng lực khi chạm đất
            rb.velocity = Vector2.zero; // Dừng di chuyển
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Hủy coin khi chạm vào người chơi
        }
    }
}

