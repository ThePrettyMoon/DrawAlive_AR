using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float touchTime;
    private bool newTouch = false;
    private float rotate_speed;

    Vector2 old_pos_1;
    Vector2 old_pos_2;


    // Start is called before the first frame update
    void Start()
    {
        rotate_speed = 150.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {

            //滑动旋转,不需要触碰到物体
            if (Input.touchCount == 1)
            {
                //滑动
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -rotate_speed * Time.deltaTime, Space.World);
                }
            }


            if (Input.touchCount == 2)
            {
                //两个手指中有一个是移动的就可以
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    //获取两个手指的位置
                    Vector2 temp_pos_1 = Input.GetTouch(0).position;
                    Vector2 temp_pos_2 = Input.GetTouch(1).position;

                    if (isEnlarge(old_pos_1, old_pos_2, temp_pos_1, temp_pos_2))
                    {
                        float old_scale = transform.localScale.x;
                        float new_scale = old_scale * 1.025f;
                        transform.localScale = new Vector3(new_scale, new_scale, new_scale);
                    }
                    else
                    {
                        float old_scale = transform.localScale.x;
                        float new_scale = old_scale / 1.025f;
                        transform.localScale = new Vector3(new_scale, new_scale, new_scale);
                    }

                    //保存目前的位置到old_pos
                    old_pos_1 = temp_pos_1;
                    old_pos_2 = temp_pos_2;
                }
            }
        }

        
    }


    //判断是否是放大的函数
    bool isEnlarge(Vector2 old_position_1, Vector2 old_position_2, Vector2 new_position_1, Vector2 new_position_2)
    {
        float length1 = Mathf.Sqrt((old_position_1.x - old_position_2.x) * (old_position_1.x - old_position_2.x) + (old_position_1.y - old_position_2.y) * (old_position_1.y - old_position_2.y));
        float length2 = Mathf.Sqrt((new_position_1.x - new_position_2.x) * (new_position_1.x - new_position_2.x) + (new_position_1.y - new_position_2.y) * (new_position_1.y - new_position_2.y));
        if (length1 < length2)
            return true;
        else
            return false;
    }
}
