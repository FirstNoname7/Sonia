using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private GameObject player;
    public Vector3 Z;
    
    void Start()
    {
       player = GameObject.Find("player");
      

    }

    
    void Update()
    {
        transform.position = Z+player.transform.position; 
        
    }
}
