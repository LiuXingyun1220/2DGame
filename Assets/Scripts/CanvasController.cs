using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Slider lenSlider;
    public Slider angleSlider;
    public Button[] indexBtn;
    public ParticleSystem[] indexPS;
    private ShuiBaData[] mShuiBaData;
    private int mSelectIndex;
    public Slider[] lenSliders;
    public Slider[] angleSliders;
    private float mProp;
    public Text mPropContent;
    public GameObject[] jingGaos;
    void Start()
    {
        mProp = 100;
        lenSlider.onValueChanged.AddListener((v)=> 
        {
            mShuiBaData[mSelectIndex].len = v;
            var main = indexPS[mSelectIndex].main;
            main.startSpeed =v;
            lenSliders[mSelectIndex].value = v;
            jingGaos[mSelectIndex].SetActive(v>5.9f);
        });
        angleSlider.onValueChanged.AddListener((v) =>
        {
            mShuiBaData[mSelectIndex].angle = v;
            indexPS[mSelectIndex].transform.localEulerAngles = Vector3.up * v;
            angleSliders[mSelectIndex].value = v;
        });
        mShuiBaData = new ShuiBaData[indexBtn.Length];
        for (int i = 0; i < mShuiBaData.Length; i++)
        {
            mShuiBaData[i] = new ShuiBaData();
        }
        SetSliderActive(false);
        for (int i = 0; i < indexBtn.Length; i++)
        {
            int index = i;
            indexBtn[index].onClick.AddListener(()=>
            {
                mSelectIndex = index;
                ShowSlider(mShuiBaData[index]);
            });
        }
    }


    private void Update()
    {
        float totle = 0;
        for (int i = 0; i < lenSliders.Length; i++)
        {
            totle += (lenSliders[i].value - 4);
        }
        mProp -= totle * Time.deltaTime * 0.1f;
        mProp = Mathf.Clamp(mProp,0,100);
        ShowProp((int)mProp);
    }
    private void ShowProp(int prop)
    {
        if (prop == 80) {
            Time.timeScale = 0;
            transform.Find("EndPanel").gameObject.SetActive(true) ;
        }
        mPropContent.text = $"内江:<color=#00A6FF>{  prop}%</color>       外江:<color=#00A6FF>{100-prop}%</color>";
    }

    private void ShowSlider(ShuiBaData data)
    {
        SetSliderActive(true);
        lenSlider.value = data.len;
        angleSlider.value = data.angle;
    }
    private void SetSliderActive(bool active) 
    {
        lenSlider.gameObject.SetActive(active);
        angleSlider.gameObject.SetActive(active);
    }

}
