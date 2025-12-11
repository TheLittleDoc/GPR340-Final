using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;

    [Header("Player Data")] 
    [SerializeField] private float maxHealth = 20f;
    private float currentHealth;

    [SerializeField] private UIManager uiManager;
    

    private Rigidbody rb;

    private float verticalLook = 0f;

    private float horizontalInput;
    private float verticalInput;

    private float mouseX;
    private float mouseY;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        currentHealth = maxHealth;
        uiManager.setMaxHealth(maxHealth);
    }

    private void Update()
    {
        GetInput();
        HandleLook();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
    }

    private void HandleLook()
    {
        // Horizontal rotation (player)
        transform.Rotate(Vector3.up * mouseX);

        // Vertical rotation (camera only)
        verticalLook -= mouseY;
        verticalLook = Mathf.Clamp(verticalLook, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalLook, 0f, 0f);
    }

    private void MovePlayer()
    {
        Vector3 move = transform.forward * verticalInput + transform.right * horizontalInput;
        move.y = 0f;
        move.Normalize();

        rb.MovePosition(rb.position + move * speed * Time.deltaTime);
    }
    
    public void TakeDamage(float damage) 
    { 
        currentHealth -= damage; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        uiManager.setHealth(currentHealth);
        Debug.Log("Player Health: " + currentHealth); 
        if(currentHealth <= 0) 
        { 
            Die(); 
        } 
    } 
    public void Heal(float amount) 
    { 
        currentHealth += amount; 
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        uiManager.setHealth(currentHealth);
         
    } 
    private void Die() {  
        Destroy(gameObject); 
    } 
    public float GetCurrentHealth() 
    { 
        return currentHealth; 
    } 
    public float GetHealthPercentage() 
    { 
        return currentHealth / maxHealth; 
    } 
}