using UnityEngine;
using UnityEngine.UI;


public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 18f;

    [SerializeField] private int health = 5;
    private int previousHealth;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    private bool isGrounded = false;

    public float knockForce = 4.5f; // Сила отбрасывания
    private Vector2 lastMoveDirection = Vector2.right;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    public Canvas pause;
    private bool isPaused = false;

    public Canvas inventory; // Панель инвентаря
    public Button buttonInventory;

    public Canvas help;

    public Canvas task;
    private bool isTaskActivated = false;
    private bool isHelpActivated = false;

    public Canvas quiz;
    private bool isQuizActivated = false;

    public Canvas guide;
    private void Awake()
    {
        lives = 5;
        health = lives;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))  
            ToggleInventory();

        if (Input.GetKeyDown(KeyCode.F1))
            GetHelp();

        if (health > lives)
            health = lives;

        if (health != previousHealth)
        {
            ChangeHealth(); // Вызываем метод обновления интерфейса
            previousHealth = health; // Обновляем предыдущее значение здоровья
        }
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

        health -= 1;

        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
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

    private void ChangeHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;
            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Останавливаем время
        isPaused = true;
        pause.gameObject.SetActive(true); // Отображаем окно паузы
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Возобновляем время
        isPaused = false;
        pause.gameObject.SetActive(false); // Скрываем окно паузы
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
    public void CloseGuide()
    {
        guide.gameObject.SetActive(false);
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
        if (other.CompareTag("Quiz") && !isQuizActivated)
        {
            quiz.gameObject.SetActive(true);

            Destroy(other.gameObject);

            isQuizActivated = true;
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