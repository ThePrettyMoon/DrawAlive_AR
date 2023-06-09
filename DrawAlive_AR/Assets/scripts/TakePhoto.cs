using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using System;

public class TakePhoto : MonoBehaviour
{
    // 拍照的相机
    public Camera camera;


    private GameObject Guiding_text;
    private GameObject Canv;
    private GameObject LoadingPanel;
    private GameObject button;

    public string serverURL = "http://116.205.138.173:9856/upload"; // 服务器上传路由的URL
    // 保存图片的文件夹路径
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

    // 拍照方法
    public void TakePicture()
    {
        // 检查存储权限
        if (!HasWritePermission())
        {
            // 申请存储权限
            RequestWritePermission();
        }
        else
        {
            // 已有存储权限，执行拍照操作
            CaptureScreenshot();
        }
    }

    // 检查存储权限
    private bool HasWritePermission()
    {
        return UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
    }

    // 请求存储权限
    private void RequestWritePermission()
    {
        UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
    }

    // 拍照并保存照片
    private void CaptureScreenshot()
    {
        // 销毁Texture2D对象
        Destroy(Guiding_text);
        Destroy(button);
        


        // 获取相机的纹理
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        camera.targetTexture = rt;

        // 读取相机的像素数据
        I_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        I_texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 将像素数据保存为png图片
        byte[] bytes = I_texture.EncodeToPNG();
        saveFolder = Application.persistentDataPath;
        string fileName = saveFolder + "/Target.png";
        imagePath = Path.Combine(saveFolder, fileName);
        System.IO.File.WriteAllBytes(fileName, bytes);

        //测试代码
        //this.gameObject.GetComponent<SideLoadImageTarget>().enabled = true;
        //测试代码

        //上传照片
        StartCoroutine(UploadImage());

        //Destroy(texture);

        LoadingPanel.gameObject.SetActive(true);
        //this.gameObject.SetActive(false);
    }

    public IEnumerator UploadImage()
    {

        // 将Texture2D转换为字节数组
        byte[] imageData = File.ReadAllBytes(imagePath);

        // 创建表单
        WWWForm form = new WWWForm();
        form.AddField("image", "image.png");
        form.AddBinaryData("image", imageData, "image.png", "image/png");

        // 创建POST请求
        UnityWebRequest request = UnityWebRequest.Post("http://123.125.240.150:22671/", form);


        // 创建DownloadHandlerTexture实例，用于处理二进制数据
        DownloadHandlerTexture handler = new DownloadHandlerTexture();

        // 将DownloadHandlerTexture实例设置为请求的下载处理程序
        request.downloadHandler = handler;

        // 发送请求并等待响应
        yield return request.SendWebRequest();

        // 检查响应状态
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // 获取下载的纹理
            downloadedTexture = handler.texture;

            // 在您的应用程序中使用纹理
            // ...
            Debug.Log(BitConverter.ToString(downloadedTexture.GetRawTextureData()));

            //将进度条设置为满
            slider.value = 1.0f;
            Debug.Log("value:"+slider.value);

            //将SideLoadImage激活
            //this.gameObject.transform.Find("sildeloadcontroller").gameObject.SetActive(true);
        }
    }
}
