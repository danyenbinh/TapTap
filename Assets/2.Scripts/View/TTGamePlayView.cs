using System.Collections;
using System.Collections.Generic;
using TapTap;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TTGamePlayView : MonoBehaviour
{
    [SerializeField, Range(0f, 1024)] int frequency;
    [SerializeField, Range(0f, 0.001f)] float threshold;
    [SerializeField, Range(0f, 0.2f)] float time;
    [SerializeField, Range(0f, 2)] private float _speed = 1;

    private int Crit => (m_numberHit / 2) + 1 <= 5 ? (m_numberHit / 2) + 1 : 5;
    //Reference
    private Button m_btnBack;
    private TextMeshProUGUI m_txtPress;
    private TTPath m_path;
    private TTObjectPool m_objectPool;
    private AudioSource m_music;
    private Transform m_resultPanel;
    private TextMeshProUGUI m_txtResult;
    private bool m_isPlayGame = false;
    private float[] m_temp = new float[1024];
    private float m_timer;
    private float m_totalTime;
    private int m_point;
    private int m_numberHit;

    //Config
    private int m_pointPerHit;

    private void Start()
    {
        // Get reference
        m_btnBack = transform.Find("TopBar/BtnBack").GetComponent<Button>();
        m_txtPress = transform.Find("txtPress").GetComponent<TextMeshProUGUI>();
        m_path = transform.Find("Path").GetComponent<TTPath>();
        m_objectPool = transform.Find("ObjectPooling").GetComponent<TTObjectPool>();
        m_music = transform.Find("Audio Source").GetComponent<AudioSource>();
        m_resultPanel = transform.Find("ResultPanel").GetComponent<Transform>();
        m_txtResult = transform.Find("ResultPanel/txtResult").GetComponent<TextMeshProUGUI>();

        //Add listener
        m_btnBack.onClick.AddListener(OnBackBtnClick);
        TTGamePlayControl.Api.onMatchEvent += Score;
        TTGamePlayControl.Api.onMissBlock += MissBlock;

        m_pointPerHit = TTModel.Api.GamePlayConfig.GetGamePlayConfigDTO().PointPerHit;
    }

    private void OnBackBtnClick()
    {
        TTGamePlayControl.Api.LoadHomeScene();
    }

   

    private void Update()
    {
        if (Input.anyKeyDown && !m_isPlayGame)
        {
            m_music.Play();
            m_isPlayGame = true;
            m_txtPress.gameObject.SetActive(false);
        }
        m_timer -= Time.deltaTime;
        m_totalTime += Time.deltaTime;

        if(m_totalTime >= m_music.clip.length)
        {
            m_resultPanel.gameObject.SetActive(true);
            m_txtResult.text = "Your Score is: \n" + m_point.ToString();
        }

        if (m_isPlayGame && m_timer <= 0)
        {
            AudioListener.GetSpectrumData(m_temp, 0, FFTWindow.Rectangular);
            if (m_temp[frequency] > threshold)
            {
                Setup();
                m_timer = time;
            }
        }
    }

    public void Setup()
    {
        int i = UnityEngine.Random.Range((int)0, (int)5);
        TTBlock block = m_objectPool.GetPoolObject().GetComponent<TTBlock>();
        block.Setup(m_path.GetPath(i), _speed);
    }

    public void Score()
    {
        m_point += m_pointPerHit * Crit;
        TTGamePlayControl.Api.UpdateScore(m_point);

        m_numberHit++;
        if (m_numberHit % 2 == 0)
            TTGamePlayControl.Api.UpdateCrit(Crit);
    }

    public void MissBlock()
    {
        m_numberHit = 0;
        TTGamePlayControl.Api.UpdateCrit(Crit);
    }

    private void OnDestroy()
    {
        TTGamePlayControl.Api.onMatchEvent -= Score;
        TTGamePlayControl.Api.onMissBlock -= MissBlock;
        m_btnBack.onClick.RemoveListener(OnBackBtnClick);
    }
}
