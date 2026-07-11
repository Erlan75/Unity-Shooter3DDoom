using UnityEngine;

public class PrimeraPersona : MonoBehaviour
{
    
    public float velocidad = 5f;
    public float sensibilidad = 2f;
    public float gravedad = -9.81f;
    public Transform camara;

    [SerializeField] public float duracionRalentizacion = 3f;
    [SerializeField] public float factorRalentizacion = 0.5f;

    [SerializeField] private Animator animatorArma;

    private CharacterController cc;
    private float pitch = 0f;
    private Vector3 velY;

    private float velocidadOriginal;
    private Coroutine corrutinaRalentizacion;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        velocidadOriginal = velocidad;
    }

    void Update()
    {
        //Mirar con el raton
        float mx = Input.GetAxis("Mouse X") * sensibilidad;
        float my = Input.GetAxis("Mouse Y") * sensibilidad;
        transform.Rotate(0, mx, 0); // girar el cuerpo
        pitch = Mathf.Clamp(pitch - my, -80f, 80f); // mirar arriba y abajo
        camara.localEulerAngles = new Vector3(pitch, 0, 0);

        // Caminar (WASD o Flechas)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 mov = (transform.right * h + transform.forward * v).normalized * velocidad;

        // Gravedad simple
        if (cc.isGrounded && velY.y < 0) velY.y = -2f;
        velY.y += gravedad * Time.deltaTime;

        cc.Move((mov + velY) * Time.deltaTime);

        // Sincronizacion de animacion de caminar
        bool caminando = cc.velocity.sqrMagnitude > 0.1f && cc.isGrounded;
        if (animatorArma != null)
        {
            animatorArma.SetBool("IsWalking", caminando);
        }
    }

    public void Ralentizar(float duracion, float factor)
    {
        if (corrutinaRalentizacion != null)
        {
            StopCoroutine(corrutinaRalentizacion);
        }
        corrutinaRalentizacion = StartCoroutine(RutinaRalentizacion(duracion, factor));
    }

    private System.Collections.IEnumerator RutinaRalentizacion(float duracion, float factor)
    {
        velocidad = velocidadOriginal * factor;
        Debug.Log("¡Ralentizado! Velocidad reducida a: " + velocidad);

        yield return new WaitForSeconds(duracion);

        velocidad = velocidadOriginal;
        corrutinaRalentizacion = null;
        Debug.Log("Fin de la ralentizacion. Velocidad restaurada a: " + velocidad);
    }
}
