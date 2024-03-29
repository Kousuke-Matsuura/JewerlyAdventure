using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float times = 0.0f;
    public float weight = 0.0f;
    public bool isMoveWhenOn = false;

    public bool isCanMove = true;
    float perDX;
    float perDY;
    Vector3 defPos;
    bool isReverse = false;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        float timestep = Time.fixedDeltaTime;
        perDX = moveX / (1.0f / timestep * times);
        perDY = moveY / (1.0f / timestep * times);

        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            
            if (isReverse)
            {
                if((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    endX = true;
                }
                if((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true;
                }
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z)); //移動させるスクリプト
            }
            else
            {
                if((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;
                }
                if((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;
                }

                Vector3 v = new Vector3(perDX, perDY, defPos.z); // 移動させるスクリプト
                transform.Translate(v); //移動させるスクリプト
            }
            if(endX && endY)
            {
                if (isReverse)
                {
                    transform.position = defPos;
                }
                isReverse = !isReverse;
                isCanMove = false;
                if(isMoveWhenOn == false)
                {
                    Invoke("Move", weight);
                }
            }
        }
    }

    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) //床に対して何がぶつかっているのか？と読み替えるべし！
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
            //collision.transform.localScale = new Vector3(1, 1, 1);
            //collision.transform.localScale = Vector3.one; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)//何が床から離れたのか？を見るメソッド
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
