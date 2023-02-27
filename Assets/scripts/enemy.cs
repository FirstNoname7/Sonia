using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    
    public PC1 PC1;
    public float speed;
    public float damage;
    //public Animator anime;
    public float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
      
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PC1.ChangeHP(damage);
        }
        
    }
}
