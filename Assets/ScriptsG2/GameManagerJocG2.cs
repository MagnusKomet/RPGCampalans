using UnityEngine;
using UnityEngine.UI;

public class GameManagerJocG2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private GameObject Dispar;
    [SerializeField]
    GameObject FonsDispar;
    [SerializeField]
    GameObject Cartutxo;
    System.Random rand;
    SpriteRenderer render;
    Rigidbody2D rb2d;
    [SerializeField]
    int timetoshoot;
    [SerializeField]
    int timeshooting;
    bool shooting = false;
    float counter = 0;

    [SerializeField]
    GameObject ocell;

    [SerializeField]
    Text text;
    float timetospawn = 180;

    public int puntuacio = 0;

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        render = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        CanviarPosicioBird();
    }
    private void CanviarPosicioBird()
    {
        Instantiate(ocell);
    }

    private void FixedUpdate()
    {
        if (timetoshoot <= 0)
        {
            Cartutxo.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            Cartutxo.GetComponent<SpriteRenderer>().enabled = false;
        }
        timetospawn--;

        if (timetospawn <= 0)
        {
            Instantiate(ocell);
            timetospawn = 180 - (3 * counter);
            counter++;
        }

        timetoshoot--;
        if (shooting)
        {
            timeshooting--;

            if (timeshooting <= 0)
            {
                FonsDispar.GetComponent<SpriteRenderer>().enabled = false;
                shooting = false;
                timeshooting = 12;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!shooting && timetoshoot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timetoshoot = 60;
                shooting = true;
                FonsDispar.GetComponent<SpriteRenderer>().enabled = true;
                Instantiate(Dispar);
                Dispar.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Dispar.transform.position = new Vector3(Dispar.transform.position.x, Dispar.transform.position.y, -8);
            }
        }


    }
}

