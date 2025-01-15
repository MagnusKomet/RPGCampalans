using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEnemyScript : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float speed = 10;

    [SerializeField]
    float tempsMovimentHorizontal = 2f;

    Transform alturaJugador;

    int estat = 0;

    bool jugadorApuntat = false;

    [SerializeField]
    GameObject laser;

    [SerializeField]
    AudioSource laser_audio;

    [SerializeField]
    AudioSource carrega_laser_audio;

    [SerializeField]
    ParticleSystem animacioCarregaLaser;

    ParticleSystem instanciaAnimacioCarrega;

    float estatTimer = 0f;

    bool hasMovedLeft = false;

    bool accioExecutada = false;

    private Logica logic;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logica>();

        if (alturaJugador == null)
        {
            GameObject jugador = GameObject.FindWithTag("jugador");
            if (jugador != null)
            {
                alturaJugador = jugador.transform;
            }
        }
    }

    void Update()
    {
        estatTimer += Time.deltaTime;
        canviarEstat();

        if (logic.has_perdut)
        {
            Invoke("moureDreta", 2f);
        }
        
    }

    void canviarEstat()
    {
        switch (estat)
        {
            case 0: // Movimiento horizontal
                if (!hasMovedLeft)
                {
                    moureHoritzontal();
                    hasMovedLeft = true;
                }

                if (estatTimer >= tempsMovimentHorizontal)
                {
                    estat = 1;
                    estatTimer = 0f;
                }
                break;

            case 1: // Apuntando al jugador
                apuntarJugador();

                if (jugadorApuntat)
                {
                    estat = 2;
                    estatTimer = 0f;
                    accioExecutada = false;
                }
                break;

            case 2: // Cargando el láser
                if (!accioExecutada)
                {
                    carregarLaser();
                    accioExecutada = true;
                }

                if (estatTimer >= 1.5f) // Duración de la animación de carga
                {
                    estat = 3;
                    estatTimer = 0f;
                    accioExecutada = false;
                }
                break;

            case 3: // Disparando el láser
                if (!accioExecutada)
                {
                    dispararLaser();
                    accioExecutada = true;
                }

                if (estatTimer >= 1f)
                {
                    estat = 4;
                    estatTimer = 0f;
                    accioExecutada = false;
                }
                break;

            case 4: // Espera
                aturar();

                if (estatTimer >= 1f)
                {
                    estat = 1;
                    jugadorApuntat = false;
                    estatTimer = 0f;
                }
                break;
        }
    }

    void moureHoritzontal()
    {
        rb.velocity = Vector2.left * speed;
    }

    void apuntarJugador()
    {
        if (alturaJugador != null)
        {

            float distancia = alturaJugador.position.y - transform.position.y;

            if (Mathf.Abs(distancia) > 0.2f)
            {
                if (distancia > 0)
                {
                    rb.velocity = Vector2.up * speed;
                }
                else
                {
                    rb.velocity = Vector2.down * speed;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                jugadorApuntat = true;
            }
        }
    }

    void aturar()
    {
        rb.velocity = Vector2.zero;
    }

    void carregarLaser()
    {
        if (alturaJugador != null)
        {
            Vector2 posicio = new Vector2(transform.position.x - 7, transform.position.y);

            instanciaAnimacioCarrega = Instantiate(animacioCarregaLaser, posicio, animacioCarregaLaser.transform.rotation);
            carrega_laser_audio?.Play();
            Destroy(instanciaAnimacioCarrega.gameObject, instanciaAnimacioCarrega.main.duration);
        }
    }

    void dispararLaser()
    {
        Vector2 posicio = new Vector2(transform.position.x - 1, transform.position.y);
        GameObject instanciaLaser = Instantiate(laser, posicio, Quaternion.identity);
        laser_audio?.Play();
        Destroy(instanciaLaser, 1.5f);
    }

    void moureDreta()
    {
        speed = 10;
        rb.velocity = Vector2.right * speed;

        if (transform.position.x > 60)
        {
            Destroy(gameObject);
        }
    }
}
