using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moviment_meteorit : MonoBehaviour
{
    public float zona_morta;
    public float velocitat_rotacio;
    private generar_meteorits generar_meteorits;
    Rigidbody2D rb;
    float angleMax = 15f;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        GameObject generador = GameObject.FindGameObjectWithTag("generador");
        generar_meteorits = generador.GetComponent<generar_meteorits>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        angle = Random.Range(-angleMax, angleMax);
        rb.velocity = Vector2.left * generar_meteorits.velocitat_inicial + Vector2.down * angle;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, velocitat_rotacio * Time.deltaTime);
        //Destruir meteorits 
        if (transform.position.x < zona_morta)
        {
            Destroy(gameObject);
        }

    }
}
