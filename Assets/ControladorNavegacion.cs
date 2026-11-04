using UnityEngine;
using UnityEngine.SceneManagement; 
public class ControladorNavegacion : MonoBehaviour
{
    
    public void CambiarEscenaJuego()
    {
        
        SceneManager.LoadScene("EscenaJuego"); 
    }
}