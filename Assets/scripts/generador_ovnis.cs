using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador_ovnis : MonoBehaviour
{
    public GameObject ovni;
    public float diferencia_altura;
    private float timer = 0;
    public float spawn_rate;
    public bool hi_han_max_ovnis = false;
    private int qtat_ovnis = 0;
    public int max_ovnis;
    private Logica logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logica>();
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
            generar_ovni();
            timer = 0;
        }

    }

    void generar_ovni()
    {
        if (!hi_han_max_ovnis && !logic.has_perdut)
        {
            float altura_minima = transform.position.y - diferencia_altura;
            float altura_maxima = transform.position.y + diferencia_altura;

            Instantiate(ovni, new Vector3(transform.position.x, Random.Range(altura_minima, altura_maxima), 0), transform.rotation);

            qtat_ovnis++;

            if (qtat_ovnis == max_ovnis)
            {
                hi_han_max_ovnis = true;
            }
        }
    }

    public void ovni_destruit()
    {
        qtat_ovnis--;

        if (qtat_ovnis < max_ovnis)
        {
            hi_han_max_ovnis = false;
        }

        logic.SumarPuntuacio(5);
    }
}
