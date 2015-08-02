using UnityEngine;
using System.Collections;

public class PCUI : MonoBehaviour {

    public Texture2D UIframe;
    public Texture2D UIhealthBar;
    public Rect UIframePosition;
    public Rect UIhealthBarPosition;

    public float horizontalDistance;
    public float verticalDistance;
    public float width;
    public float height;
    public float UIhealthPercentage;

	void Start ()
    {
	
	}
	
	void Update () 
    {
        OnGUI();
	}

    void OnGUI()
    {
        drawUIFrame();
        drawUIBar();
    }

    void drawUIFrame()
    {
        UIframePosition.x = (Screen.width - UIframePosition.width) / 2;
        UIframePosition.width = Screen.width * 0.39f;
        UIframePosition.height = Screen.height * 0.4f;
        GUI.DrawTexture(UIframePosition, UIframe);
    }
    void drawUIBar()
    {
        UIhealthBarPosition.x = UIframePosition.x + UIframePosition.width * horizontalDistance;
        UIhealthBarPosition.y = UIframePosition.y + UIframePosition.height * verticalDistance;
        UIhealthBarPosition.width = UIframePosition.width * width * UIhealthPercentage;
        UIhealthBarPosition.height = UIframePosition.height * height;

        GUI.DrawTexture(UIhealthBarPosition, UIhealthBar);
    }
}
