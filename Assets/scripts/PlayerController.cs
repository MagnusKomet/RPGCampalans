using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //la velocitat del moviment del jugador
    public float moveSpeed;
    //l'animador de l'sprite del jugador
    public Animator playerAnimator;

    //aquí guardarem UNA i només UNA referencia al jugador,
    // com que és estàtic serà accesible per tots els objectes
    public static PlayerController instance;

    //el punt d'unió entre dues àrees. És com un teletransportador, un punt que 
    // existeix a les dues àrees i que farem servir per canviar d'una a una altra.
    public string areaJointPoint;

    //per controlar la darrera escena quan transitem cap un Screen, la que deixem enrera
    private string lastArea;
    private Vector3 lastPositionPlayer;

    //Per controlar els límits per on ens podem moure
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private Rigidbody2D playerRB;

    // Use this for initialization
    void Start () {
        //si no existeix el PlayerController
        if (instance == null)
        {
            //l'assignem
            instance = this;
        } else {
            //el destruim, si ja existeix
            Destroy(gameObject);
        }

        //que no destrueixi el jugador
        DontDestroyOnLoad(gameObject);

        //assignem el rigidbody
        playerRB = this.GetComponent<Rigidbody2D>();

        //assignem l'animador, si no està assignat per paràmetre
        // ho deixem que es pugui canviar per paràmetre per si volem canviar 
        // l'animació per exemple...
        if ( playerAnimator == null )
            playerAnimator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //podem la velocitat del rigidbody a on indica l'input
        playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical")) * moveSpeed;

        //li diem a l'Animator quins paràmetres té per que canviï l'animació a usar
        // l'animador té dos paràmetres i decideix quina animació usar depenent d'aquests.
        playerAnimator.SetFloat("moveX", playerRB.velocity.x);
        playerAnimator.SetFloat("moveY", playerRB.velocity.y);

        //si estem quiets, AxisRaw és un enter i llavors li passem a l'animador
        // el darrer moviment, per saber cap on hem de quedar encarats.
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 
            || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            playerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        //clamp player inside bounds: que no surti!
        // la funció clamp precisament retalla, si cal, un valor entre dos altres valors
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z);
    }


    // Guarda la darrera posició del jugador a l'escena
    public void SetLastArea(string area){
        lastArea = area;
        lastPositionPlayer = this.transform.position;
        Debug.Log("PlayerController:: Guardem la darrera Area on hem estat: " + lastArea);
    }

    //Recupera la darrera àrea emmagatzemada on ha estat el Player
    public string GetLastArea()
    {
        return lastArea;
    }

    //Recupera la darrera posició emmagatzemada
    public Vector3 GetLastPosition()
    {
        return lastPositionPlayer;
    }

    //Guarda els bounds per on ens podem moure, amb un petit ajust per no tallar
    // el personatge a les vores.
    public void SetPlayerBounds(Vector3 bottomLeft, Vector3 topRight) {
        this.bottomLeftLimit = bottomLeft + new Vector3(.5f, 1f, 0f);
        this.topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }

}
