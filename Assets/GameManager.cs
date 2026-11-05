using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Generación de Nivel")]
    public GameObject[] modulosPrefabs; 
    public float longitudModulo = 10f; 
    private float proximoSpawnZ = 0f;

    [Header("Dificultad Progresiva")]
    public float velocidadJuego = 1f;
    public float aumentoDificultad = 0.05f;

    void Start()
    {
        
        for (int i = 0; i < 5; i++)
        {
            GenerarModulo();
        }
    }

    void Update()
    {
        
        velocidadJuego += aumentoDificultad * Time.deltaTime;
    }

    public void GenerarModulo()
    {
        
        int indiceAleatorio = Random.Range(0, modulosPrefabs.Length);
        Instantiate(modulosPrefabs[indiceAleatorio], new Vector3(0, 0, proximoSpawnZ), Quaternion.identity);
        
        proximoSpawnZ += longitudModulo;
    }

    
    public void ReiniciarJuego()
    {
        
        int partidas = PlayerPrefs.GetInt("PartidasJugadas", 0);
        PlayerPrefs.SetInt("PartidasJugadas", partidas + 1);
        PlayerPrefs.Save();

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}