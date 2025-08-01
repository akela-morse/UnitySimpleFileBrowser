﻿using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace SimpleFileBrowser
{
    [RequireComponent(typeof(TMP_Text))]
    public class FileBrowserLocalizeText : MonoBehaviour
    {
        [SerializeField] private string m_StringKey;

        private TMP_Text m_TmpText;
        private bool m_RequestUpdateOnEnable;

        public void ForceRefresh()
        {
            OnSelectedLocaleChanged(LocalizationSettings.SelectedLocale);
        }

        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnEnable()
        {
            if (!m_RequestUpdateOnEnable)
                return;

            m_RequestUpdateOnEnable = false;
            Refresh();
        }

        private void OnSelectedLocaleChanged(Locale locale)
        {
            if (!isActiveAndEnabled)
            {
                m_RequestUpdateOnEnable = true;
                return;
            }

            Refresh();
        }

        private void Refresh()
        {
            var skin = FileBrowser.Skin;

            if (!skin)
                return;

            // We want to know whether the object is initialised or not, so we cast it to object to circumvent Unity's overloaded operator that also accounts for destruction
            if ((object)m_TmpText == null)
                m_TmpText = GetComponent<TMP_Text>();

            m_TmpText.text = skin.LocalizedText.GetStringFromKey(m_StringKey);
        }
    }
}