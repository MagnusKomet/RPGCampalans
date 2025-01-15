using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    private bool parlar;
    private int parlar_progress = 0;

    [SerializeField]
    private string frase1;
    [SerializeField]
    private string frase2;
    [SerializeField]
    private string frase3;
    [SerializeField]
    private string frase4;
    [SerializeField]
    TextMeshProUGUI TextMeshPro;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    GameObject TextCharacter;
    [SerializeField]
    TMP_FontAsset font;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro.fontSize = 16;
        TextMeshPro.font = font;
        Panel.SetActive(false);
        TextCharacter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && parlar)
        {
            Panel.SetActive(true);
            TextCharacter.SetActive(true);
            switch (parlar_progress)
            {
                case 0:
                    TextMeshPro.enabled = true;
                    TextMeshPro.text = frase1;
                    if (frase2 != "") { parlar_progress++; }
                    else { parlar_progress = 99; }
                    break;
                case 1:
                    TextMeshPro.text = frase2;
                    if (frase3 != "") { parlar_progress++; }
                    else { parlar_progress = 99; }
                    break;
                case 2:
                    TextMeshPro.text = frase3;
                    if (frase4 != "") { parlar_progress++; }
                    else { parlar_progress = 99; }
                    break;
                case 3:
                    TextMeshPro.text = frase4;
                    parlar_progress = 99;
                    break;
                default:
                    parlar_progress = 0;
                    TextMeshPro.enabled = false;
                    Panel.SetActive(false);
                    TextCharacter.SetActive(false);
                    break;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parlar = true;
        parlar_progress = 0;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parlar = false;
        parlar_progress = 0;
        TextMeshPro.enabled = false;
        Panel.SetActive(false);
        TextCharacter.SetActive(false);
    }


}
