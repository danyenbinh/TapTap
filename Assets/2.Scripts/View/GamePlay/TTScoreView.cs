using TMPro;
using System;
using UnityEngine;

namespace TapTap
{
    public class TTScoreView : MonoBehaviour
    {
        private TextMeshProUGUI txtScore;
        private void Start()
        {
            txtScore = GetComponent<TextMeshProUGUI>();
            txtScore.text = "Score: 0";
            TTGamePlayControl.Api.onScoreChange += UpdateScore;
        }

        private void UpdateScore(int point)
        {
            txtScore.text = string.Format($"Score: {point}");
        }    

        private void OnDestroy()
        {
           TTGamePlayControl.Api.onScoreChange -= UpdateScore;
        }
    }
}

