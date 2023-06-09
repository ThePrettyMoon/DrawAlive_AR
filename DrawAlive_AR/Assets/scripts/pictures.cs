using UnityEngine;

public class pictures : MonoBehaviour
{
    public Texture2D image; // 要使用的图片
    public float spacing = 0.001f; // 图片之间的间距
    public int numImages = 100; // 图片数量
    public Vector3 startPosition; // 开始位置
    public Vector3 endPosition; // 结束位置

    void Start()
    {
        //获取回传的图片
        image = TakePhoto.downloadedTexture;

        // 创建一百张图片
        for (int i = 0; i < numImages; i++)
        {
            // 创建平面对象
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.SetParent(transform);

            // 设置纹理
            //plane.GetComponent<Renderer>().material.mainTexture = image;
            Renderer renderer = plane.GetComponent<Renderer>();
            Material material = renderer.material;
            Shader shader = Shader.Find("Mobile/Particles/Alpha Blended");
            material.shader = shader;
            material.mainTexture = image;


            // 设置位置
            float t = (float)i / (numImages - 1);
            plane.transform.position = Vector3.Lerp(startPosition, endPosition, t) + Vector3.forward * i * spacing + this.gameObject.transform.position;

            // 设置旋转和缩放
            plane.transform.rotation = Quaternion.identity;
            plane.transform.localScale = Vector3.one * 0.1f; // 调整大小
            //旋转90度不然会直接变成全平面
            plane.transform.Rotate(90, 0, 0);
        }
    }
}
