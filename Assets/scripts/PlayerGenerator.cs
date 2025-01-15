using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{

    public GameObject player;
    public GameObject muro;
    public GameObject selector;
    Animator animator;

    // Use this for initialization
    // Si no esxiteix el jugador, el creem
    void Start()
    {

        animator = player.gameObject.GetComponent<Animator>();
        player.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Seleccio1()
    {
        animator.runtimeAnimatorController = Resources.Load("Player/Player") as RuntimeAnimatorController;
        AcabadaSeleccio();
    }

    public void Seleccio2()
    {
        animator.runtimeAnimatorController = Resources.Load("Player2/Player2") as RuntimeAnimatorController;
        AcabadaSeleccio();
    }

    public void Seleccio3()
    {
        animator.runtimeAnimatorController = Resources.Load("Player3/Player3") as RuntimeAnimatorController;
        AcabadaSeleccio();
    }

    private void AcabadaSeleccio()
    {
        muro.SetActive(false);
        selector.SetActive(false);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }
}
