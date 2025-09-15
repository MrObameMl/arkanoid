using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    public Sprite []BlockSprites;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private SpriteRenderer _renderer;
    private int _blockHP = 3;
    private int _cubeIndex;
    private int _randomIndex;
    [SerializeField, Range(10, 100)]private float _rotationSpeed;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        _cubeIndex = transform.GetSiblingIndex();
        _cubeIndex = transform.GetSiblingIndex(); 
        
        _randomIndex = Random.Range(0, 3);
        if (_cubeIndex % 2 == 0)
        {
            _rotationSpeed *= 1;
        }
        else
        {
            _rotationSpeed *= -1;
        }        
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.gameObject.TryGetComponent<Ball>(out var ball))
        {           
            switch (_randomIndex)
            {
                case 0:
                    Behaviour_1();
                    break;
                case 1:
                    Behaviour_2();
                    break;
                case 2:
                    Behaviour_3();
                    break;
                default:
                    Debug.LogWarning("Behaviours not found, attention!!!");
                    break;
            }
        }
   }

    private void Update()
    {
       // transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));  
    }

    public void Behaviour_1()
    {
        //����� ����� � ������� 

        _renderer.color = new Color(Random.value, Random.value, Random.value);
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.gravityScale = 1;
        _collider.isTrigger = true;
        StartCoroutine(DestroyBlock());
        IEnumerator DestroyBlock()
        {
            float destroyBorderPosition = -17.14f;
            while (transform.position.y > destroyBorderPosition )
            {
                yield return null;
            }
            Destroy(gameObject);
        }
    }
    public void Behaviour_2()
    {
        //���������� �����

        float reduceSpeed = 5f;
        float scaleMultiplier = 0.3f;
        Vector3 targetScale = Vector3.one * scaleMultiplier;
        StartCoroutine(ReduceBlock());
        IEnumerator ReduceBlock()
        {
            while (Vector3.Distance(transform.localScale, targetScale) > 0.1f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, reduceSpeed * Time.deltaTime);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
    public void Behaviour_3()
    { 
        Vector3 midSize = Vector3.one * 0.7f;
        Vector3 minSize = Vector3.one * 0.3f;

         IEnumerator ReduceBlock(Vector3 targetScale, float reduceSpeed)
         {
            Vector3 objectScale = transform.localScale;
            while (Vector3.Distance(objectScale, targetScale) > 0.1f)
            {
                _collider.isTrigger = true;
                objectScale = Vector3.Lerp(objectScale, targetScale, reduceSpeed * Time.deltaTime);
                transform.localScale = objectScale;
                yield return null;
            }
            objectScale = targetScale;
            _collider.isTrigger = false;
        }

        _blockHP--;

        if (_blockHP == 2)
        {
            StartCoroutine(ReduceBlock( midSize, 1f));
        }
        else if (_blockHP == 1)
        {
            StartCoroutine(ReduceBlock(minSize, 1f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Behaviour_4()
    {
        _blockHP--;
        _renderer.sprite = BlockSprites[_blockHP];
        if (_blockHP == 0)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.gravityScale = 1;
            _collider.isTrigger = true;
            StartCoroutine(DestroyBlock());
            IEnumerator DestroyBlock()
            {
                float destroyBorderPosition = -17.14f;
                while (transform.position.y > destroyBorderPosition)
                {
                    yield return null;
                }
                Destroy(gameObject);
            }
        }
    }
}

/*
 * ����� ��� ����������� ������� ��������� ���������(���������) ������������ ���� ���� ����������� ����������� ��� ��� �� ���������� ����� ������� ������ ���������
 * while(true)
 * {
 * 
 * }
 * � ������� ������� ������������ ������� ������ �����.� �������� ������� ������������ ��������, ������� ������ �����������.�������� ���������� ������ � ��� ������ ���� ������� ��������. 
 * 
 * for(int i = 0; i < Length; i++)
 * {
 *  
 * }
 * ���� for ������������ � ��� ������, ����� �������� �������� ����������� ���������� ����.
 * int i = 0 ��� ������ �������
 * i < Length ��� ��������, ��� Lentgh ��� �������� ��������
 * i++ ��� ��� ���������� �������� �� �������
 * 
 * foreach(var item in collection)
 * {
 * 
 * }
 * ���� foreach ������������ ��� ��������(����������) ��������� (��������) ������ ���������(�������, �����, �.�.)
 * ��������� ��� ������ ������ ������ ����(�� ��������), ��������� � ����� �����.
 * var - ��� ����������� �����, ������� �������� ����������� � �������� ���� ������ ����� ����������.
 * item - ��� �������� ����������, ������� ������ �������� ��������� �������(������) ������ ���������.
 * in - ��� ����������� �����, ������� �������� ��������� � ���������� �����.
 */
