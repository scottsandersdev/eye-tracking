using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tobii.Gaming;

public class canvas_test : MonoBehaviour {
    public GameObject GazePoint;
    private Color _indigo = new Color32(75, 0, 130, 255);

    private Text colorText;
    private Text coordinatesText;
    private Text xMaxText;
    private Text xMinText;
    private float xMax = 0;
    private float xMin = 0;
    private Image bg;

    void Start() {
        colorText = GameObject.Find("colorText").GetComponent<Text>();
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
//            int roundedX = Mathf.RoundToInt(gazePosition.x);
            Vector2 gazePositionRounded = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));
            if (gazePositionRounded.x > xMax) {
                xMax = gazePositionRounded.x;
                xMaxText.text = "xMax = " + gazePositionRounded.x;
            }

            if (gazePositionRounded.x < xMin) {
                xMin = gazePositionRounded.x;
                xMinText.text = "xMin = " + gazePositionRounded.x;
            }
            //print(gazePositionRounded.x + " , " + gazePositionRounded.y);
            coordinatesText.text = gazePositionRounded.x + ", " + gazePositionRounded.y;
            if (gazePositionRounded.x < 795) {
                colorText.text = "Red";
                bg.color = Color.red;
            }
            if (gazePositionRounded.x > 795 && gazePositionRounded.x < 1590) {
                colorText.text = "Pink";
                bg.color = Color.magenta;
            }
            if (gazePositionRounded.x > 1590) {
                colorText.text = "Purple";
                bg.color = _indigo;
            }
        }
    }
}
