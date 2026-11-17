using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    private int nivelAnterior = 1;

    [Header("Interfaz HUD y Paneles")]
    public TextMeshProUGUI textoPuntajeUI;
    public GameObject panelPausa;
    public GameObject panelGameOver;
    public TextMeshProUGUI textoPuntajeFinalUI;
    public TextMeshProUGUI textoRecordFinalUI;

    [Header("Efectos Visuales y Progreso UI")]
    public ParticleSystem particulasNivel;
    public Slider barraProgresoNivel;
    public TextMeshProUGUI textoAvisoNivelUI;

    private bool juegoTerminado = false;
    private float tiempoAviso = 0f;

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

        if (barraProgresoNivel != null)
        {
            barraProgresoNivel.value = puntajeActual % 100;
        }

        if (nivelActual > nivelAnterior)
        {
            nivelAnterior = nivelActual;
            if (particulasNivel != null) particulasNivel.Play();

            if (textoAvisoNivelUI != null)
            {
                textoAvisoNivelUI.text = "¡NIVEL " + nivelActual + "!";
                textoAvisoNivelUI.gameObject.SetActive(true);
                tiempoAviso = 2f;
            }
        }

        if (tiempoAviso > 0f)
        {
            tiempoAviso -= Time.deltaTime;
            if (tiempoAviso <= 0f && textoAvisoNivelUI != null)
            {
                textoAvisoNivelUI.gameObject.SetActive(false);
            }
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
        bool esNuevoRecord = false;

        if (puntajeActual > mejorPuntaje)
        {
            mejorPuntaje = puntajeActual;
            PlayerPrefs.SetInt("MejorPuntaje", mejorPuntaje);
            PlayerPrefs.Save();
            esNuevoRecord = true;
        }

        Time.timeScale = 0f;
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            if (textoPuntajeFinalUI != null) textoPuntajeFinalUI.text = "Puntaje Final: " + puntajeActual;
            if (textoRecordFinalUI != null)
            {
                textoRecordFinalUI.text = esNuevoRecord 
                    ? "¡NUEVO RÉCORD!: " + mejorPuntaje 
                    : "Mejor Récord: " + mejorPuntaje;
            }
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