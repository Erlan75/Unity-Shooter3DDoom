using UnityEngine;
using Unity.VisualScripting;

public class Disparar : MonoBehaviour
{
    public Camera camara;
    public int dano = 2;
    public float alcance = 100f;
    public float cadencia = 0.5f;
    public AudioClip sonidoDisparo;
    public GameObject muzzle;

    private AudioSource fuente;
    private float proximo = 0f;

    [SerializeField] private int maxMunicion = 10;
    [SerializeField] private int municionActual;
    [SerializeField] private float tiempoRecarga = 1.5f;
    [SerializeField] private bool estaRecargando = false;
    [SerializeField] private Animator animatorArma;

    void Start()
    {
        fuente = GetComponent<AudioSource>();
        if (muzzle != null) muzzle.SetActive(false);
        municionActual = maxMunicion;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= proximo && !estaRecargando && municionActual > 0)
        {
           proximo = Time.time + cadencia;
           Disparo(); 
        }    

        if ((Input.GetKeyDown(KeyCode.R) || municionActual <= 0) && municionActual < maxMunicion && !estaRecargando)
        {
            StartCoroutine(Recargar());
        }
    }

    void Disparo()
    {
        if (sonidoDisparo != null) fuente.PlayOneShot(sonidoDisparo);
        
        municionActual--;

        if (animatorArma != null)
        {
            animatorArma.SetTrigger("Fire");
        }

        if (muzzle != null) 
        { 
            muzzle.SetActive(true);
            Invoke("ApagarMuzzle", 0.05f);
        }

        Ray ray = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, alcance))
        {
            Vida v = hit.collider.GetComponentInParent<Vida>();
            if (v != null) v.RecibirDano(dano);
        }
    }

    void ApagarMuzzle()
    {
        if (muzzle != null)
        {
            
            muzzle.SetActive(false);
        }
    }

    System.Collections.IEnumerator Recargar()
    {
        estaRecargando = true;
        if (muzzle != null) muzzle.SetActive(false);
        if (animatorArma != null)
        {
            animatorArma.SetTrigger("Reload");
        }
        yield return new WaitForSeconds(tiempoRecarga);
        municionActual = maxMunicion;
        estaRecargando = false;
    }

    public int ObtenerMunicionActual() => municionActual;
    public int ObtenerMaxMunicion() => maxMunicion;
    public bool ObtenerEstaRecargando() => estaRecargando;
}
