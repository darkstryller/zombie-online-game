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
    [SerializeField] private float evadeCooldown = 2f; // Tiempo de recarga por carga
    
    private Vector2 mousePos;
    private Rigidbody2D rb;

    [Header("Interaction")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform interactPoint;

#endregion

#region Metodos
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentEvades = maxEvades; // Empezamos con las 2 cargas llenas
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

            // Empezamos el temporizador de evasión
            StartCoroutine(EndEvade());

            // Iniciamos la recarga de esa carga gastada
            StartCoroutine(ReloadEvade());
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
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
