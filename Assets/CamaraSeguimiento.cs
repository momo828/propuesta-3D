using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform jugador; 
    private Vector3 distanciaFija;

    void Start()
    {
        
        distanciaFija = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        if (jugador != null)
        {
            
            Vector3 nuevaPosicion = new Vector3(
                jugador.position.x + distanciaFija.x, 
                transform.position.y, 
                jugador.position.z + distanciaFija.z
            );
            
            transform.position = nuevaPosicion;
        }
    }
}