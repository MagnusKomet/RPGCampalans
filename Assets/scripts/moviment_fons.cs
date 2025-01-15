using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moviment_fons : MonoBehaviour
{
    public float move_speed;
    public float zona_morta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * move_speed) * Time.deltaTime;

        if (transform.position.x < zona_morta)
        {
            Destroy(gameObject);
        }
    }
}
