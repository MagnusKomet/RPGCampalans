using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generar_estrelles : MonoBehaviour
{
    public GameObject[] estrelles;
    public float spawn_rate;
    private float timer = 0;
    public float diferencia_altura;

    // Start is called before the first frame update
    void Start()
    {
        Generar_estrelles();
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
            Generar_estrelles();
            timer = 0;
        }
    }

    void Generar_estrelles()
    {
        float altura_minima = transform.position.y - diferencia_altura;
        float altura_maxima = transform.position.y + diferencia_altura;
        int index = Random.Range(0, estrelles.Length);

        Instantiate(estrelles[index], new Vector3(transform.position.x, Random.Range(altura_minima, altura_maxima), 0), transform.rotation);

    }
}
