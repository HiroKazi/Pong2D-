using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //mengambil rigidbody component dari sebuah bole
        Invoke("GoBall", 3); //memanggil function GoBall dlm 2 detik
        
    }
    void GoBall()
    {
        float rand = Random.Range(0, 2); //akan random nilai diantara 0-1
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15).normalized*speed); //add force memberikan tenaga
                                                 //liat doc add force disini https://docs.unity3d.com/ScriptReference/Rigidbody.AddForce.html

        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15 ).normalized*speed);
        }
    }

    void ResetBall() //ini kita buat nilai transform jadi 0
    {
        rb2d.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    void RestartGame()
    {
        //Debug.Log("Restart!");
        ResetBall();
        Invoke("GoBall", 1);
    }

    [SerializeField] private int wallCollisionCount;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag( "Player")) //jika terkena player
        {
            //Debug.Log("King Punch!");
            rb2d.AddForce(new Vector2(20, -15).normalized*speed);
            wallCollisionCount = 0;

        }
        else if (coll.gameObject.name == "wall") //jika terkena enemy
        {
            //Debug.Log("Pig Punch!");
            rb2d.AddForce(new Vector2(-20, -15).normalized*speed);
            wallCollisionCount = 0;
        }
        else //jika terkena wall
        {
            wallCollisionCount = wallCollisionCount + 1;
            Debug.Log("Wall Collision! = " + wallCollisionCount);
            if (wallCollisionCount > 6) GoBall();
        }
    }

}