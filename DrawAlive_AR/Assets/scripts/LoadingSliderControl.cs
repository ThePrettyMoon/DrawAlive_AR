using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSliderControl : MonoBehaviour
{
    private Slider slider;
    private GameObject panel;

    private GameObject cam;
    private GameObject Image_target;

    // Start is called before the first frame update
    void Start()
    {
        //获取这个物体的slider组件
        slider = this.transform.GetComponent<Slider>();
        slider.value = 0;

        //获取panel
        panel = GameObject.Find("Panel");

        //获取imagetarget物体
        Image_target = GameObject.Find("ARCamera").gameObject.transform.Find("ImageTarget").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //进度条绘制
        if (slider.value < 0.99f)
        {
            slider.value += 1.0f/40.0f*Time.deltaTime;
        }
        if(slider.value>=1.0f)
        {
            Destroy(panel);
            Image_target.SetActive(true);
        }
    }

}
