using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        Invoke("GoBall", 3); 
        
    }
    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15).normalized*speed); 
                                                 

        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15 ).normalized*speed);
        }
    }

    void ResetBall() 
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    [SerializeField] private int wallCollisionCount;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag( "Player")) 
        {
            rb2d.AddForce(new Vector2(20, -15).normalized*speed);
            wallCollisionCount = 0;

        }
        else if (coll.gameObject.name == "wall") 
        {
            rb2d.AddForce(new Vector2(-20, -15).normalized*speed);
            wallCollisionCount = 0;
        }
        else 
        {
            wallCollisionCount = wallCollisionCount + 1;
            Debug.Log("Wall Collision! = " + wallCollisionCount);
            if (wallCollisionCount > 6) GoBall();
        }
    }

}