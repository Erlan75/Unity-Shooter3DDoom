using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Patron Singleton para un acceso facil desde cualquier otro script
    public static GameManager Instance { get; private set; }

    [SerializeField] private Text textoEnemigosRestantes;
    [SerializeField] private GameObject panelVictoria;

    private int enemigosVivos = 0;
    private bool juegoTerminado = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Uso de directivas de preprocesamiento para compatibilidad con Unity 6
#if UNITY_2023_1_OR_NEWER
        EnemigoIA[] enemigos = FindObjectsByType<EnemigoIA>(FindObjectsSortMode.None);
#else
        EnemigoIA[] enemigos = FindObjectsOfType<EnemigoIA>();
#endif
        enemigosVivos = enemigos.Length;
        ActualizarUIEnemigos();
    }

    public void RegistrarMuerteEnemigo()
    {
        enemigosVivos--;
        if (enemigosVivos < 0)
        {
            enemigosVivos = 0;
        }
        ActualizarUIEnemigos();

        if (PuedoGanar())
        {
            GanarJuego();
        }
    }

    public bool PuedoGanar() => enemigosVivos <= 0;

    public void GanarJuego()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        // Congelar juego y liberar cursor
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("¡Victoria! Juego terminado.");
    }

    private void ActualizarUIEnemigos()
    {
        if (textoEnemigosRestantes != null)
        {
            textoEnemigosRestantes.text = "ENEMIGOS: " + enemigosVivos;
        }
    }
}
