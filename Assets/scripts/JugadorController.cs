using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JugadorController : MonoBehaviour
{
    private Rigidbody rb;
    private int contador;
    public TMP_Text textoContador, textoGanar, Matricula, textoTiempo;
    public float velocidad;
    private float tiempoRestante = 120f;
    private bool juegoTerminado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        contador = 0;
        setTextoContador();
        textoGanar.text = "";
        Matricula = GameObject.Find("Matricula").GetComponent<TMP_Text>();
        Matricula.text = "Jhoan Vargas 1210417";
        textoTiempo = GameObject.Find("TextoTiempo").GetComponent<TMP_Text>();
    }

    IEnumerator ReiniciarNivel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void Update()
    {
        if (!juegoTerminado)
        {
            tiempoRestante -= Time.deltaTime;
            textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString() + "s";

            if (tiempoRestante <= 0)
            {
                textoGanar.text = "¡Tiempo agotado! Perdiste.";
                juegoTerminado = true;
                StartCoroutine(ReiniciarNivel());
            }

            if (transform.position.y < -5f && !juegoTerminado)
            {
                juegoTerminado = true;
                textoGanar.text = "¡Te caíste!";
                StartCoroutine(ReiniciarNivel());
            }

        }
    }

    void FixedUpdate()
    {
        if (juegoTerminado) return;

        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        rb.AddForce(movimiento * velocidad);
    }

    void OnTriggerEnter(Collider other)
    {
        if (juegoTerminado) return;

        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            contador++;
            setTextoContador();
        }
    }

    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();
    if (contador >= 12)
    {
        textoGanar.text = "¡Ganaste!";
        juegoTerminado = true;
        StartCoroutine(VolverAlMenu());
    }
    }

    IEnumerator VolverAlMenu()
    {
        yield return new WaitForSeconds(5f);

        int escenaActual = SceneManager.GetActiveScene().buildIndex;
    int totalEscenas = SceneManager.sceneCountInBuildSettings;

    if (escenaActual + 1 < totalEscenas)
    {
        SceneManager.LoadScene(escenaActual + 1);
    }
    else
    {
        SceneManager.LoadScene("Menu Inicio");
    }
    }
}
