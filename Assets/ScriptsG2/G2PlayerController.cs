using UnityEngine;
using UnityEngine.SceneManagement;

public class G2PlayerController : MonoBehaviour 
{


    //aquí guardarem UNA i només UNA referencia al jugador,
    // com que és estàtic serà accesible per tots els objectes
    public static G2PlayerController instance;

    
    [SerializeField]
    private Collider2D outCamera;
    [SerializeField]
    private Camera cam;
    public bool HasKey = false;
    public bool HasShotgun = false;
    private bool va = false;

    [SerializeField]
    private GameObject shotgun;

    
    [SerializeField]
    private Animator CofreAnim;

    // Use this for initialization
    void Start () {
        //si no existeix el PlayerController
        
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "JocG2Scene")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (SceneManager.GetActiveScene().name == "LabrG2Scene")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if (!va)
            {
                cam = FindObjectOfType<Camera>();
                Collider2D[] colliders = GameObject.FindObjectsOfType<Collider2D>();

                foreach (Collider2D item in colliders)
                {
                    if (item.name == "CameraOutIn")
                    {
                        outCamera = item;
                    }
                }
                shotgun = GameObject.Find("Shotgun");
                Animator[] animators = GameObject.FindObjectsOfType<Animator>();

                foreach (Animator animator in animators)
                {
                    if (animator.name == "Cofre")
                    {
                        CofreAnim = animator;
                    }
                }
                va = true;
            }

        }
    }
    // Update is called once per frame
    void Update () {
        //podem la velocitat del rigidbody a on indica l'input
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "LabrG2Scene")
        {
            if (collision.CompareTag("backgroundCameraKnow"))
            {
                cam.orthographicSize = 3;
                this.gameObject.GetComponent<PlayerController>().moveSpeed = 7.5f;
            }
        }
        
        

    }
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "LabrG2Scene")
        {
            if (collision.CompareTag("backgroundCameraKnow"))
            {
                cam.orthographicSize = 5;
                this.gameObject.GetComponent<PlayerController>().moveSpeed = 5;
            }
            if (collision.CompareTag("key"))
            {
                Destroy(collision.gameObject);
                HasKey = true;

            }
            if (collision.CompareTag("cofre") && HasKey)
            {
                CofreAnim.SetBool("AbreSesamo", true);
                HasShotgun = true;
                shotgun.GetComponent<SpriteRenderer>().enabled = true;

            }
            if (collision.CompareTag("enter new game") && HasShotgun)
            {
                SceneManager.LoadScene("JocG2Scene");
                Destroy(FindAnyObjectByType<CameraController>());
            }
        }
            

    }

    

}
