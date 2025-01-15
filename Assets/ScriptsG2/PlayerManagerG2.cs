using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerG2 : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.transform.position = new Vector3(25,-10,0);
    }
}
