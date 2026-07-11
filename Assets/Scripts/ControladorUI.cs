using UnityEngine;
using UnityEngine.UI;

public class ControladorUI : MonoBehaviour
{
    [SerializeField] private Text textoMunicion;
    [SerializeField] private Disparar scriptDisparar;

    void Start()
    {
        if (scriptDisparar == null)
        {
#if UNITY_2023_1_OR_NEWER
            scriptDisparar = FindFirstObjectByType<Disparar>();
#else
            scriptDisparar = FindObjectOfType<Disparar>();
#endif
        }
    }

    void Update()
    {
        if (scriptDisparar != null && textoMunicion != null)
        {
            if (scriptDisparar.ObtenerEstaRecargando())
            {
                textoMunicion.text = "RECARGANDO...";
            }
            else
            {
                textoMunicion.text = "MUNICIÓN: " + scriptDisparar.ObtenerMunicionActual() + " / " + scriptDisparar.ObtenerMaxMunicion();
            }
        }
    }
}
