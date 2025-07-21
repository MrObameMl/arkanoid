using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float JumpForce;
    public Platform Platform;

    private bool _ballOnPlatform = true;
    private Rigidbody2D _rigidbody;
    private Vector3 _reflectedDirection;
    private Vector3 _startBallPosition;
    private Vector3 _startPlatformPosition;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _startBallPosition = transform.position;

        _startPlatformPosition = Platform.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReturnBallAndPlatformToStartPosion(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
        ChangeColorCollisionObjects(collision);
        FixedBallDirection();
        StartCoroutine(FixedBallDirectionDelay(collision));
    }

    private void Update()
    {
        LaunchBall();
    }

    private void FixedUpdate()
    {
        _reflectedDirection = _rigidbody.velocity;
    }

    private void FixedBallDirection()
    {
        float minYvelocity = 1;
        Vector3 ballYvelocity = _rigidbody.velocity;
        if (Mathf.Abs(ballYvelocity.y) < minYvelocity)
        {
            ballYvelocity.y = ballYvelocity.y > 0 ? minYvelocity : -minYvelocity;
            _rigidbody.velocity = ballYvelocity.normalized * JumpForce;
        }

        float minXvelocity = 1;
        Vector3 ballXvelocity = _rigidbody.velocity;
        if (Mathf.Abs(ballXvelocity.x) < minXvelocity)
        {
            ballXvelocity.x = ballXvelocity.x > 0 ? minXvelocity : -minXvelocity;
            _rigidbody.velocity = ballXvelocity.normalized * JumpForce;
        }

    }

    private void ReturnBallAndPlatformToStartPosion(Collider2D collision)
    {
        if (collision.CompareTag("RespawnBorder"))
        {
            transform.position = _startBallPosition;
            Platform.transform.position = _startPlatformPosition;
            _rigidbody.velocity = Vector2.zero;
            _ballOnPlatform = true;
        }        
    }

    private void LaunchBall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _ballOnPlatform)
        {
            //_rigidbody.velocity = (Vector2.up + new Vector2(Random.Range(-1, 1), 0)).normalized * JumpForce;
            _rigidbody.velocity = Vector2.up * JumpForce;
            _ballOnPlatform = false;
        }

        if (_ballOnPlatform)
        {
            transform.position = new Vector2(Platform.transform.position.x, transform.position.y);
        }
    }

    private void HandleCollision(Collision2D collision)
    {
        Vector2 collisionPoint = collision.contacts[0].normal;
        _rigidbody.velocity = Vector2.Reflect(_reflectedDirection, collisionPoint).normalized * JumpForce;
    }

    private void ChangeColorCollisionObjects(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            SpriteRenderer borderRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            borderRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    private IEnumerator FixedBallDirectionDelay(Collision2D collision)
    {
        Vector3 savedDirection = _reflectedDirection;
        yield return new WaitForSeconds(0.5f);
        if (_rigidbody.velocity == Vector2.zero)
        {
            _rigidbody.velocity = savedDirection.normalized * JumpForce;
        }
    }
}

/* 
 логические союзы - это инструменты которые позволяют добавить несколько условий и сделать их проверку. Главная особенность заключается в том, что при использовании логического союза и все условия должны быть true. А при использования логического союза или одно тз условий должно быть true.  
и - &&
или - ||
 */
