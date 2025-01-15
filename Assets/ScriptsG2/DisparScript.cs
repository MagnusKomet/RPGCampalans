using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparScript : MonoBehaviour
{
    private int timeshooting = 12;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector2(-50, -50);
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = -8;
        this.transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeshooting--;

        if (timeshooting <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
    }
}
