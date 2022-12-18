using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 500;
    [SerializeField] float _maxDragDistance=3.5f;

    Vector2 _startPosition;
    Rigidbody2D _rigidBody2D;
    SpriteRenderer _spriteRenderer;

    void Awake(){
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start(){
        _startPosition = _rigidBody2D.position;
        _rigidBody2D.isKinematic = true;
    }

    void OnMouseDown(){
        _spriteRenderer.color = Color.red;
    }

    void OnMouseUp(){
        Vector2 currentPosition = _rigidBody2D.position;
        Vector2 direction = _startPosition - currentPosition; 
        direction.Normalize();

        _rigidBody2D.isKinematic = false;
        _rigidBody2D.AddForce(direction * _launchForce);

        _spriteRenderer.color = Color.white;
    }

    void OnMouseDrag(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desirePosition = mousePosition;
        
        float distance = Vector2.Distance(desirePosition,_startPosition);
        if(distance > _maxDragDistance){
            Vector2 direction = desirePosition - _startPosition;
            direction.Normalize();
            desirePosition = _startPosition + (direction * _maxDragDistance);
        }

        if(desirePosition.x > _startPosition.x) desirePosition.x =_startPosition.x;

        _rigidBody2D.position = desirePosition;
    }

    // Update is called once per frame
    void Update(){
         
    }

    void OnCollisionEnter2D(Collision2D collision){
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay(){
        yield return new WaitForSeconds(3);
        _rigidBody2D.position = _startPosition;
        _rigidBody2D.isKinematic = true;
        _rigidBody2D.velocity = Vector2.zero;
    }
}
