using TMPro;
using System;
using UnityEngine;
using DG.Tweening;

namespace TapTap
{
    public class TTCritView : MonoBehaviour
    {
        private TextMeshProUGUI txtCrite;
        private int current;
        private void Start()
        {
            txtCrite = GetComponent<TextMeshProUGUI>();
            txtCrite.text = "X1";
            TTGamePlayControl.Api.onCritEvent += UpdateScore;
        }

        private void UpdateScore(int crit)
        {
            if (current == crit && current == 1)
                return;
            transform.DOScale(1.2f, 0.1f).OnComplete(() => {
                
                current = crit;
                txtCrite.text = string.Format($"X{crit}");
                transform.DOScale(1f, 0.05f);
            });           
        }    

        private void OnDestroy()
        {
            TTGamePlayControl.Api.onCritEvent -= UpdateScore;
        }
    }
}

