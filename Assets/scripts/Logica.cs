using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class Logica : MonoBehaviour
{
    [SerializeField]
    private int score;
    public TextMeshProUGUI score_text;
    public GameObject pantalla_gameover;
    public bool has_perdut = false;
    public Logica logic;
    public float fadeDuration;
    public Button boto;
    public Image imatge;
    public List<TextMeshProUGUI> texts;
    private float timer;
    private float score_timer;
    public GameObject panell_settings;
    private int high_score = 0;
    public TextMeshProUGUI high_score_text;
    public float temps_per_sumar_puntuacio;
    public GameObject lletres_pausa;
    public bool isPaused = false;
    public AudioSource musica_fons;
    private bool hasGuanyat = false;
    public TextMeshProUGUI youWonTxt;
    public Button continuarBtn;


    void Start()
    {
        //if (high_score_text != null)
        //{
        //    high_score = PlayerPrefs.GetInt("high_score", 0);
        //    high_score_text.text = "High Score: " + high_score.ToString();
        //}
    }
    void Update()
    {
        if (score >= 50)
        {
            hasGuanyat = true;
            score_text.color = Color.green;

        }

        if (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
       
            Color buttonColor = boto.image.color;
            buttonColor.a = alpha;
            boto.image.color = buttonColor;
       
            Color imageColor = imatge.color;
            imageColor.a = alpha;
            imatge.color = imageColor;

            foreach (TextMeshProUGUI text in texts)
            {
                Color textColor = text.color;
                textColor.a = alpha;
                text.color = textColor;
            }

            timer += Time.deltaTime;
        }

        if (score_timer >= temps_per_sumar_puntuacio && !has_perdut)
        {
            SumarPuntuacio(1);
            score_timer = 0;
        }
        else
        {
            score_timer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !has_perdut)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (has_perdut)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                reiniciar_joc();
            }
        }
    }

    [ContextMenu("Sumar punt")]
    public void SumarPuntuacio(int puntuacio_a_sumar)
    {
        if (score_text != null)
        {
            score = score + puntuacio_a_sumar;
            score_text.text = score.ToString();
            
        }
    }

    public void reiniciar_joc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        has_perdut = true;

        if (hasGuanyat)
        {
            youWonTxt.gameObject.SetActive(true);
            continuarBtn.gameObject.SetActive(true);

        }
        else
        {
            pantalla_gameover.SetActive(true);
        }
        

        //if (score > high_score)
        //{
        //    high_score = score;
        //    PlayerPrefs.SetInt("high_score", score);
        //    PlayerPrefs.Save();
        //    high_score_text.text = "High Score: " + high_score.ToString();
        //}
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        panell_settings.SetActive(false);
    }

    public void TornarAlMenu()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void MostrarSettings()
    {
        panell_settings.SetActive(true);
    }

    public void PasarNivell()
    {
        SceneManager.LoadScene("RPG_4");
    }

    void PauseGame()
    {
        if (lletres_pausa != null)
        {
            lletres_pausa.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            musica_fons.Pause();
        }
    }

    void ResumeGame()
    {
        if (lletres_pausa != null)
        {
            lletres_pausa.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            musica_fons.UnPause();
        }
    }
}
