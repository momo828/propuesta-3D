using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Generación de Nivel")]
    public GameObject[] modulosPrefabs;
    public float longitudModulo = 10f;
    private float proximoSpawnZ = 0f;
    private float proximoSpawnX = 0f; // La variable ahora está correctamente arriba

    [Header("Dificultad y Puntaje")]
    public float velocidadAvance = 5f;
    public float aumentoDificultad = 0.2f;
    public int puntajeActual = 0;
    private float puntajeExacto = 0f; 

    [Header("Interfaz Visual")]
    public TextMeshProUGUI textoPuntajeUI; 

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GenerarModulo();
        }
    }

    void Update()
    {
        velocidadAvance += aumentoDificultad * Time.deltaTime;
        
        puntajeExacto += velocidadAvance * Time.deltaTime;
        puntajeActual = (int)puntajeExacto;

        if (textoPuntajeUI != null)
        {
            textoPuntajeUI.text = "Puntos: " + puntajeActual;
        }
       
    int nivelActual = (puntajeActual / 100) + 1;
    
    if (textoPuntajeUI != null)
    {
        textoPuntajeUI.text = "Nivel: " + nivelActual + " | Puntos: " + puntajeActual;
    }
    }

    public void GenerarModulo()
    {
        int indiceAleatorio = Random.Range(0, modulosPrefabs.Length);

        // Desfasa el siguiente módulo a la izquierda o derecha
        float curvaX = Random.Range(-3f, 3f);
        proximoSpawnX += curvaX;

        // Evita que la curva se salga demasiado de la pantalla
        proximoSpawnX = Mathf.Clamp(proximoSpawnX, -6f, 6f);

        // Genera el módulo en la nueva posición
        Instantiate(modulosPrefabs[indiceAleatorio], new Vector3(proximoSpawnX, 0, proximoSpawnZ), Quaternion.identity);
        
        proximoSpawnZ += longitudModulo;
    }

    public void ReiniciarJuego()
    {
        int mejorPuntaje = PlayerPrefs.GetInt("MejorPuntaje", 0);
        if (puntajeActual > mejorPuntaje)
        {
            PlayerPrefs.SetInt("MejorPuntaje", puntajeActual);
            PlayerPrefs.Save(); 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}