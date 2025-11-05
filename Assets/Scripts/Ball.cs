using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public static event Action OnBallFalledDown;
    public float JumpForce;
    public Platform Platform;

    private bool _ballOnPlatform = true;
    private bool _launch = true;

    private Rigidbody2D _rigidbody;
    private Vector3 _reflectedDirection;
    private Vector3 _startBallPosition;
    private Vector3 _startPlatformPosition;    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _startBallPosition = transform.position;

        _startPlatformPosition = Platform.transform.position;

        UIManager.OnAttemptsEnded += StopBall;
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

        if (_rigidbody.linearVelocity.magnitude < JumpForce - 0.5f)
        {
            _rigidbody.linearVelocity = _reflectedDirection;
        }

        if (_rigidbody.IsSleeping() && !_ballOnPlatform && _launch)
        {
            StartCoroutine(StartBallRepeatly());
        }
    }

    private IEnumerator StartBallRepeatly()
    {
        _launch = false;
        yield return new WaitForSeconds(0.5f);        
        _rigidbody.linearVelocity = Random.insideUnitCircle * JumpForce;

        yield return new WaitForSeconds(0.5f);
        _launch = true;
    }

    private void FixedUpdate()
    {
        _reflectedDirection = _rigidbody.linearVelocity;
    }

    private void FixedBallDirection()
    {
        float minYvelocity = 1;
        Vector3 ballYvelocity = _rigidbody.linearVelocity;
        if (Mathf.Abs(ballYvelocity.y) < minYvelocity)
        {
            ballYvelocity.y = ballYvelocity.y > 0 ? minYvelocity : -minYvelocity;
            _rigidbody.linearVelocity = ballYvelocity.normalized * JumpForce;
        }

        float minXvelocity = 1;
        Vector3 ballXvelocity = _rigidbody.linearVelocity;
        if (Mathf.Abs(ballXvelocity.x) < minXvelocity)
        {
            ballXvelocity.x = ballXvelocity.x > 0 ? minXvelocity : -minXvelocity;
            _rigidbody.linearVelocity = ballXvelocity.normalized * JumpForce;
        }

    }

    private void ReturnBallAndPlatformToStartPosion(Collider2D collision)
    {
        if (collision.CompareTag("RespawnBorder"))
        {
            OnBallFalledDown?.Invoke();
            StartCoroutine(ReturnDelay());
            
            IEnumerator ReturnDelay()
            {
                yield return new WaitForSeconds(1f);
                _rigidbody.linearVelocity = Vector2.zero;
                _ballOnPlatform = true;
                transform.position = _startBallPosition;
                Platform.transform.position = _startPlatformPosition;
            }
        }        
    }

    private void LaunchBall()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _ballOnPlatform)
        {
            //_rigidbody.velocity = (Vector2.up + new Vector2(Random.Range(-1, 1), 0)).normalized * JumpForce;
            _rigidbody.linearVelocity = Vector2.up * JumpForce;
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
        _rigidbody.linearVelocity = Vector2.Reflect(_reflectedDirection, collisionPoint).normalized * JumpForce;
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
        if (_rigidbody.linearVelocity == Vector2.zero)
        {
            _rigidbody.linearVelocity = savedDirection.normalized * JumpForce;
        }
    }

    private void StopBall()
    {
        _rigidbody.linearVelocity = Vector2.zero;
        JumpForce = 0;
        transform.position = _startBallPosition;

    }

    private void OnDestroy()
    {
        UIManager.OnAttemptsEnded -= StopBall;
    }
}

/* 
 ���������� ����� - ��� ����������� ������� ��������� �������� ��������� ������� � ������� �� ��������. ������� ����������� ����������� � ���, ��� ��� ������������� ����������� ����� � ��� ������� ������ ���� true. � ��� ������������� ����������� ����� ��� ���� �� ������� ������ ���� true.  
� - &&
��� - ||
 */
