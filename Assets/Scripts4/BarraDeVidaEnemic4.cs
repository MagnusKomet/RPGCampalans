using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BarraDeVidaEnemic4 : MonoBehaviour
{
    [SerializeField]
    Image barraDeVida;

    [SerializeField]
    TextMeshProUGUI missatgeVictoria;

    public float vidaActual = 100f;

    public float vidaMaxima = 100f;

    public float vidaMinima = 0f;

    public string nextScene;
    void Update()
    {
        barraDeVida.fillAmount = vidaActual / vidaMaxima;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) 
        {
            vidaActual -= vidaMaxima * 0.1f;

            if (vidaActual <= vidaMinima)
            {
                Destroy(barraDeVida);
                Destroy(gameObject);
                MostrarMissatgeVictoria();
                SceneManager.LoadScene(nextScene);
            }

            Destroy(collision.gameObject);
        }
        
    }
    private void MostrarMissatgeVictoria()
    {
        if (missatgeVictoria != null)
        {
            missatgeVictoria.gameObject.SetActive(true);
        }
    }
}
