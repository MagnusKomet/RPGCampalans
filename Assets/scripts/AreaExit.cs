using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

    public string areaToLoad;
    public string areaJointPoint;

    //el punt que servirà de sortida
    // si el tornessim al mateix punt on estava
    // tornaria col·lisionar i faríem un bucle infinit...
    public AreaEntrance entranceAssociatedToExit;

    // Use this for initialization
    void Start () {
        //li diem que el punt d'unió d'àrees de l'Entrance és el mateix que té
        // Exit
        entranceAssociatedToExit.areaJointPoint = areaJointPoint;
	}
	

    //Quan col·lisiona amb algun altre collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si és el jugador
        if (other.tag == "Player") {
            //guardem al jugador el punt on el voldrem posar
            Debug.Log("Area Exit:: Carreguem nova area: " + areaToLoad + "; guardem area actual:" + SceneManager.GetActiveScene().name);
            PlayerController.instance.areaJointPoint = areaJointPoint;
            PlayerController.instance.SetLastArea(SceneManager.GetActiveScene().name);

            //carreguem l'escena
            SceneManager.LoadScene(areaToLoad);
        }
    }
}
