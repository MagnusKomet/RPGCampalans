using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador_enemicsLaser : MonoBehaviour
{
    [SerializeField]
    GameObject enemicLaser;

    [SerializeField]
    float spawnRate;

    [SerializeField]
    float altura_max;

    [SerializeField]
    float altura_min;

    float time;

    float alturaRandom;

    [SerializeField]
    int enemicsMax = 1;

    int enemicsActuals = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (enemicsActuals < enemicsMax)
        {
            if (time >= spawnRate)
            {
                alturaRandom = Random.Range(altura_min, altura_max);

                Vector2 posicio = new Vector2(transform.position.x, alturaRandom);
                Instantiate(enemicLaser, posicio, Quaternion.identity);
                time = 0;
                enemicsActuals++;
            }
        }
    }
}
