using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;
    public float delayTime = 3.0f;
    public float fireSpeedX = -4.0f;
    public float fireSpeedY = 0.0f;
    public float length = 8.0f;

    GameObject player;
    GameObject gateObj;
    float passedTimes = 0;

    // Start is called before the first frame update
    void Start()
    {
        Transform tr = transform.Find("gate");
        gateObj = tr.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        passedTimes += Time.deltaTime;
        if (CheckLength(player.transform.position))
        {
            if(passedTimes > delayTime)
            {
                passedTimes = 0;
                Vector3 pos = new Vector3(gateObj.transform.position.x, gateObj.transform.position.y, transform.position.z);
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);
                rbody.AddForce(v, ForceMode2D.Impulse);
            }
        }
    }

    bool CheckLength(Vector2 targetPos)
    {
        bool ret = false;
        float d = Vector2.Distance(transform.position, targetPos);
        if(length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
