using System.Collections;
using System.Collections.Generic;
using PointPubUnityPlugin;
using UnityEngine;

public class PointPubSample : MonoBehaviour {
    private AndroidJavaObject pluginInstance;

    void Start() {

    }

    void Update() {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    void OnGUI()
    {
        var fontSize = (int)(0.035f * Screen.width);
        var buttonWidth = 0.35f * Screen.width;
        var buttonHeight = 0.15f * Screen.height;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = fontSize;

        float buttonX = (Screen.width - buttonWidth) / 2;
        float buttonY = (Screen.height - buttonHeight) / 2;

        if (GUI.Button(new Rect(buttonX, buttonY, buttonWidth, buttonHeight), "Start Offerwall", buttonStyle))
        {
            PointPubSDK.Instance.EnableLogTrace();
            PointPubSDK.Instance.StartOfferwall("userId");
        }
    }
}
