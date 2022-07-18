using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TTHomeView : MonoBehaviour
{
    //reference
    private Button m_btnPlay;

    private void Start()
    {
        //find reference
        m_btnPlay = transform.Find("Container/Content/BtnPlay").GetComponent<Button>();

        //add listener
        m_btnPlay.onClick.AddListener(OnBtnPlayClick);
    }

    private void OnBtnPlayClick()
    {
        TTHomeControl.Api.LoadGamePlayScene();
    }

    private void OnDestroy()
    {
        m_btnPlay.onClick.RemoveAllListeners();
    }
}
