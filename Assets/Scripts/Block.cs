using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField, Range(10, 100)] private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _renderer;
    private int _blockHP = 3;
    private int _cubeIndex;
    private int _randomIndex;


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
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
            switch (LevelManager.ReturnBehaviourIndex())
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
        //смена цвета и падения 

        _renderer.color = new Color(Random.value, Random.value, Random.value);
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
    public void Behaviour_2()
    {
        //уменьшение блока

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
            StartCoroutine(ReduceBlock(midSize, 1f));
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
}

/*
 * Цыклы это конструкции которые позволяют выполнять(повторять) определенный блок кода определённое колличество раз или на протяжении всего времени работы программы
 * while(true)
 * {
 * 
 * }
 * в круглых скобках записывается условие работы цикла.В фигурных скобках записывается действие, которое должно выполниться.Действие выполнится только в том случае если условие правдиво. 
 * 
 * for(int i = 0; i < Length; i++)
 * {
 *  
 * }
 * Цикл for используется в том случае, когда зараниее известно колличество повторений кода.
 * int i = 0 это начало отсчета
 * i < Length это диапазон, где Lentgh это конечное значение
 * i++ это шаг увеличения значения на единицу
 * 
 * foreach(var item in collection)
 * {
 * 
 * }
 * цикл foreach используется для перебора(сортировки) элементов (объектов) внутри коллекции(массивы, листы, т.п.)
 * Коллекции это наборы данных одного типа(не человека), собранных в одном месте.
 * var - это специальное слово, которое заменяет потребность в указании типа данных перед переменной.
 * item - это название переменной, которое должно отражать отдельный элемент(объект) внутри коллекции.
 * in - это специальное слово, которое означает указатель в конкретное место.
 */
