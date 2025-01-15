using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {

    public float camSpeed = 8f;

    private Transform target;

    private float camHalfHeight;
    private float camHalfWidth;


    public Tilemap backgroundMap;
    private Vector3 bottomLeftCamLimit;
    private Vector3 topRightCamLimit;

    // Use this for initialization
    void Start () {
        //posa el target allà on tenim el jugador, busquem el controlador (l'script)
        PlayerController playerController = FindObjectOfType<PlayerController>();
        target = playerController.transform;

        Debug.Log("CameraController:: Background (min/max) (" + backgroundMap.localBounds.min + "/" + backgroundMap.localBounds.max + ")");

        //posicionem inicialment la camera al mig del mapa
        transform.position = new Vector3(
            (backgroundMap.localBounds.max.x + backgroundMap.localBounds.min.x) / 2,
            (backgroundMap.localBounds.max.y + backgroundMap.localBounds.min.y) / 2,
            transform.position.z
        );
        Debug.Log("CameraController:: InitialPos: " + transform.position);

        //Recuperem la càmera: Camera.main
        //recuperem la mida de la càmera
        //  ^
        //  |
        // h|   w
        //  |------>
        //
        camHalfHeight = Camera.main.orthographicSize;  //recupera el size (h) de la camera, per ex. 5
        camHalfWidth = camHalfHeight * Camera.main.aspect; //si l'aspect és 16:9, llavors w=8,9
        //camHalfWidth = camHalfHeight * Screen.width / Screen.height; //si l'aspect és 16:9, llavors w=8,9
        Debug.Log("CameraController:: Camera (hW/hH): (" + camHalfWidth + "/" + camHalfHeight + ")");

        //calculem els límits per on es pot moure el centre de la camara, que seran
        // les mides del mapa restant la meitat de la mida de la camara   
        float leftBound = (float)(camHalfWidth - backgroundMap.size.x / 2.0f);
        float rightBound = (float)(backgroundMap.size.x / 2.0f - camHalfWidth);
        float bottomBound = (float)(camHalfHeight - backgroundMap.size.y / 2.0f);
        float topBound = (float)(backgroundMap.size.y / 2.0f - camHalfHeight);

        bottomLeftCamLimit = transform.position + new Vector3(leftBound, bottomBound, 0f);
        topRightCamLimit = transform.position + new Vector3(rightBound, topBound, 0f);
        Debug.Log("CameraController:: CameraLimits (min/max): (" + bottomLeftCamLimit + "/" + topRightCamLimit + ")");


        //informem al PlayerController dels Bounds del mapa, que els emmagatzema
        playerController.SetPlayerBounds(backgroundMap.localBounds.min, backgroundMap.localBounds.max);
    }
    private void Update()
    {
        //posem la camara que segueixi al jugador
        Vector3 posicioPlayer = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, posicioPlayer, camSpeed * Time.deltaTime);
        //Debug.Log("CameraController:: CamPos: " + transform.position);
    }

    // LateUpdate is called once per frame, after all others Update
    void LateUpdate () {


        //keep the camera inside the bounds: que la camara no surti dels limits, retallem
        Vector3 camClampedPos = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftCamLimit.x, topRightCamLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftCamLimit.y, topRightCamLimit.y),
            transform.position.z);

        transform.position = camClampedPos;
        //Debug.Log("CameraController:: ClampedCamPos: " + transform.position);
    }
}

