using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BarraDeVidaPlayer4 : MonoBehaviour
{
    [SerializeField]
    Image barraDeVidaPlyer;

    [SerializeField]
    TextMeshProUGUI missatgeDerrota;

    [SerializeField]
    GameObject escut;

    public string mateixaScene;

    public float vidaActualPlayer = 100f;

    public float vidaMaximaPlayer = 100f;

    public float vidaMinimaPlayer = 0f;

    public bool protegit = false;
    void Update()
    {
        barraDeVidaPlyer.fillAmount = vidaActualPlayer / vidaMaximaPlayer;

        if (Input.GetKey(KeyCode.Q))
        {
            protegit = true;
            escut.SetActive(true);
        }
        else
        {
            protegit= false;
            escut.SetActive(false);
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("PlayerAttack") && !protegit) 
        {

            vidaActualPlayer -= vidaMaximaPlayer * 0.1f;

            if (vidaActualPlayer <= vidaMinimaPlayer)
            {
                Destroy(barraDeVidaPlyer);
                Destroy(gameObject);
                MostrarMissatgeDerrota();
                SceneManager.LoadScene(mateixaScene);
            }
        }
        
    }
    private void MostrarMissatgeDerrota()
    {
        if (missatgeDerrota != null)
        {
            missatgeDerrota.gameObject.SetActive(true);
        }
    }
}
