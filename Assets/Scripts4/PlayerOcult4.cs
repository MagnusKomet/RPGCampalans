using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOcult4 : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.position = new Vector3(1000,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
