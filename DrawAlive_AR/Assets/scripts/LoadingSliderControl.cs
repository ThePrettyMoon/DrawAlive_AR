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
        //��ȡ��������slider���
        slider = this.transform.GetComponent<Slider>();
        slider.value = 0;

        //��ȡpanel
        panel = GameObject.Find("Panel");

        //��ȡimagetarget����
        Image_target = GameObject.Find("ARCamera").gameObject.transform.Find("ImageTarget").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //����������
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
