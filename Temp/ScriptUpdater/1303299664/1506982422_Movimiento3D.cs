using UnityEngine;

public class Movimiento3D : MonoBehaviour
{
    public float velocidad = 5f;
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
        float movZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movX, 0.0f, movZ) * velocidad;
        
       
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);

        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            enSuelo = false; 
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name == "Plane") 
        {
            enSuelo = true;
        }
    }
}