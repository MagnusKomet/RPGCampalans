using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Vector3 initialScale;
    [SerializeField]
    float laser_speed;
    float time = 0;

    [SerializeField]
    float temps_vida = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentScale = transform.localScale;
        currentScale.x += Time.deltaTime * laser_speed;

        transform.localScale = currentScale;

        time += Time.deltaTime;

        if(time >= temps_vida)
        {
            Destroy(gameObject);
        }
    }
}
