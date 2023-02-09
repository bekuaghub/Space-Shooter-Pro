using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour
{
    private TextMeshProUGUI _scoreTextBox;
    private TextMeshProUGUI _timeTextBox;
    [SerializeField] private TextMeshProUGUI _gameOverTextbox;
    private Slider _slider;
    [SerializeField] bool _timerActive = true;
    private float nextTime, timerFreq = 1.0f;

    void Start()
    {
        _scoreTextBox = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        _timeTextBox = GameObject.FindWithTag("TimeText").GetComponent<TextMeshProUGUI>();
        _slider = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
        StartCoroutine(TimerUpdate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int score)
    {
        _scoreTextBox.text = "SCORE: " + score.ToString().PadLeft(6, '0');
    }

    IEnumerator TimerUpdate()
    {
        while (_timerActive)
        {
            TimeSpan time = TimeSpan.FromSeconds(Time.time);
            _timeTextBox.text = "TIME: " + time.ToString(@"hh\:mm\:ss");
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void OnHealthUpdate(float healthValue)
    {
        _slider.value = healthValue;
    }
    public void GameOverActivate()
    {
        _gameOverTextbox.gameObject.SetActive(true);
        _timerActive = false;
    }

}
