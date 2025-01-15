using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generar_meteorits : MonoBehaviour
{
    public GameObject[] meteorits;
    public float spawn_rate = 2;
    private float timer = 0;
    public float diferencia_altura;
    public float velocitat_inicial = 10;

    [SerializeField]
    float augment_velocitat = 0.01f;

    public float velocitat_max;
    private Logica logic;
    public float escala_minima;
    public float escala_maxima;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawn_rate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            if (velocitat_inicial < velocitat_max)
            {
                velocitat_inicial += augment_velocitat;

                if (velocitat_inicial > velocitat_max)
                {
                    velocitat_inicial = velocitat_max;
                }
            }

            generar_meteorit();
            timer = 0;
        }
    }

    public void generar_meteorit()
    {
        float altura_minima = transform.position.y - diferencia_altura;
        float altura_maxima = transform.position.y + diferencia_altura;

        float posicio_y = Random.Range(altura_minima, altura_maxima);
        float escala = Random.Range(escala_minima, escala_maxima);

        int index = Random.Range(0, meteorits.Length);

        Vector3 escalaAleatoria = new Vector3(escala, escala, escala);

        Instantiate(meteorits[index], new Vector3(transform.position.x, posicio_y, transform.position.z), Quaternion.identity).transform.localScale = escalaAleatoria;
    }



}
