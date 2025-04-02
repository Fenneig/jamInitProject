﻿using Jam.Scripts.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Jam.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _credits;

        [Inject] private SceneLoader _sceneLoader;
        
        private void Awake()
        {
            _startGame.onClick.AddListener(StartGame);
            _settings.onClick.AddListener(OpenSettings);
            _credits.onClick.AddListener(OpenCredits);
        }
        
        private void StartGame()
        {
            StartCoroutine(_sceneLoader.LoadScene(SceneEnum.Gameplay));
        }
        
        private void OpenSettings()
        {
            
        }
        
        private void OpenCredits()
        {
            
        }
    }
}
