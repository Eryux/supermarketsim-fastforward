// Copyright(c) 2024 - C.Nicolas <contact@bark.tf>
// github.com/eryux

using BepInEx;
using HarmonyLib;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FastForward
{
    [BepInPlugin("tf.bark.sms.FastForward", "FastForward", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static float _speed = 1f;


        public bool GamePaused { get; set; } = false;


        Setting _setting;

        Harmony _patches;

        TextMeshProUGUI _speedText;

        bool _mainSceneLoaded = false;


        private void Awake()
        {
            // Load configuration ---------------

            _setting = new Setting(Config);
            Logger.LogInfo("Configuration loaded");

            // Apply patches --------------------

            Logger.LogInfo("Apply patches...");

            DayCycleManagerPatch.plugin = this;
            EscapeMenuManagerPatch.plugin = this;
            WarningScreenPatch.plugin = this;

            _patches = new Harmony("tf.bark.sms.FastForward.patches");
            _patches.PatchAll(typeof(PlayerInteractionPatch));
            _patches.PatchAll(typeof(DayCycleManagerPatch));
            _patches.PatchAll(typeof(EscapeMenuManagerPatch));
            _patches.PatchAll(typeof(WarningScreenPatch));

            // ----------------------------------

            Logger.LogInfo($"Plugin FastForward is loaded!");
        }


        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        void OnDestroy()
        {
            if (_patches != null)
            {
                _patches.UnpatchSelf();
            }

            Logger.LogInfo("FastForward is unloaded.");
        }


        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _mainSceneLoaded = scene.buildIndex == 1;

            if (_mainSceneLoaded)
            {
                Initialize();
            }
            else
            {
                SetGameSpeed(1f);
            }
        }


        void Initialize()
        {
            // Build UI --
            GameObject ingameCanvasObject = GameObject.Find("Ingame Canvas");

            if (ingameCanvasObject != null)
            {
                GameObject timeBackgroundObject = GameObject.Find("Time BG");

                if (timeBackgroundObject != null)
                {
                    GameObject speedUIObject = Instantiate(timeBackgroundObject, timeBackgroundObject.transform.parent);
                    speedUIObject.name = "Game Speed UI";
                    
                    RectTransform speedUITransform = (RectTransform)speedUIObject.transform;
                    speedUITransform.position = Vector3.zero;
                    speedUITransform.rotation = Quaternion.identity;
                    speedUITransform.localScale = new Vector3(0.5f, 1f, 1f);
                    speedUITransform.anchoredPosition = new Vector3(-135f, 10f, 0f);
                    speedUITransform.SetSiblingIndex(0);

                    GameObject speedUITextObject = new GameObject("Game Speed Text");
                    RectTransform speedUITextTransform = speedUITextObject.AddComponent<RectTransform>();
                    speedUITextTransform.SetParent(speedUITransform);
                    speedUITextTransform.anchorMin = new Vector2(0f, 0f);
                    speedUITextTransform.anchorMax = new Vector2(1f, 0.45f);
                    speedUITextTransform.offsetMin = Vector2.zero;
                    speedUITextTransform.offsetMax = Vector2.zero;

                    _speedText = speedUITextObject.AddComponent<TextMeshProUGUI>();
                    _speedText.fontSize = 18f;
                    _speedText.alignment = TextAlignmentOptions.Center;
                    
                    TMP_FontAsset font = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().FirstOrDefault(x => x.name == "UptownBoy SDF");
                    if (font != null) {
                        _speedText.font = font;
                    }
                }
            }

            // Reset game speed to x1
            SetGameSpeed(1f);
        }


        void Update()
        {
            if (_setting.Input_DecreaseGameSpeed.Value.IsDown())
            {
                float newGameSpeed = 0f;

                for (int i = 0; i < _setting.GameSpeedLoop.Value.Length; ++i)
                {
                    if (_setting.GameSpeedLoop.Value[i] < _speed && _setting.GameSpeedLoop.Value[i] > newGameSpeed)
                    {
                        newGameSpeed = _setting.GameSpeedLoop.Value[i];
                    }
                }
                
                if (newGameSpeed > 0f)
                {
                    SetGameSpeed(newGameSpeed);
                }
            }

            if (_setting.Input_ResetGameSpeed.Value.IsDown())
            {
                SetGameSpeed(1f);
            }

            if (_setting.Input_IncreaseGameSpeed.Value.IsDown())
            {
                float newGameSpeed = float.MaxValue;

                for (int i = 0; i < _setting.GameSpeedLoop.Value.Length; ++i)
                {
                    if (_setting.GameSpeedLoop.Value[i] > _speed && _setting.GameSpeedLoop.Value[i] < newGameSpeed)
                    {
                        newGameSpeed = _setting.GameSpeedLoop.Value[i];
                    }
                }

                if (newGameSpeed < float.MaxValue)
                {
                    SetGameSpeed(newGameSpeed);
                }
            }
        }


        public void SetGameSpeed(float value)
        {
            if (_mainSceneLoaded && !GamePaused)
            {
                _speed = value;
                Time.timeScale = _speed;

                Logger.LogInfo($"Time speed set to x{_speed}");

                if (_speedText != null )
                {
                    _speedText.text = $"x{_speed}";
                }
            }
        }
    }
}
