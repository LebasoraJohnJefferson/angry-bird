using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{

    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    bool _hasDied = false; 

    void OnCollisionEnter2D(Collision2D collision){

        if(_hasDied) return;


        if(ShouldDieFromCollision(collision)){
            StartCoroutine(Die());
        }
    }

    bool ShouldDieFromCollision(Collision2D collision){
        Bird bird = collision.gameObject.GetComponent<Bird>();
 
        return collision.contacts[0].normal.y < -0.5 || bird != null ?  true : false;
    }

    IEnumerator Die(){
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false); 
    }
}
