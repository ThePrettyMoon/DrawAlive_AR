using UnityEngine;
using Vuforia;
public class SideLoadImageTarget : MonoBehaviour
{
    public Texture2D textureFile;
    public float printedTargetSize;
    public string targetName;

    //搭载3D模型的预制体
    public GameObject Model_prefab;

    //需要搭载3Dmodel的物体的transform
    public Transform parentTransform;

    private bool flag;

    void Start()
    {
        flag = false;

    }

    private void Update()
    {
        if(TakePhoto.I_texture!=null && flag==false)
        {
            flag=true;
            Debug.Log("已经开始创建物体");

            textureFile = TakePhoto.I_texture;

            // Use Vuforia Application to invoke the function after Vuforia Engine is initialized
            VuforiaApplication.Instance.OnVuforiaStarted += CreateImageTargetFromSideloadedTexture;

            //获取识别图的transform，用于创建物体
            parentTransform = GameObject.Find("mytarget").gameObject.transform;

            Create3DModelPrefab();
        }
    }

    //临时创建一个imageTarget对象，使用指定的识别图片
    void CreateImageTargetFromSideloadedTexture()
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            textureFile,
            printedTargetSize,
            targetName
            );
        // add the Default Observer Event Handler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();

        Debug.Log("Instant Image Target created " + mTarget.TargetName);
    }


    //引入预制体3Dmodel
    void Create3DModelPrefab()
    {
        if (Model_prefab != null)
        {
            if (parentTransform != null)
            {
                // 在挂载的物体下创建子物体
                GameObject newChild = Instantiate(Model_prefab, parentTransform);
                newChild.transform.localPosition = Vector3.zero;
                newChild.transform.localRotation = Quaternion.identity;
                newChild.transform.localScale = Vector3.one;


                //按照预先设置好的大小被创建
                newChild.transform.localPosition = new Vector3(0.00034424f, 0.017091f, 0f);
                newChild.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
    }
}