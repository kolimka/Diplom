using UnityEngine;
using UnityEngine.UI;


public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;
    private bool isGrounded = false;

    public float knockForce = 5.0f; // Сила отбрасывания
    private Vector2 lastMoveDirection = Vector2.right;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    public Canvas inventory; // Панель инвентаря
    public Button buttonInventory;

    public Canvas help;

    public Canvas task;
    private bool isTaskActivated = false;
    private bool isHelpActivated = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
    }

    private void Start()
    {
        buttonInventory.onClick.AddListener(ToggleInventory);
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            lastMoveDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
            Run();
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetKeyDown(KeyCode.I))  
            ToggleInventory();

        if (Input.GetKeyDown(KeyCode.F1))
            GetHelp();
    }

    private void FixedUpdate()
    {
        CheckGround();   
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    public override void GetDamage()
    {
        
        lives -= 1;
        Debug.Log("Lives: " + lives);

        // Отталкивание в противоположном направлении
        if (lastMoveDirection.x > 0)
        {
            rb.velocity = new Vector2(-knockForce, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(knockForce, rb.velocity.y);
        }
    }

    private void GetHelp()
    {
        if (!isHelpActivated)
        {
            help.gameObject.SetActive(true);
            isHelpActivated = true;
        }
        else
        {
            help.gameObject.SetActive(false);
            isHelpActivated = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Task") && !isTaskActivated)
        {
            // Активируйте Canvas Task
            task.gameObject.SetActive(true);

            // Отключите капсулу
            Destroy(other.gameObject);

            // Установите флаг, чтобы предотвратить повторную активацию
            isTaskActivated = true;
        }
    }

    void ToggleInventory()
    {
        if (inventory.gameObject.activeSelf)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    void OpenInventory()
    {
        inventory.gameObject.SetActive(true);
    }

    void CloseInventory()
    {
        inventory.gameObject.SetActive(false);
    }
}