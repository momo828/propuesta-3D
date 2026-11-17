using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorNavegacion : MonoBehaviour
{
    public TextMeshProUGUI textoRecordUI;

    void Start()
    {
       
        Time.timeScale = 1f;

        
        int mejorPuntaje = PlayerPrefs.GetInt("MejorPuntaje", 0);
        if (textoRecordUI != null)
        {
            textoRecordUI.text = "Mejor Récord: " + mejorPuntaje;
        }
    }

    public void CambiarEscenaJuego()
    {
        SceneManager.LoadScene("EscenaJuego");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo de la aplicación...");
        Application.Quit();
    }
}