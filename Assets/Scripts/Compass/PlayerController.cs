using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制角色移动、
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");//控制水平移动方向 A;-1 D:1  0
        float moveY = Input.GetAxisRaw("Vertical");//垂直方向 w：1  S：-1  0

        Vector2 position = transform.position;
        position.x += moveX * speed * Time.deltaTime;
        position.y += moveY * speed * Time.deltaTime;
        transform.position = position;
     
    }
}
