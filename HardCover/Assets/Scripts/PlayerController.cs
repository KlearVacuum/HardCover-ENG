using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveForce;
    [Range(0, 1)] [SerializeField] float stopDampenRatio;
    Vector2 moveForce;

    bool flipped;
    float startingScale;
    Vector2 currentFacingDir;
    float flipXTime = 0.1f;
    float facingDirTime;

    private int canMove = 1;

    Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    Animator animator;

    private Vector3 mTempPosition;
    private Quaternion mTempRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = spriteRenderer.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        GlobalGameData.playerController = this;

        currentFacingDir = Vector2.right;
        flipped = false;
        startingScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        MovementForce();
    }

    void MovementInput()
    {
        float horiInput = Input.GetAxisRaw("Horizontal") * canMove;
        moveForce.x += horiInput * moveSpeed * rb.mass * Time.deltaTime;
        animator.SetBool("isMoving", horiInput != 0);
    }

    void MovementForce()
    {
        if (moveForce != Vector2.zero)
        {
            if (rb.velocity.magnitude < maxMoveForce)
            {
                rb.AddForce(moveForce);
            }

            SetDirection(moveForce);

            moveForce = Vector2.zero;
        }
        else
        {
            rb.velocity *= stopDampenRatio;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        Vector2 newDir = direction;
        newDir.y = 0;
        newDir.Normalize();
        if (Vector2.Dot(newDir, currentFacingDir) < 0f)
        {
            // moving opposite way
            facingDirTime -= Time.deltaTime;

            if (facingDirTime < 0)
            {
                StartCoroutine(FlipXCoroutine(flipXTime));
                currentFacingDir = newDir;
                facingDirTime = flipXTime;
            }
        }
        else
        {
            facingDirTime = flipXTime;
        }
    }

    IEnumerator FlipXCoroutine(float seconds)
    {
        float totalTime = seconds;
        float t = 0;
        float startScale = 1;
        float endScale = 1;
        if (flipped)
        {
            startScale = -startingScale;
            endScale = startingScale;
        }
        else
        {
            startScale = startingScale;
            endScale = -startingScale;
        }

        while (t < totalTime)
        {
            float lerp = Mathf.Lerp(startScale, endScale, t / totalTime);
            if (lerp <= 0.001f && lerp >= -0.001f) lerp = 0.001f;
            transform.localScale = new Vector3(lerp, transform.localScale.y, transform.localScale.z);
            t += Time.deltaTime;
            if (t > totalTime) t = totalTime;
            yield return null;
        }

        transform.localScale = new Vector3(endScale, transform.localScale.y, transform.localScale.z);
        flipped = !flipped;
    }

    public void DisableMovement()
    {
        canMove = 0;
        rb.gravityScale = 0;
    }

    public void EnableMovement()
    {
        canMove = 1;
        rb.gravityScale = 1;
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        mTempPosition = transform.position;
        mTempRotation = transform.rotation;

        transform.position = position;
        transform.rotation = rotation;
    }

    public void ResetPositionAndRotation()
    {
        SetPositionAndRotation(mTempPosition, mTempRotation);
    }
}