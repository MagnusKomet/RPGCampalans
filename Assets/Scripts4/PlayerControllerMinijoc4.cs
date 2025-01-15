using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMinijoc4 : MonoBehaviour
{
    public float jumpForce = 7f; // Fuerza del salto
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto desde donde se dispara la bala
    public float bulletSpeed = 10f; // Velocidad de la bala

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            isGrounded = false; 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }

    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                bulletRb.velocity = transform.right * bulletSpeed;

            }
            Destroy(bullet, 0.75f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
