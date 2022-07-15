using TMPro;
using System;
using UnityEngine;
using DG.Tweening;

namespace TapTap
{
    public class UICrit : MonoBehaviour
    {
        private TextMeshProUGUI txtCrite;
        private int current;
        private void Start()
        {
            txtCrite = GetComponent<TextMeshProUGUI>();
            txtCrite.text = "X1";
            GameManager.Instance.OnCritChange += UpdateScore;
        }

        private void UpdateScore(object sender, EventArgs eventArgs)
        {
            int crit = (int)sender;
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
            GameManager.Instance.OnCritChange -= UpdateScore;
        }
    }
}

