using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TTGeneralView : MonoBehaviour
{
    private GameObject m_loadingPrefab;//prefab loading
    private GameObject m_loadingBgPrefab;//prefab loading with background popup
    private GameObject m_popupMessagePrefab;//prefab popup message
    //
    private CanvasGroup m_loadingObj;//default object loading
    private Transform m_canvasObj;//object canvas

	void Start ()
    {
		Init();
	}

    public void Init()
    {
        // find reference
        m_canvasObj = transform.Find("Canvas");
        //
        m_loadingPrefab = ResourceObject.GetResource<GameObject>(TTConstant.CONFIG_PREFAB_LOADING);
        m_loadingBgPrefab = ResourceObject.GetResource<GameObject>(TTConstant.CONFIG_PREFAB_LOADING_BG);
        m_popupMessagePrefab = ResourceObject.GetResource<GameObject>(TTConstant.CONFIG_PREFAB_POPUP_MESSAGE);

        // register event
        TTControl.Api.ShowLoadingEvent += OnLoading; // register show hide loading
        TTControl.Api.ShowPopupEvent += ShowPopup; // register show popup message
    }

    void OnDestroy()
    {
        // unregister event
        TTControl.Api.ShowLoadingEvent -= OnLoading;
        TTControl.Api.ShowPopupEvent -= ShowPopup;
    }

    // handle action show hide loading
    private void OnLoading(bool isShow, TTLoadingType type = TTLoadingType.Default)
    {
        if (isShow)
        {
            if(m_loadingObj != null)
                return;

            m_loadingObj = Instantiate(type == TTLoadingType.Default?m_loadingPrefab : m_loadingBgPrefab, m_canvasObj).GetComponent<CanvasGroup>();
            m_loadingObj.DOFade(0f,0.3f).From();
        }
        else
        {
            if(m_loadingObj != null){
                m_loadingObj.DOFade(0f,0.3f)
                .SetDelay(0.3f)
                .OnComplete(()=>Destroy(m_loadingObj.gameObject));
            }
        }
    }

    private void ShowPopup(TTPopupIconType iconType, string content, bool autoTurnOff, Action onClose)
    {
        GameObject go = Instantiate(m_popupMessagePrefab,m_canvasObj);
        go.GetComponent<TTPopupMessageView>().Init(iconType,content,autoTurnOff,onClose);
    }
}
