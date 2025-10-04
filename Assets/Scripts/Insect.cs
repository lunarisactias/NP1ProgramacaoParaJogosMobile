using UnityEngine;
using System.Collections;

public class Insect : MonoBehaviour
{
    [Header("Insect Move Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float zigzagAmplitude = 2f;
    [SerializeField] private float zigzagFrequency = 2f;

    [Header("Insect Target Settings")]
    [SerializeField] private GameObject[] targetPositions;
    private GameObject targetPosition;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Start()
    {
        ChooseTarget();
        zigzagAmplitude = Random.Range(3f, 6f);
        zigzagFrequency = Random.Range(3f, 6f);
    }
    private void Update()
    {
        Move();
        FlipSprite();

        if (Vector2.Distance(transform.position, targetPosition.transform.position) < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        if (targetPosition != null)
        {
            //Movimento e Rotação
            Vector2 direction = (targetPosition.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //Zigzag
            float sidewaysOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
            Vector2 forward = transform.up;
            Vector2 right = transform.right;
            Vector2 zigzagDirection = forward * speed + right * sidewaysOffset;
            rb.linearVelocity = zigzagDirection;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void ChooseTarget()
    {
        if (targetPositions.Length > 0)
        {
            int randomIndex = Random.Range(0, targetPositions.Length);
            targetPosition = targetPositions[randomIndex];
        }
    }

    public void FlipSprite()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (transform.position.x < targetPosition.transform.position.x)
        {
            sprite.flipX = false;
        }
        else if (transform.position.x > targetPosition.transform.position.x)
        {
            sprite.flipX = true;
        }
    }
}
