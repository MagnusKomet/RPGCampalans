using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bala_jugador : MonoBehaviour
{
    public float move_speed;
    public float zona_morta;
    private Vector3 moveDirection;
    private Logica logic;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = transform.right;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logica>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * move_speed * Time.deltaTime;

        if (transform.position.x > zona_morta)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "meteorit")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "ovni")
        {
            comportament_ovni ovni = collision.gameObject.GetComponent<comportament_ovni>();
            ovni.vida_ovni--;
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "escut")
        {
            Destroy(gameObject);
        }
    }
}
