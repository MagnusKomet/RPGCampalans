using System;
using System.Collections;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToroMoviment : MonoBehaviour
{
    private const string KEY_PLAYER_OBJECT_NAME = "Player";
    private const string KEY_OBSTACLE_MORTAL = "ObstacleMortal";
    private const string KEY_PARET = "paret";

    private const string ANIMATION_IDLE = "animation_Idle";
    private const string ANIMATION_ATTACK = "animation_Attack";
    private const string ANIMATION_VULNERABLE = "animation_Vulnerable";

    private const bool START_WAITING_TIMER = true;

    private const int HP_START_MODE_SEGONA_FASE = 2;
    private const int HP_MAX_DEFAULT = 5;
    private const float SPEED_DEFAULT = 10f;
    private const float MODE_VULNERABLE_DURACIO = 5f;

    private bool MODE_SEGONA_FASE = false;
    private bool MODE_VULNERABLE = false;

    [SerializeField]
    RuntimeAnimatorController spriteCorrentEnfadat;

    [SerializeField]
    RuntimeAnimatorController spriteIdleEnfadat;

    [SerializeField]
    RuntimeAnimatorController spriteCorrent;

    [SerializeField]
    RuntimeAnimatorController spriteIdle;

    [SerializeField]
    RuntimeAnimatorController spriteVulnerable;

    /*[SerializeField]
    GameObject objecteAnimacioRebreDany;*/

    [SerializeField]
    GameObject stanceEnfadat;

    [SerializeField]
    GameObject stanceDerrotat;

    private SpriteRenderer spriteRenderer;
    private GameObject Player;
    private Animator animator;
    private Rigidbody2D rg2d;
    private bool stopped;
    private bool atacant;
    private bool obstacleTocat;
    private int hp;
    private float speed;
    private float attack_speed = 5f;
    private bool victoria = false;
    private AudioSource soundRock;
    private bool stopGame = false;
    public GameObject areaExit;

    // Start is called before the first frame update
    void Start()
    {
        soundRock = GameObject.Find("soundToroTrencarRoca").GetComponent<AudioSource>();
        hp = HP_MAX_DEFAULT;
        speed = SPEED_DEFAULT;
        stanceEnfadat.SetActive(false);
        stanceDerrotat.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        obstacleTocat = false;
        atacant = false;
        stopped = false;
        Player = GameObject.Find(KEY_PLAYER_OBJECT_NAME);

        StartAttack();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player = GameObject.Find(KEY_PLAYER_OBJECT_NAME);
        rg2d = GetComponent<Rigidbody2D>();

        if (Player != null)
        {
            if (!victoria)
            {
                victoria = hp <= 0;
                ComprovarEntrarModeEnfadat();

                // Actualitzar animació i moviment del toro

                PosToro.Inicial = transform.position;

                if (atacant && Vector2.Distance(PosToro.Objectiu, PosToro.Inicial) < 1)
                {
                    atacant = false;
                    stopped = true;
                    StartAnimation(null);
                    Debug.LogWarning("dst < 1");
                }

                if (stopped && Vector2.Distance(PosToro.Objectiu, PosToro.Inicial) > 3)
                {
                    stopped = false;
                    StartAnimation(ANIMATION_IDLE);
                    Debug.LogError("dst > 3");
                    rg2d.velocity = new Vector2(0f, 0f);
                    StartAttack();
                }

                AlternarOrientacioHoritzontalToro();

                if (obstacleTocat)
                {
                    StopAttack();
                }
            }
            else
            {
                if (!stopGame)
                {
                    stopGame = true;
                    StartAnimation(ANIMATION_VULNERABLE);
                    stanceDerrotat.SetActive(true);
                    StartCoroutine(fadeInAndOut(stanceDerrotat, true, 1));
                }

                areaExit.transform.position = new Vector3(8.5f, -9, 0);
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colTag = collision.gameObject.tag;

        if (colTag.Equals(KEY_OBSTACLE_MORTAL))
        {
            soundRock.Play();
            obstacleTocat = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colName = collision.gameObject.name;
        string colTag = collision.gameObject.tag;

        if (colName.Equals(KEY_PLAYER_OBJECT_NAME))
        {
            if (!victoria)
            {
                if (MODE_VULNERABLE)
                {
                    ToroHit();

                }
                else
                {
                    Destroy(this.gameObject);
                    SceneManager.LoadScene("ToroCamiScene");
                }
            }
        }
        else if (colTag.Equals(KEY_PARET) && !obstacleTocat)
        {
            Debug.LogWarning("tocat, stop src: Paret");
            stopped = true;
            StartAnimation(ANIMATION_IDLE);
            rg2d.velocity = new Vector2(0f, 0f);
            StartAttack();
        }
    }

    void Attack()
    {
        if (!Player.IsUnityNull())
        {
            StartAnimation(ANIMATION_ATTACK);

            obstacleTocat = false;
            MODE_VULNERABLE = false;
            stopped = false;
            atacant = true;

            PosToro.Inicial = transform.position;
            PosToro.Objectiu = Player.transform.position;

            float x = (PosToro.Objectiu.x - PosToro.Inicial.x);
            float y = (PosToro.Objectiu.y - PosToro.Inicial.y);
            float z = (float)Math.Sqrt((x * x) + (y * y));

            Vector2 direccio = new Vector2(x / z, y / z);

            rg2d.velocity = direccio * speed;
        }
    }

    void AlternarOrientacioHoritzontalToro()
    {
        // Actualitzar cap on mira el toro
        if (transform.position.x < PosToro.Objectiu.x)
        {
            this.transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        }
        else
        {
            this.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
        }
    }

    void StopAttack()
    {
        obstacleTocat = false;
        MODE_VULNERABLE = true;
        stopped = true;
        rg2d.velocity = new Vector2(0f, 0f);
        Debug.LogWarning("tocat, stop src: Obstacle");
        StartAnimation(ANIMATION_VULNERABLE);
        StartAttack();
    }

    void ComprovarEntrarModeEnfadat()
    {
        if (hp <= HP_START_MODE_SEGONA_FASE && !MODE_SEGONA_FASE)
        {
            MODE_SEGONA_FASE = true;
            attack_speed = 2f;
            StartAnimation(ANIMATION_IDLE);
            StartAnimacioStanceEnfadat();
        }
    }

    private void StartAnimation(string animacio)
    {
        switch (animacio)
        {
            case ANIMATION_IDLE:
                animator.runtimeAnimatorController = MODE_SEGONA_FASE ? spriteIdleEnfadat : spriteIdle;
                break;
            case ANIMATION_ATTACK:
                animator.runtimeAnimatorController = MODE_SEGONA_FASE ? spriteCorrentEnfadat : spriteCorrent;
                break;
            case ANIMATION_VULNERABLE:
                animator.runtimeAnimatorController = spriteVulnerable;
                break;
            default:
                animator.runtimeAnimatorController = null;
                break;
        }
    }


    void ToroHit()
    {
        spriteRenderer.color = Color.red;
        MODE_VULNERABLE = false;
        hp--;
        Debug.Log("HIT: start");
        StartCoroutine(Hit());
        StartAnimation(ANIMATION_IDLE);
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
        Debug.Log("HIT: end");
    }

    void StartAttack()
    {
        StartCoroutine(StartAttackTimer());
    }

    IEnumerator StartAttackTimer()
    {
        if (MODE_VULNERABLE)
        {
            Debug.LogWarning("STARTED ATTACK TIMER: Vulnerable (now) " + MODE_VULNERABLE_DURACIO + "s");
            yield return new WaitForSeconds(MODE_VULNERABLE_DURACIO);
            MODE_VULNERABLE = false;
            if (!victoria)
            {
                Attack();
            }
        }
        else
        {
            Debug.LogWarning("STARTED ATTACK TIMER " + attack_speed + "s");
            yield return new WaitForSeconds(attack_speed);
            if (!victoria)
            {
                Attack();
            }
        }
       
        
        
    }

    void StartAnimacioStanceEnfadat()
    {
        stanceEnfadat.SetActive(true);
        StartCoroutine(fadeInAndOut(stanceEnfadat, true, 1));
        StartCoroutine(fadeInAndOut(stanceEnfadat, false, 1));
    }

    IEnumerator fadeInAndOut(GameObject objectToFade, bool fadeIn, float duration)
    {
        if (!fadeIn)
        {
            yield return new WaitForSeconds(2);
        }
        float counter = 0f;

        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }
        Color currentColor = Color.clear;

        SpriteRenderer tempSPRenderer = objectToFade.GetComponent<SpriteRenderer>();

        if (tempSPRenderer != null)
        {
            currentColor = tempSPRenderer.color;
        }

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            tempSPRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
        if (!fadeIn)
        {
            stanceEnfadat.SetActive(false);
        }
    }

}
