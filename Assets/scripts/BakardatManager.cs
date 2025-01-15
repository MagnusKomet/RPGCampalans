using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakardatManager : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().enabled = false;        
    }

}
