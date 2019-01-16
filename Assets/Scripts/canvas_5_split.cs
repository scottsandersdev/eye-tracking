using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tobii.Gaming;

public class canvas_5_split : MonoBehaviour {
    public GameObject GazePoint;

    private Color _indigo = new Color32(75, 0, 130, 255);

    private Text coordinatesText;
    private Text xMaxText;
    private Text xMinText;

    private float xMax = 0;
    private float xMin = 0;

    private Image bg;

    void Start() {
        coordinatesText = GameObject.Find("coordinatesText").GetComponent<Text>();
        xMaxText = GameObject.Find("xMax").GetComponent<Text>();
        xMinText = GameObject.Find("xMin").GetComponent<Text>();
        bg = GameObject.Find("Background").GetComponent<Image>();
    }

    void Update() {
        UserPresence userPresence = TobiiAPI.GetUserPresence();
        print(userPresence.IsUserPresent());
        GazePoint gazePoint = TobiiAPI.GetGazePoint();

        if (gazePoint.IsValid) {
            Vector2 gazePosition = gazePoint.Screen;

            Vector2 gazePositionRounded = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));

            coordinatesText.text = gazePositionRounded.x + ", " + gazePositionRounded.y;

            if (gazePositionRounded.x > xMax) {
                xMax = gazePositionRounded.x;
                xMaxText.text = "xMax = " + gazePositionRounded.x;
            }

            if (gazePositionRounded.x < xMin) {
                xMin = gazePositionRounded.x;
                xMinText.text = "xMin = " + gazePositionRounded.x;
            }

            if (gazePositionRounded.x < 600) {
                bg.color = Color.red;
            }

            if (gazePositionRounded.x >= 600 && gazePositionRounded.x < 1200) {
                bg.color = Color.magenta;           }

            if (gazePositionRounded.x >= 1200 && gazePositionRounded.x < 1800) {
                bg.color = _indigo;
            }

            if (gazePositionRounded.x >= 1800 && gazePositionRounded.x < 2400) {
                bg.color = Color.green;
            }

            if (gazePositionRounded.x >= 2400) {
                bg.color = Color.yellow;
            }
        }
    }
}
