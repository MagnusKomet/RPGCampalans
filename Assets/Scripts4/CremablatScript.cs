using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CremablatScript : MonoBehaviour
{
    // Variables de movimiento
    float speed = 3;
    [SerializeField]
    Rigidbody2D rb;
    float timer = 0;
    float tempsMoviment;
    float tempsAturat = 2;
    int estat = 0; // 0: Derecha, 1: Pausa, 2: Atacando, 3: Izquierda

    // Variables de ataque
    bool atacantElectric = false;
    bool atacantFoc = false;
    bool attackactivat = false;
    [SerializeField]
    float vida = 13;

    [SerializeField]
    ParticleSystem animacioAtacFoc;

    [SerializeField]
    ParticleSystem animacioAtacElectric;

    [SerializeField]
    ParticleSystem neu;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject colliderFoc;

    [SerializeField]
    GameObject colliderEspurnes;

    [SerializeField]
    GameObject colliderNeu;

    public Animator animator;

    float tempsattack = 60f; // Tiempo más largo para ataques
    float tempsattack2 = 30f; // Tiempo de activación secundaria
    bool atacRealitzat = false;
    double auxrota;

    //Vector2 diferencia;
    float distanciaMovimiento = 3f; // Distancia fija para moverse
    

    void Start()
    {
        tempsMoviment = UnityEngine.Random.Range(1f, 3f);
        colliderFoc.SetActive(false);
        colliderEspurnes.SetActive(false);
        colliderNeu.SetActive(true);
        neu.Play();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (atacantFoc)
        {
            colliderFoc.SetActive(true);
        }
        else
        {
            colliderFoc.SetActive(false);
        }

        if (!atacantElectric)
        {
            colliderEspurnes.SetActive(false);
        }

        switch (estat)
        {
            case 0:
                MouretAdelante();
                if (timer >= tempsMoviment)
                {
                    estat = 1;
                    timer = 0;
                }
                break;

            case 1:
                Aturar();
                if (timer >= tempsAturat)
                {
                    estat = 2;
                    timer = 0;
                }
                break;

            case 2:
                if (!atacRealitzat)
                {
                    int atac = UnityEngine.Random.Range(0, 2);
                    if (atac == 0) AtacElectric();
                    else AtacFoc();
                    atacRealitzat = true;
                }
                if (!atacantElectric && !atacantFoc)
                {
                    estat = 3;
                    timer = 0;
                    atacRealitzat = false;
                }
                break;

            case 3:
                MouretAtras();
                if (timer >= tempsMoviment)
                {
                    estat = 4;
                    timer = 0;
                }
                break;

            case 4:
                Aturar();
                if (timer >= tempsAturat)
                {
                    estat = 5;
                    timer = 0;
                }
                break;

            case 5:
                if (!atacRealitzat)
                {
                    int atac = UnityEngine.Random.Range(0, 2);
                    if (atac == 0) AtacElectric();
                    else AtacFoc();
                    atacRealitzat = true;
                }
                if (!atacantElectric && !atacantFoc)
                {
                    estat = 0;
                    timer = 0;
                    atacRealitzat = false;
                }
                break;
        }
    }

    void MouretAdelante()
    {
        speed = UnityEngine.Random.Range(1, 3);
        rb.velocity = Vector2.right * speed;
        tempsMoviment = distanciaMovimiento / speed;
    }

    void MouretAtras()
    {
        speed = UnityEngine.Random.Range(1, 3);
        rb.velocity = Vector2.left * speed;
        tempsMoviment = distanciaMovimiento / speed;
    }

    void AtacElectric()
    {
        atacantElectric = true;
        animacioAtacElectric.Play();


        StartCoroutine(DispararEspurnesElectricas());
    }

    void AtacFoc()
    {
        atacantFoc = true;
        animacioAtacFoc.Play();
        StartCoroutine(EsperarXSegonsFoc(4f));
    }

    void Aturar()
    {
        rb.velocity = Vector2.zero;
        speed = 0;
    }

    IEnumerator DispararEspurnesElectricas()
    {
        for (int i = 0; i < 3; i++) // Dispara 5 veces
        {
            colliderEspurnes.SetActive(true);
            yield return new WaitForSeconds(4f);
            colliderEspurnes.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        atacantElectric = false;
    }

    IEnumerator EsperarXSegonsFoc(float temps)
    {
        yield return new WaitForSeconds(temps);
        atacantFoc = false;
    }
}
