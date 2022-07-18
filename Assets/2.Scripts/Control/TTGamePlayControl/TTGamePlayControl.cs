using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTGamePlayControl
{
    #region API
    private static TTGamePlayControl api;
    public static TTGamePlayControl Api
    {
        get { return api; }
        set { api = value; }
    }
    #endregion 

    public Action<int> onScoreChange; 

    public Action onMatchEvent;

    public Action<int> onCritEvent;

    public Action onMissBlock;

    public void LoadHomeScene()
    {
        SceneManager.UnloadSceneAsync(TTConstant.SCENE_GAMEPLAY);
        SceneManager.LoadScene(TTConstant.SCENE_HOME, LoadSceneMode.Additive);
    }

    public void UpdateScore(int score)
    {
        onScoreChange?.Invoke(score);
    }

    public void UpdateCrit(int crit)
    {
        onCritEvent?.Invoke(crit);
    }

    public void GetPoint()
    {
        onMatchEvent?.Invoke();
    }

    public void MissBlock()
    {
        onMissBlock?.Invoke();
    }
}
