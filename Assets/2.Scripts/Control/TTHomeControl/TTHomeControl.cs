using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTHomeControl
{
    #region API
    private static TTHomeControl api;
    public static TTHomeControl Api
    {
        get { return api; }
        set { api = value; }
    }
    #endregion

    public void LoadGamePlayScene()
    {
        SceneManager.UnloadSceneAsync(TTConstant.SCENE_HOME);
        SceneManager.LoadScene(TTConstant.SCENE_GAMEPLAY, LoadSceneMode.Additive);
    }

}
