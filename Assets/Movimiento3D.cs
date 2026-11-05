using UnityEngine;

public class Movimiento3D : MonoBehaviour
{
    public float velocidadLateral = 7f;
    public float fuerzaSalto = 6f;
    private Rigidbody rb;
    private bool enSuelo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float movX = Input.GetAxis("Horizontal");

      
        float velocidadZ = GameManager.instancia.velocidadAvance;

        
        rb.linearVelocity = new Vector3(movX * velocidadLateral, rb.linearVelocity.y, velocidadZ);

        
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false;
        }

     
        if (transform.position.y < -3f)
        {
            GameManager.instancia.ReiniciarJuego();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane") 
        {
            enSuelo = true;
        }
        
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            GameManager.instancia.ReiniciarJuego();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Generador"))
        {
            GameManager.instancia.GenerarModulo();
        }
    }
}