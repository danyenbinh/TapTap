using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace TapTap
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] GameObject txtPress;
        [SerializeField] private Path path;
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private AudioSource music;
        [SerializeField] private int pointPerHit = 100;
        [SerializeField, Range(0f, 1024)] int frequency;
        [SerializeField, Range(0f, 0.001f)] float threshold;
        [SerializeField] FFTWindow fFT;
        [SerializeField, Range(0f, 0.2f)] float time;

        [SerializeField, Range(0f, 2)] private float _speed = 1;
        private bool _isPlayGame = false;
        private float[] temp = new float[1024];
        private float _timer;
        public EventHandler OnPointChange;
        public EventHandler OnCritChange;

        public int Point { get; private set; }
        public int Crit => (_numberHit / 2) + 1 <=5 ? (_numberHit / 2) + 1 : 5;
        private int _numberHit;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.anyKeyDown && !_isPlayGame)
            {
                music.Play();
                _isPlayGame = true;
                txtPress.SetActive(false);
            }
            _timer -= Time.deltaTime;
            if(_isPlayGame && _timer <= 0)
            {
                AudioListener.GetSpectrumData(temp, 0, fFT);
                if(temp[frequency] > threshold)
                {
                    Setup();
                    _timer = time;
                }
            }
        }

        public void Setup()
        {
            int i = UnityEngine.Random.Range((int)0, (int)5);
            Block block = objectPool.GetPoolObject().GetComponent<Block>();
            block.Setup(path.GetPath(i), _speed);
        }

        public void Score()
        {
            Point += pointPerHit * Crit;
            OnPointChange?.Invoke(this, EventArgs.Empty);
            
            _numberHit++;
            if (_numberHit % 2 == 0)
                OnCritChange?.Invoke(Crit, EventArgs.Empty);
        }

        public void MissBlock()
        {
            _numberHit = 0;
            OnCritChange?.Invoke(Crit, EventArgs.Empty);
        }
    }
}

