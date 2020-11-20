﻿using UnityEngine;
using UnityEngine.UI;


namespace JigiJumper.UI
{
    public class SettingsWindowUI : WindowUI
    {
        [Header("Windows")]
        [SerializeField] private WindowUI _homeWindow = null;

        [SerializeField] private Button _btnBack = null;
        [SerializeField] private Toggle _toggle = null;

        private void Awake()
        {
            InitialComponent();

            _selfRect.gameObject.SetActive(false);
        }

        private void InitialComponent()
        {
            _behaviorUIs = new Behaviour[]
            {
                _btnBack,
                _toggle
            };

            _toggle.isOn = _setting.showBanner;

            _btnBack.onClick.AddListener(() => TransitionToWindow(this, _homeWindow));
            _toggle.onValueChanged.AddListener((value) => _setting.showBanner = value);
        }

    }
}