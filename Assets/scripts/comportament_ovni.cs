using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class comportament_ovni : MonoBehaviour
{
    private float timer1 = 0;
    private float timer2 = 0;
    public int temps_en_moviment;
    public float move_speed;
    private bool mogut = false;
    public float shoot_rate;
    public GameObject bala_enemiga;
    private float shoot_timer;
    public AudioSource laser2;
    private Logica logic;
    public int vida_ovni;
    private generador_ovnis generadorOvnis;
    public ParticleSystem explosio;
    public AudioSource explosioSFX;
    public float temps_espera_marxar;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logica>();
        generadorOvnis = FindObjectOfType<generador_ovnis>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temps_en_moviment > timer1 && !mogut)
        {
            transform.position = transform.position + (Vector3.left * move_speed) * Time.deltaTime;
            timer1 = timer1 + Time.deltaTime;
        }
        else
        {
            mogut = true;
        }


        shoot_timer = shoot_timer + Time.deltaTime;

        if (shoot_timer >= shoot_rate)
        {
            shoot();
            shoot_timer = 0;
        }


        if (vida_ovni <= 0)
        {
            eliminar_ovni();
        }

        if (logic.has_perdut)
        {
            if (temps_espera_marxar > timer2)
            {
                timer2 = timer2 + Time.deltaTime;
            }
            else
            {
                transform.position = transform.position + (Vector3.right * move_speed) * Time.deltaTime;
            }
        }

        if (transform.position.x >= 70)
        {
            Destroy(gameObject);
        }

    }

    void eliminar_ovni()
    {
        ParticleSystem instanciaExplosio = Instantiate(explosio, new Vector3(transform.position.x, transform.position.y, 0), explosio.transform.rotation);
        instanciaExplosio.Play();
        GameObject soundObject = new GameObject("So explosió");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = explosioSFX.clip;

        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];

        audioSource.Play();
        Destroy(soundObject, audioSource.clip.length);

        float totalDuration = instanciaExplosio.main.duration + instanciaExplosio.main.startLifetime.constantMax;
        Destroy(instanciaExplosio.gameObject, totalDuration);

        Destroy(gameObject);
        generadorOvnis.ovni_destruit();
    }

    void shoot()
    {
        if (!logic.has_perdut)
        {
            Instantiate(bala_enemiga, new Vector3(transform.position.x - 6, transform.position.y, 0), bala_enemiga.transform.rotation);
            laser2.Play();
        }
    }
}
