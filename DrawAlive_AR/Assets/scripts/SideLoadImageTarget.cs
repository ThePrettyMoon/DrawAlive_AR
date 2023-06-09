using UnityEngine;
using Vuforia;
public class SideLoadImageTarget : MonoBehaviour
{
    public Texture2D textureFile;
    public float printedTargetSize;
    public string targetName;

    //����3Dģ�͵�Ԥ����
    public GameObject Model_prefab;

    //��Ҫ����3Dmodel�������transform
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
            Debug.Log("�Ѿ���ʼ��������");

            textureFile = TakePhoto.I_texture;

            // Use Vuforia Application to invoke the function after Vuforia Engine is initialized
            VuforiaApplication.Instance.OnVuforiaStarted += CreateImageTargetFromSideloadedTexture;

            //��ȡʶ��ͼ��transform�����ڴ�������
            parentTransform = GameObject.Find("mytarget").gameObject.transform;

            Create3DModelPrefab();
        }
    }

    //��ʱ����һ��imageTarget����ʹ��ָ����ʶ��ͼƬ
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


    //����Ԥ����3Dmodel
    void Create3DModelPrefab()
    {
        if (Model_prefab != null)
        {
            if (parentTransform != null)
            {
                // �ڹ��ص������´���������
                GameObject newChild = Instantiate(Model_prefab, parentTransform);
                newChild.transform.localPosition = Vector3.zero;
                newChild.transform.localRotation = Quaternion.identity;
                newChild.transform.localScale = Vector3.one;


                //����Ԥ�����úõĴ�С������
                newChild.transform.localPosition = new Vector3(0.00034424f, 0.017091f, 0f);
                newChild.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }
        }
    }
}