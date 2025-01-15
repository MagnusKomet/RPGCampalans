using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
    // Si no esxiteix el jugador, el creem
	void Start () {
        if (PlayerController.instance == null){
            Debug.Log("PlayerLoader:: No Player, instantiating...");
            Instantiate(player);
        }	
	}
}
