using System.Collections.Generic;
using PointPubUnityPlugin;
using UnityEngine;

public class PointPubSample : MonoBehaviour {

    private Vector2 scrollPosition;
    private List<string> buttonTitles = new List<string> {
        "EnableLogTrace",
        "StartOfferwall",
    };

    void Start()
    {
        Debug.Log("The application started.");
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                Application.Quit();
            }
        }
    }

    void OnGUI()
    {
        // Safe area 계산
        Rect safeArea = Screen.safeArea;

        // UI 크기 비율 설정
        int fontSize = (int)(Screen.width * 0.035f);
        float buttonWidth = safeArea.width * 0.7f;
        float buttonHeight = safeArea.height * 0.1f;
        float spacing = Screen.height * 0.02f;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = fontSize;

        // 스크롤 뷰의 전체 높이에 상단 여백 추가
        float totalContentHeight = buttonTitles.Count * (buttonHeight + spacing) + spacing;
        Rect viewRect = new Rect(0, 0, safeArea.width, totalContentHeight);

        scrollPosition = GUI.BeginScrollView(
            new Rect(safeArea.x, safeArea.y, safeArea.width, safeArea.height),
            scrollPosition,
            viewRect
        );

        // 최상단 여백
        float currentY = spacing;
        for (int i = 0; i < buttonTitles.Count; i++) {
            string title = buttonTitles[i];
            float x = (safeArea.width - buttonWidth) / 2;
            if (GUI.Button(new Rect(x, currentY, buttonWidth, buttonHeight), title, buttonStyle)) {
                HandleButtonClick(title);
            }
            currentY += buttonHeight + spacing;
        }

        GUI.EndScrollView();
    }

    private void HandleButtonClick(string title)
    {
        switch (title)
        {
            case "EnableLogTrace":
                PointPubSDK.Instance.EnableLogTrace();
                break;

            case "StartOfferwall":
                PointPubSDK.Instance.SetAppId("<ENTER_YOUR_APP_ID>");
                PointPubSDK.Instance.SetUserId("<ENTER_YOUR_USER_ID>");

                PointPubSDK.Instance.StartOfferwall(() => {
                    Debug.Log("Offerwall View Opened.");
                }, () => {
                    Debug.Log("Offerwall View Closed");
                });
                break;

            default:
                Debug.Log($"Unhandled button: {title}");
                break;
        }
    }
}

