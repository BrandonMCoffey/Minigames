using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts {
    public class GameController : MonoBehaviour {
        public static GameController Instance;

        [SerializeField] private CarController _carController = null;
        [SerializeField] private ButtonController _levelButtons = null;
        [SerializeField] private TextMeshProUGUI _timerText = null;
        [SerializeField] private List<Level> _levels = new List<Level>();

        private GameObject _loadedLevel;
        private int _currentLevel;
        private int _currentScore;
        private int _totalCollectibles;
        private bool _hasStartedLevel;
        private float _timer;

        private void Awake()
        {
            // Singleton instance
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (_levelButtons == null) {
                Debug.Log("Error: No Text Buttons attached to Game Controller");
                return;
            }
            int levelNumber = 1;
            foreach (var key in _levels.Select(level => level.LevelTitle)) {
                if (!PlayerPrefs.HasKey(key)) {
                    PlayerPrefs.SetInt(key, 0);
                }
                _levelButtons.AddLevel(levelNumber, key);
                levelNumber++;
            }
            _levelButtons.UpdateScores();
            _levelButtons.DestroyTemplate();
            SelectLevel(0);
#if UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif
        }

        private void Update()
        {
            if (!_hasStartedLevel) return;
            _timer += Time.deltaTime;
            if (_timerText != null) {
                _timerText.text = Mathf.Floor(_timer / 60).ToString("00") + ":" + Mathf.Floor(_timer % 60).ToString("00");
            }
        }

        public void StartLevel()
        {
            _hasStartedLevel = true;
        }

        public void GatherCollectible(int amount)
        {
            _totalCollectibles -= amount;
            if (_totalCollectibles == 0) {
                _currentScore++;
            }
        }

        public void WinLevel()
        {
            if (!_hasStartedLevel) return;
            _hasStartedLevel = false;
            if (PlayerPrefs.GetInt(_levels[_currentLevel].LevelTitle) < _currentScore) {
                PlayerPrefs.SetInt(_levels[_currentLevel].LevelTitle, _currentScore);
            }
            _levelButtons.UpdateScores();
            StartCoroutine(WinEffects());
        }

        public IEnumerator WinEffects()
        {
            yield return new WaitForSeconds(2);
            SelectLevel(_currentLevel + 1);
        }

        public void SelectLevel(int levelNumber)
        {
            if (levelNumber < 0 || levelNumber >= _levels.Count) {
                Debug.Log("Error: Level number " + levelNumber + " is not valid");
                return;
            }
            _currentLevel = levelNumber;
            _levelButtons.SelectLevel(levelNumber);
            _totalCollectibles = _levels[levelNumber].GetCollectibleAmount();
            _currentScore = 1;
            _timer = 0;
            if (_loadedLevel != null) {
                Destroy(_loadedLevel);
            }
            _loadedLevel = Instantiate(_levels[_currentLevel].gameObject);
            _hasStartedLevel = false;
            if (_carController != null) {
                _carController.Reload(_levels[_currentLevel].CarStartingPosition);
            } else {
                Debug.Log("Warning: No Car Controller Connected");
            }
        }
    }
}