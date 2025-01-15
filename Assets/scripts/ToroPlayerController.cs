using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToroPlayerController : MonoBehaviour
{
    // TORO
    private bool blue_orb = false;

    private GameObject blueAltarImg;

    public Sprite EmptyAltar;

    private GameObject ironFences;

    private GameObject estatua;

    public Sprite[] sprites;

    //public Sprite activated_spike;

    public float changeTime = 1.5f;

    private int index = 0;

    AudioSource orbtaken;
    AudioSource statueactvated;


    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {
            

            estatua = GameObject.Find("Estatua");
            ironFences = GameObject.Find("IronFences");
            blueAltarImg = null;

            if (blueAltarImg == null)
            {
                blueAltarImg = GameObject.Find("BlueOrb");
            }

            Invoke("Invoke", 0.1f);

            statueactvated = GameObject.Find("StatueActive").GetComponent<AudioSource>();

            orbtaken = GameObject.Find("TakeOrb").GetComponent<AudioSource>();
            estatua.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //que no destrueixi el jugador
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {
            estatua.SetActive(false);
            blueAltarImg = null;

            if (blueAltarImg == null)
            {
                blueAltarImg = GameObject.Find("BlueOrb");
                blueAltarImg.SetActive(false);
                //Debug.Log("BLUE ALTAR IMG TAKEN");
            }

            InvokeRepeating("CanviarSprites", 0f, changeTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {
            if (collision.gameObject.name == "DialogArea")
            {
                Debug.LogWarning("POL: Triggered dialog area");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {
            if (collision.gameObject.name == "BlueAltar")
            {
                blue_orb = true;
                blueAltarImg.SetActive(true);
                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = EmptyAltar;

                orbtaken.Play();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {

            if (collision.gameObject.name == "EstatuaCollider")
            {
                if (Input.GetKeyDown(KeyCode.E) && blue_orb)
                {
                    estatua.SetActive(true);
                    ironFences.SetActive(false);

                    statueactvated.Play();
                }
            }

            SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();

            if ((spriteRenderer != null && spriteRenderer.sprite == sprites[0]) &&
                collision.gameObject.tag == "spike")
            {
                gameObject.transform.position = new Vector2(-4.5f, -5.5f);
                //Debug.Log("SPIKE");
            }
        }
    }

    void Invoke()
    {
        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {
            if (blue_orb)
            {
                blueAltarImg.SetActive(true);
                if (SceneManager.GetActiveScene().name == "ToroCamiScene2")
                {
                    SpriteRenderer spriteRenderer = GameObject.Find("BlueAltar").gameObject.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = EmptyAltar;
                }

                //Debug.Log("BLUE ORB TRUE");
            }
            else
            {
                blueAltarImg.SetActive(false);
                //Debug.Log("BLUE ORB FALSE");
            }
        }
    }

    void CanviarSprites()
    {
        if (SceneManager.GetActiveScene().name == "ToroCamiScene" ||
            SceneManager.GetActiveScene().name == "ToroCamiScene2")
        {
            // Obtenir objectes amb tag spike
            GameObject[] spikes = GameObject.FindGameObjectsWithTag("spike");
            // Canviar el sprite de cada objecte spike
            foreach (GameObject spike in spikes)
            {
                SpriteRenderer spriteRenderer = spike.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // Canviar sprite
                    spriteRenderer.sprite = sprites[index];

                    // Incrementar index i reiniciar-lo
                    index = (index + 1) % sprites.Length;
                }
            }
        }
            
    }

    
}
