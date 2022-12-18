using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlController : MonoBehaviour
{   
    [SerializeField] string _nextLevelName;

    Monster[] _monsters; 

    void OnEnable(){
        _monsters = FindObjectsOfType<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MonsterAllDead()){
            GoToNextLvl();
        }
    }

    bool MonsterAllDead(){
        foreach(var monster in _monsters){
            if(monster.gameObject.activeSelf){
                return false;
            }
        }
        return true;
    }

    void GoToNextLvl(){
        Debug.Log("Go to lvl" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

}
