using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public int vidaMax = 3;
    public bool esJugador = false;
    public int vidaActual;

    public Text textoVida;

    [SerializeField] private GameObject panelGameOver;

    void Start()
    {
        vidaActual = vidaMax;
        ActualizarUI();
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log(gameObject.name + " recibio " + cantidad + " de dano. Vida restante: " + vidaActual);
        ActualizarUI();
        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        if (esJugador)
        {
            if (panelGameOver != null) panelGameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegistrarMuerteEnemigo();
            }
            Destroy(gameObject);
        }
    }

    public int VidaActual()
    {
        return vidaActual;
    }

    public void ReintentarNivel()
    {
        Time.timeScale = 1f; // Restablece el tiempo del juego
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ActualizarUI()
    {
        if (textoVida != null)
        {
            textoVida.text = "VIDA: " + vidaActual;
        }
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMax) vidaActual = vidaMax;
        ActualizarUI();
    }
}
