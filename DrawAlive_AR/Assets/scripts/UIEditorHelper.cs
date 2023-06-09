using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI �ı༭�������࣬���ڸ��ֵ��Զ����༭��
/// </summary>
public class UIEditorHelper : MonoBehaviour
{
    /// <summary>
    /// UIĬ�ϵĲ��ʣ�
    /// </summary>
    [ContextMenuItem("�滻UI������", "SetDefautMat")]
    public Material UIDefaultMat;

    /// <summary>
    /// ���ó�Ĭ�ϲ��ʣ�
    /// </summary>
    [ExecuteInEditMode]
    public void SetDefautMat()
    {
        if (UIDefaultMat == null)
        {
            Debug.LogError("ѡ���Ĭ�ϲ�����Ϊ�գ�");
            return;
        }
        //��ȡ���е�ͼƬ���ı���
        Image[] mArrImg = transform.GetComponentsInChildren<Image>();
        Text[] mArrText = transform.GetComponentsInChildren<Text>();
        Debug.Log("ͼƬ������" + mArrImg.Length);
        Debug.Log("�ı�������" + mArrText.Length);
        //�����������û�в�����ʹ�õ�Ĭ�ϲ����򣩣���ô�滻������ѡ���Ĳ�����
        for (int i = 0; i < mArrImg.Length; i++)
        {
            var img = mArrImg[i];
            if (img.material == img.defaultMaterial)
            {
                //Mask�������������������������
                if (img.transform.GetComponent<Mask>() == null)
                    img.material = UIDefaultMat;
            }
        }
        for (int i = 0; i < mArrText.Length; i++)
        {
            var text = mArrText[i];
            if (text.material == text.defaultMaterial)
            {
                text.material = UIDefaultMat;
            }
        }
        Debug.Log("�滻��ɣ�");
    }

}