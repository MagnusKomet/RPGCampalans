using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class bala_enemiga : MonoBehaviour
{
    public float move_speed;
    public float zona_morta;
    private Logica logic;
    private GameObject jugador;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("jugador");
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logica>();

        Vector3 direccio = jugador.transform.position - transform.position;

        rb.velocity = new Vector2(direccio.x, direccio.y).normalized * move_speed;

        float rotacio = Mathf.Atan2(-direccio.y, -direccio.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotacio);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < zona_morta)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "jugador")
        {
            logic.GameOver();
            Destroy(gameObject);
        }
    }
}
