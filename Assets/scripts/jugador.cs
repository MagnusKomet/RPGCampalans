using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class jugador : MonoBehaviour
{
    private Rigidbody2D rb;
    public Logica logic;
    public float limit_altura;
    public GameObject bala_jugador;
    public GameObject bala_enemiga;
    public float nau_power = 4.314969f;
    private AudioSource laser1;
    public Transform bala_pos1;
    private float timer;
    public ParticleSystem explosio;
    public AudioSource so_explosio;
    public ParticleSystem particules;
    public AudioSource so_jet;
    public AudioMixer audioMixer;

    [SerializeField]
    Image Ammo1;
    [SerializeField]
    Image Ammo2;
    [SerializeField]
    Image Ammo3;
    [SerializeField]
    Image Ammo4;
    [SerializeField]
    Image Ammo5;

    private int actual_ammo;
    private int max_ammo = 5;
    private bool ammo_is_full;
    private bool ammo_is_empty;

    [SerializeField]
    float ammo_recharge_time = 1f;

    float temps = 0f;
    void Start()
    {
        laser1 = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logica>();
        actual_ammo = 5;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !logic.has_perdut && !logic.isPaused)
        {
            rb.gravityScale = -nau_power;
            particules.Play();
            so_jet.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = nau_power;
            particules.Stop();
            so_jet.Stop();
        }

        if (transform.position.y < -limit_altura || transform.position.y > limit_altura)
        {
            logic.GameOver();
        }

        if (transform.position.y < -limit_altura)
        {
            Destroy(gameObject);
        }

        if (actual_ammo == max_ammo)
        {
            ammo_is_full = true;
        }
        else if (actual_ammo == 0)
        {
            ammo_is_empty = true;
        }
        else if (actual_ammo > 0 && actual_ammo < 5)
        {
            ammo_is_full = false;
            ammo_is_empty = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!ammo_is_empty)
            {
                shoot();
                restar_municio();
            }
        }

        if (!ammo_is_full)
        {
            temps += Time.deltaTime;

            if (temps >= ammo_recharge_time)
            {
                augmentar_municio();
                temps = 0;
            }
        }

        switch (actual_ammo)
        {
            case 5:
                Ammo5.enabled = true;
                Ammo4.enabled = true;
                Ammo3.enabled = true;
                Ammo2.enabled = true;
                Ammo1.enabled = true;
                break;

            case 4:
                Ammo5.enabled = false;
                Ammo4.enabled = true;
                Ammo3.enabled = true;
                Ammo2.enabled = true;
                Ammo1.enabled = true;
                break;
            case 3:
                Ammo5.enabled = false;
                Ammo4.enabled = false;
                Ammo3.enabled = true;
                Ammo2.enabled = true;
                Ammo1.enabled = true;
                break;
            case 2:
                Ammo5.enabled = false;
                Ammo4.enabled = false;
                Ammo3.enabled = false;
                Ammo2.enabled = true;
                Ammo1.enabled = true;
                break;
            case 1:
                Ammo5.enabled = false;
                Ammo4.enabled = false;
                Ammo3.enabled = false;
                Ammo2.enabled = false;
                Ammo1.enabled = true;
                break;
            case 0:
                Ammo5.enabled = false;
                Ammo4.enabled = false;
                Ammo3.enabled = false;
                Ammo2.enabled = false;
                Ammo1.enabled = false;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("meteorit") || collision.gameObject.tag == ("bala_enemiga") || collision.gameObject.tag == ("laser"))
        {
            //reproduir animació explosió
            ParticleSystem instanciaExplosio = Instantiate(explosio, transform.position, explosio.transform.rotation);
            instanciaExplosio.Play();

            //reproduir so
            GameObject soundObject = new GameObject("So explosió");
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = so_explosio.clip;

            // Asignar el AudioSource al grupo de audio Master del AudioMixer
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];

            audioSource.Play();
            Destroy(soundObject, audioSource.clip.length);

            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);

            logic.GameOver();
        }
    }

    public void shoot()
    {
        Instantiate(bala_jugador, bala_pos1.position, transform.rotation);
        laser1.Play();
    }

    private void restar_municio()
    {
        if (!ammo_is_empty)
        {
            actual_ammo--;
        }
    }

    private void augmentar_municio()
    {
        if (!ammo_is_full && logic.has_perdut == false)
        {
            actual_ammo++;
        }
    }


}
