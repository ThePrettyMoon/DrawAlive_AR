using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControl : MonoBehaviour
{
    private float touchTime;
    private bool newTouch=false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //当触碰到物体
            if(Physics.Raycast(ray, out hitInfo))
            {
                //双击交互事件
                //touchCount代表多少个手指点击屏幕
                //touchphase.began表示新的触摸
                if(Input.touchCount==1 && Input.GetTouch(0).phase==TouchPhase.Began)
                {
                    if (Input.GetTouch(0).tapCount == 2)
                        Destroy(hitInfo.collider.gameObject);
                }

                //长按的事件
                if (Input.touchCount == 1)
                {
                    Touch touch = Input.GetTouch(0);

                    //如果是新按下的
                    if (touch.phase == TouchPhase.Began)
                    {
                        newTouch = true;
                        touchTime = Time.time;
                    }
                    //如果是按住没有动
                    else if (touch.phase == TouchPhase.Stationary)
                    {
                        if (newTouch == true && Time.time - touchTime > 1)
                        {
                            newTouch = false;
                            Destroy(hitInfo.collider.gameObject);
                        }
                    }
                    else
                    {
                        newTouch = false;
                    }
                }

                
            }

        }
    }



}
