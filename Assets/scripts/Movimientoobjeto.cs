using UnityEngine;

public class MovimientoPared : MonoBehaviour
{
   public float distancia = 5f;       // Cuánta distancia recorrer hacia cada lado
    public float velocidad = 2f;       // Velocidad de movimiento

    private Vector3 posicionInicial;
    private bool yendoADerecha = true;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float desplazamiento = velocidad * Time.deltaTime;
        Vector3 direccion = yendoADerecha ? Vector3.right : Vector3.left;

        transform.Translate(direccion * desplazamiento);

        float distanciaDesdeInicio = transform.position.x - posicionInicial.x;

        if (yendoADerecha && distanciaDesdeInicio >= distancia)
        {
            yendoADerecha = false;
        }
        else if (!yendoADerecha && distanciaDesdeInicio <= -distancia)
        {
            yendoADerecha = true;
        }
    }
}
