using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    #region  Variables
    [Header("Movimiento")]
    [Range(0, 10)]
    [SerializeField] private float movSpeed = 5f;
    private float horizontal;
    private float vertical;
    private Vector2 dir;

    [Header("Esquive")]
    [Range(5, 10)]
    [SerializeField] private float evadeForce;
    private bool isEvading = false;
    [SerializeField] private int maxEvades = 2;  // Máximo de cargas de esquive
    private int currentEvades;  // Cargas disponibles
    private float evadeDuration = 0.2f;
    [SerializeField] private float evadeCooldown = 2f; // Tiempo de cooldown por carga de esquive
    
    private Vector2 mousePos;
    private Rigidbody2D rb;

    [Header("Interaction")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform interactPoint;
    [SerializeField] private GunHolderScript gunHolder;
    [SerializeField] private CameraWork cameraFollow;
    private HealthScript health;
    private Camera mainCamera;

#endregion

#region Metodos
void Start()
{
    rb = GetComponent<Rigidbody2D>();
    health = GetComponent<HealthScript>();
    currentEvades = maxEvades;
    mainCamera = Camera.main;

    if (photonView.IsMine) // Verifica si este es el jugador local
    {
        cameraFollow = mainCamera.GetComponent<CameraWork>();

        if (cameraFollow != null)
        {
            cameraFollow.SetPlayer(transform);  // Asigna la cámara para el jugador local
        }
        else
        {
            Debug.LogError("No se encontró el script CameraWork en la cámara principal.");
        }
    }
    else
    {
        Camera camera = GetComponentInChildren<Camera>();
        if (camera != null)
        {
            camera.gameObject.SetActive(false); // Desactiva la cámara del jugador remoto
        }
    }
}

    void Update()
    {
        if (photonView.IsMine)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            dir = new Vector2(horizontal, vertical);

            Look();

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1) && currentEvades > 0 && !isEvading)
            {
                Evade();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ONInteract();
            }
            
            ChangeGuns();

            if (Input.GetKeyDown(KeyCode.F))
            {
                health.TakeDamage(10);
                Debug.Log(health._currentHealth);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void FixedUpdate()
    {
       if (photonView.IsMine)
        {
            if (isEvading)
            {
                return;
            }
            
            Move();
        }
    }

    void Move()
    {
        rb.velocity = dir.normalized * movSpeed;
    }

    void Evade()
    {
        if (rb.velocity != Vector2.zero)
        {
            isEvading = true;
            currentEvades--;  // Gastamos una carga
            rb.velocity = Vector2.zero;
            rb.AddForce(dir.normalized * evadeForce, ForceMode2D.Impulse);

            StartCoroutine(EndEvade());
            StartCoroutine(ReloadEvade());
        }
        
    }

    void ChangeGuns()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunHolder.ChangeGun(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunHolder.ChangeGun(1);
        }
    }

    IEnumerator EndEvade()
    {
        yield return new WaitForSeconds(evadeDuration);
        isEvading = false;
    }

    IEnumerator ReloadEvade()
    {
        yield return new WaitForSeconds(evadeCooldown);

        if (currentEvades < maxEvades) // Solo recargamos si hay menos de 2 cargas
        {
            currentEvades++;
        }
    }

    public float GetevadeTime()
    {
        return(float) currentEvades / maxEvades;
    }

    void Look()
    {
        if (!photonView.IsMine) return; // chequeo x las dudas

        if (mainCamera == null) return;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ONInteract()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(interactPoint.position, new Vector2(1f, 1f), 0f, interactableLayer); 

        foreach (Collider2D items in colliders)
        {
            if (items.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                Debug.Log("Toque: " + "<color=green>" + " " + interactable + "</color>");
            }
        }      
    }

    #endregion
}
