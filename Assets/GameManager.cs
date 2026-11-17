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
    private float proximoSpawnX = 0f;

    [Header("Dificultad y Puntaje")]
    public float velocidadAvance = 5f;
    public float aumentoDificultad = 0.5f;
    public int puntajeActual = 0;
    private float puntajeExacto = 0f;

    [Header("Interfaz HUD y Paneles")]
    public TextMeshProUGUI textoPuntajeUI;
    public GameObject panelPausa;
    public GameObject panelGameOver;
    public TextMeshProUGUI textoPuntajeFinalUI;
    public TextMeshProUGUI textoRecordFinalUI;

    private bool juegoTerminado = false;

    void Awake()
    {
        instancia = this;
        Time.timeScale = 1f;
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
        if (juegoTerminado) return;

        velocidadAvance += aumentoDificultad * Time.deltaTime;
        puntajeExacto += velocidadAvance * Time.deltaTime;
        puntajeActual = (int)puntajeExacto;

        int nivelActual = (puntajeActual / 100) + 1;

        if (textoPuntajeUI != null)
        {
            textoPuntajeUI.text = "Nivel: " + nivelActual + " | Puntos: " + puntajeActual;
        }
    }

    public void GenerarModulo()
    {
        int indiceAleatorio = Random.Range(0, modulosPrefabs.Length);
        float curvaX = Random.Range(-3f, 3f);
        proximoSpawnX += curvaX;
        proximoSpawnX = Mathf.Clamp(proximoSpawnX, -6f, 6f);

        Instantiate(modulosPrefabs[indiceAleatorio], new Vector3(proximoSpawnX, 0, proximoSpawnZ), Quaternion.identity);
        proximoSpawnZ += longitudModulo;
    }

    public void ReiniciarJuego()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        int mejorPuntaje = PlayerPrefs.GetInt("MejorPuntaje", 0);
        if (puntajeActual > mejorPuntaje)
        {
            mejorPuntaje = puntajeActual;
            PlayerPrefs.SetInt("MejorPuntaje", mejorPuntaje);
            PlayerPrefs.Save();
        }

        
        Time.timeScale = 0f;
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            if (textoPuntajeFinalUI != null) textoPuntajeFinalUI.text = "Puntaje Final: " + puntajeActual;
            if (textoRecordFinalUI != null) textoRecordFinalUI.text = "Mejor Récord: " + mejorPuntaje;
        }
    }

   

    public void PausarJuego()
    {
        if (juegoTerminado) return;
        Time.timeScale = 0f;
        if (panelPausa != null) panelPausa.SetActive(true);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1f;
        if (panelPausa != null) panelPausa.SetActive(false);
    }

    public void RecargarEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}