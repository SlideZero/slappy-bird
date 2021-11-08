using UnityEngine;

public class PlayerController : PhysicsObject
{
    public GameManager gameManager;
    //public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;
    public AudioClip playerFlap;
    public AudioClip playerHit;

    private Animator _animator;
    private AudioSource _audioSource;
    private Touch _touch;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();

        if (gameManager.GameOverState)
        {
            _animator.SetBool("Death", true);
            transform.position = new Vector3(-1.1f, transform.position.y, 0);
        }
    }

    protected override void FixedUpdate()
    {
        if (gameManager.GameStartState)
            base.FixedUpdate();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && !gameManager.GameOverState)//&& grounded)
        {
            _audioSource.PlayOneShot(playerFlap);
            velocity.y = jumpTakeOffSpeed;
        }

        if (!gameManager.GameStartState)
        {
            if (Input.GetButtonDown("Jump"))
                gameManager.GameStartState = true;
        }

        // Touch input controls
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began && !gameManager.GameOverState)
            {
                _audioSource.PlayOneShot(playerFlap);
                velocity.y = jumpTakeOffSpeed;
            }

            if (!gameManager.GameStartState)
            {
                if (_touch.phase == TouchPhase.Began)
                    gameManager.GameStartState = true;
            }
        }
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    if (velocity.y > 0)
        //    {
        //        velocity.y = velocity.y * 0.5f;
        //    }
        //}

        //targetVelocity = move * maxSpeed;
    }

    protected override void ComputeCollision()
    {
        if (!gameManager.GameOverState)
            _audioSource.PlayOneShot(playerHit);

        gameManager.GameOverState = true;
    }
}
