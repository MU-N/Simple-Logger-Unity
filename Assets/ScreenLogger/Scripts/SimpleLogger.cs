
using System;
using System.Collections;
using UnityEngine;

namespace Nasser.io.SimpleLogger
{
    public class SimpleLogger : MonoBehaviour
    {

        [Header("First Log Waiting  Time ")]
        [Tooltip("This watingTime for the time waits for the next delete first log")]
        [Range(0f, 2.5f)]
        [SerializeField] float waitingTime = 1f;

        [Space(5)]
        [Header("Log Font Size")]
        [Tooltip("the size for the log text in the screen")]
        [Range(5f, 28f)]
        [SerializeField] int logFontSize = 8;

        [Space(5)]
        [Header("Log Margin")]
        [Tooltip("The space around each element’s to the next")]
        [Range(-20f, 20f)]
        [SerializeField] int LogMargin = 8;

        public Queue myLogQueue = new Queue();

        LoggerInfo loggerInfoTemp;

        GUIStyle currentGUIStyle;

        bool initStyleDone = false;

        private void Start()
        {
            StartCoroutine(nameof(WaitForDeleteLog), waitingTime);
        }
        struct LoggerInfo
        {
            public string logValue;
            public Color LogColor;
        }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void InitStyles()
        {
            initStyleDone = true;
            currentGUIStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = logFontSize,
                margin = new RectOffset(LogMargin, LogMargin, LogMargin, LogMargin),
                fontStyle = FontStyle.Bold
            };
        }



        void HandleLog(string logString, string stackTrace, LogType type)
        {
            loggerInfoTemp.logValue = $"[{DateTime.Now.ToString("HH:mm:ss")}]: {logString}";

            if (type == LogType.Error)
                loggerInfoTemp.LogColor = Color.red;
            if (type == LogType.Warning)
                loggerInfoTemp.LogColor = Color.yellow;
            if (type == LogType.Log)
                loggerInfoTemp.LogColor = Color.green;

            myLogQueue.Enqueue(loggerInfoTemp);

        }

        void OnGUI()
        {
            if (!initStyleDone)
                InitStyles();
            foreach (LoggerInfo logItem in myLogQueue)
            {
                GUI.contentColor = logItem.LogColor;
                GUILayout.Label(logItem.logValue, currentGUIStyle);
            }

        }



        IEnumerator WaitForDeleteLog(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                if (myLogQueue.Count > 0)
                    myLogQueue.Dequeue();
            }
        }
    }
}