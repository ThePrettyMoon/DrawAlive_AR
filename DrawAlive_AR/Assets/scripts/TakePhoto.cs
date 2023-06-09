using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;

public class TakePhoto : MonoBehaviour
{
    // ���յ����
    public Camera camera;


    private GameObject Guiding_text;
    private GameObject Canv;
    private GameObject LoadingPanel;
    private GameObject button;

    public string serverURL = "http://116.205.138.173:9856/upload"; // �������ϴ�·�ɵ�URL
    // ����ͼƬ���ļ���·��
    public string saveFolder;
    public string imagePath;

    Material material;

    public Slider slider;

    public static Texture2D downloadedTexture;

    public static Texture2D I_texture;

    private void Start()
    {
        Guiding_text = GameObject.Find("Shooting_guidance_text");
        Canv = GameObject.Find("Canvas");
        LoadingPanel = Canv.transform.Find("Panel").gameObject;
        button = GameObject.Find("Button");
    }

    // ���շ���
    public void TakePicture()
    {
        // ���洢Ȩ��
        if (!HasWritePermission())
        {
            // ����洢Ȩ��
            RequestWritePermission();
        }
        else
        {
            // ���д洢Ȩ�ޣ�ִ�����ղ���
            CaptureScreenshot();
        }
    }

    // ���洢Ȩ��
    private bool HasWritePermission()
    {
        return UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
    }

    // ����洢Ȩ��
    private void RequestWritePermission()
    {
        UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
    }

    // ���ղ�������Ƭ
    private void CaptureScreenshot()
    {
        // ����Texture2D����
        Destroy(Guiding_text);
        Destroy(button);
        


        // ��ȡ���������
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        camera.targetTexture = rt;

        // ��ȡ�������������
        I_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        I_texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // ���������ݱ���ΪpngͼƬ
        byte[] bytes = I_texture.EncodeToPNG();
        saveFolder = Application.persistentDataPath;
        string fileName = saveFolder + "/Target.png";
        imagePath = Path.Combine(saveFolder, fileName);
        System.IO.File.WriteAllBytes(fileName, bytes);

        //���Դ���
        //this.gameObject.GetComponent<SideLoadImageTarget>().enabled = true;
        //���Դ���

        //�ϴ���Ƭ
        StartCoroutine(UploadImage());

        //Destroy(texture);

        LoadingPanel.gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
    }

    public IEnumerator UploadImage()
    {

        // ��Texture2Dת��Ϊ�ֽ�����
        byte[] imageData = File.ReadAllBytes(imagePath);

        // ������
        WWWForm form = new WWWForm();
        form.AddField("image", "image.png");
        form.AddBinaryData("image", imageData, "image.png", "image/png");

        // ����POST����
        UnityWebRequest request = UnityWebRequest.Post("http://123.125.240.150:22671/", form);


        // ����DownloadHandlerTextureʵ�������ڴ������������
        DownloadHandlerTexture handler = new DownloadHandlerTexture();

        // ��DownloadHandlerTextureʵ������Ϊ��������ش������
        request.downloadHandler = handler;

        // �������󲢵ȴ���Ӧ
        yield return request.SendWebRequest();

        // �����Ӧ״̬
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // ��ȡ���ص�����
            downloadedTexture = handler.texture;

            // ������Ӧ�ó�����ʹ������
            // ...
            Debug.Log(BitConverter.ToString(downloadedTexture.GetRawTextureData()));

            //������������Ϊ��
            slider.value = 1.0f;
            Debug.Log("value:"+slider.value);

            //��SideLoadImage����
            //this.gameObject.transform.Find("sildeloadcontroller").gameObject.SetActive(true);
        }
    }
}
