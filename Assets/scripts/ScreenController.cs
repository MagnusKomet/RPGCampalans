using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Callback OnClick del botó
    public void OnBackButtonClick() {
        //Debug.Log("PlayerController: " + PlayerController.instance.name);
        string lastArea = PlayerController.instance.GetLastArea();
        Debug.Log("ScreenController:: Click: Going to" + lastArea);

        SceneManager.LoadScene(lastArea);

    }
}
