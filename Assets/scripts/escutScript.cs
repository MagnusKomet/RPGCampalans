using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escutScript : MonoBehaviour
{
    SpriteRenderer srEscut;
    [SerializeField]
    float tempsDesaparicioEscut;
    [SerializeField]
    ParticleSystem animacioEscut;
    ParticleSystem instanciaAnimacioEscut;
    [SerializeField]
    AudioSource soEscut;
    float time = 0;

    void Start()
    {
        srEscut = GetComponent<SpriteRenderer>();
        srEscut.enabled = false;
    }

    void Update()
    {
        if (srEscut.enabled)
        {
            time += Time.deltaTime;

            if (time >= tempsDesaparicioEscut)
            {
                StartCoroutine(FadeOut());
                time = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bala")
        {
            srEscut.enabled = true;
            Vector2 posicio = new Vector2(transform.position.x - 0.8f, transform.position.y);
            instanciaAnimacioEscut = Instantiate(animacioEscut, posicio, animacioEscut.transform.rotation);
            soEscut.Play();
            Destroy(instanciaAnimacioEscut.gameObject, instanciaAnimacioEscut.main.duration + instanciaAnimacioEscut.main.startLifetime.constantMax);
        }
    }

    private IEnumerator FadeOut()
    {
        Color originalColor = srEscut.color;
        float elapsedTime = 0;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / 1f);
            srEscut.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        srEscut.enabled = false;
        srEscut.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
    }
}
