using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoIA : MonoBehaviour
{
    [SerializeField] private float danoAtaque = 10f;
    [SerializeField] private float cadenciaAtaque = 1.5f;
    [SerializeField] private float distanciaAtaque = 5f;

    private NavMeshAgent agent;
    private Transform jugador;
    private float siguienteTiempoAtaque = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject objetoJugador = GameObject.FindWithTag("Player");
        if (objetoJugador != null)
        {
            jugador = objetoJugador.transform;
        }
    }

    void Update()
    {
        if (jugador == null) return;

        // Persecucion constante hacia el jugador
        agent.SetDestination(jugador.position);

        // Ataque automatico basado en distancia y tiempo
        float distanciaActual = Vector3.Distance(transform.position, jugador.position);
        if (distanciaActual <= distanciaAtaque && Time.time >= siguienteTiempoAtaque)
        {
            AtacarJugador();
            siguienteTiempoAtaque = Time.time + cadenciaAtaque;
        }
    }

    private void AtacarJugador()
    {
        Vida vidaJugador = jugador.GetComponent<Vida>();
        if (vidaJugador != null)
        {
            vidaJugador.RecibirDano((int)danoAtaque);
        }
        Debug.Log("¡El enemigo ha disparado al jugador!");
    }
}
