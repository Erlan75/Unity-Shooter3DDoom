using UnityEngine;

public class ZonaMeta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el jugador entra en contacto con el trigger de escape
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GanarJuego();
            }
        }
    }
}
