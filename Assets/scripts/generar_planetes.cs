using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generar_planetes : MonoBehaviour
{
    public GameObject[] planetes;
    public float spawn_rate;
    private float timer = 0;
    private int index_anterior;

    // Start is called before the first frame update
    void Start()
    {
        Generar_planeta();
        index_anterior = -1;
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
            Generar_planeta();
            timer = 0;
        }
    }

    void Generar_planeta()
    {
        int index = Random.Range(0, planetes.Length);

        while (index == index_anterior)
        {
            index = Random.Range(0, planetes.Length);
        }
        index_anterior = index;
        Instantiate(planetes[index], new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
