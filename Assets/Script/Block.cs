using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TapTap
{
    public class Block : MonoBehaviour
    {
        public bool CanBePress { get; private set; }

        private PathInfo _path;
        private float _speed;
        private bool _isMove = false;
        Tween tween1;
        Tween tween2;

        public void Setup(PathInfo path, float speed)
        {
            _path = path;
            _speed = speed;
            gameObject.SetActive(true);
            transform.DOScale(0.3f, 0f);
            transform.position = path.Begin.position;
            _isMove = true;
            tween1 = transform.DOMove(path.Target.position, 10 / _speed).SetEase(Ease.OutCubic);
            tween2 = transform.DOScale(1, 10 / _speed).OnComplete(() =>
            {
                GameManager.Instance.MissBlock();
                CanBePress = false;
                gameObject.SetActive(false);
            });
        }

        public void Update()
        {
            if(Input.GetKeyDown(_path.KeyCode) && CanBePress)
            {   
                tween1.Kill();
                tween2.Kill();
                CanBePress = false;
                GameManager.Instance.Score();
                transform.DOScale(1.3f, 0.1f).OnComplete(() => { gameObject.SetActive(false); });
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ButtonPress"))
                CanBePress = true;
            if (collision.CompareTag("Ground"))
                GameManager.Instance.MissBlock();
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("ButtonPress"))
                CanBePress = false;
        }
    }
}
