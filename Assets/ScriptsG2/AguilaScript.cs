using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AguilaScript : MonoBehaviour
{
    // Start is called before the first frame update
    System.Random rand;
    SpriteRenderer render;
    Rigidbody2D rb2d;
    float timetodie = 300;
    bool downed = false;
    static bool dispara_al_guanyar = false;

    private static int puntuacio = 0;

    [SerializeField]
    Text text;

    [SerializeField]
    TextMeshProUGUI textguanyar;

    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<Text>();
        textguanyar = FindObjectOfType<TextMeshProUGUI>(true);
        rand = new System.Random();
        render = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        float alçada = Random.Range(-4, 3);

        bool esquerra = rand.Next(2) == 1;

        float speed = rand.Next(7, 10);

        if (esquerra)
        {
            render.flipX = true;
            transform.position = new Vector2(-10, alçada);
            rb2d.velocity = Vector2.right * speed;

        }
        else
        {
            render.flipX = false;
            transform.position = new Vector2(10, alçada);
            rb2d.velocity = Vector2.left * speed;
        }
    }
    //no es crida al PUTU MAIN, fas un ALTRE PUTU METODE


    private void FixedUpdate()
    {
        timetodie--;

        if (puntuacio >= 20)
        {
            textguanyar.gameObject.SetActive(true);
            if (dispara_al_guanyar)
            {
                textguanyar.gameObject.SetActive(false);
            }

            
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene("");
            }
        }

        if (timetodie <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shot")
        {
            if (downed)
            {
                return;
            }

            rb2d.velocity = Vector2.down * 5;
            puntuacio++;
            text.text = "Puntuació: " + puntuacio;
            timetodie = 180;
            if (render.flipX)
            {
                this.transform.Rotate(0, 0, -90);
            }
            else
            {
                this.transform.Rotate(0, 0, 90);
            }
            downed = true;
            if (puntuacio >= 21)
            {
                dispara_al_guanyar = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
