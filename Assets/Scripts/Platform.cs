using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();


    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movePosition = _rigidbody.position + Vector2.right * Speed * horizontal * Time.fixedDeltaTime;
        movePosition.x = Mathf.Clamp(movePosition.x, -23, 23);
        _rigidbody.MovePosition(movePosition);
        /* if (transform.position.x>=23)
         {
             transform.position = new Vector3(23, -13, 0);
         }
         if (transform.position.x<=-23)
         {
             transform.position = new Vector3(-23, -13, 0);
         }*/
        
    }

}
