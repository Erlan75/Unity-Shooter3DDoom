using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public AudioClip sonidoCuracion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambiado a GetComponentInParent para evitar nulos en la jerarquia del jugador
            Vida vidaJugador = other.GetComponentInParent<Vida>();
            if (vidaJugador != null)
            {
                // Accedemos directamente a vidaActual y vidaMax
                if (vidaJugador.vidaActual < vidaJugador.vidaMax)
                {
                    vidaJugador.Curar(1);

                    if (sonidoCuracion != null)
                    {
                        AudioSource.PlayClipAtPoint(sonidoCuracion, transform.position);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}
