using TMPro;
using System;
using UnityEngine;

namespace TapTap
{
    public class UIScore : MonoBehaviour
    {
        private TextMeshProUGUI txtScore;
        private void Start()
        {
            txtScore = GetComponent<TextMeshProUGUI>();
            txtScore.text = "Score: 0";
            GameManager.Instance.OnPointChange += UpdateScore;
        }

        private void UpdateScore(object sender, EventArgs eventArgs)
        {
            txtScore.text = string.Format($"Score: {GameManager.Instance.Point}");
        }    

        private void OnDestroy()
        {
            GameManager.Instance.OnPointChange -= UpdateScore;
        }
    }
}

