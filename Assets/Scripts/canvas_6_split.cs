using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using Tobii.Gaming;

public class canvas_6_split : MonoBehaviour {
    public GameObject GazePoint;

    private Color _indigo = new Color32(75, 0, 130, 255);

    private SocketIOComponent socket;

    private Text coordinatesText;
    private Text xMaxText;
    private Text xMinText;
    private Text yMaxText;
    private Text yMinText;

    int xMax = 0;
    int xMin = 0;
    int yMax = 0;
    int yMin = 0;

    private Image bg;

    void Start() {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("open", TestOpen);

        coordinatesText = GameObject.Find("coordinatesText").GetComponent<Text>();
        xMaxText = GameObject.Find("xMax").GetComponent<Text>();
        xMinText = GameObject.Find("xMin").GetComponent<Text>();
        yMaxText = GameObject.Find("yMax").GetComponent<Text>();
        yMinText = GameObject.Find("yMin").GetComponent<Text>();
        bg = GameObject.Find("Background").GetComponent<Image>();
    }

    void Update() {
        socket.Emit("open");
        UserPresence userPresence = TobiiAPI.GetUserPresence();
        print(userPresence.IsUserPresent());

        GazePoint gazePoint = TobiiAPI.GetGazePoint();

        if (gazePoint.IsValid) {
            Vector2 gazePosition = gazePoint.Screen;

            int xPos = Mathf.RoundToInt(gazePosition.x);
            int yPos = Mathf.RoundToInt(gazePosition.y);

            coordinatesText.text = xPos + ", " + yPos;

            SetMinMax(xPos, yPos);
            
            CalculateFocus(xPos, yPos);
        }
    }

    private void CalculateFocus(int xPos, int yPos) {
        bool isTop = yPos > 250;

        if(isTop) {
            if (xPos < 1000) {
                bg.color = Color.red;
            }

            if (xPos >= 1000 && xPos < 2000) {
                bg.color = Color.magenta;
            }

            if (xPos >= 2000) {
                bg.color = _indigo;
            }
        } else {
            if (xPos < 1000) {
                bg.color = Color.green;
            }

            if (xPos >= 1000 && xPos < 2000) {
                bg.color = Color.gray;
            }

            if (xPos >= 2000) {
                bg.color = Color.yellow;
            }
        }
    }

    public void TestOpen(SocketIOEvent e) {
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }

    private void checkForUser() {
        UserPresence userPresence = TobiiAPI.GetUserPresence();
        print(userPresence.IsUserPresent());
    }

    private void SetMinMax(int xPos, int yPos) {
        if (xPos > xMax) {
            xMax = xPos;
            xMaxText.text = "xMax = " + xPos;
        }

        if (xPos < xMin)  {
            xMin = xPos;
            xMinText.text = "xMin = " + xPos;
        }

        if (yPos > yMax) {
            yMax = yPos;
            yMaxText.text = "yMax = " + yPos;
        }

        if (yPos < yMin) {
            yMin = yPos;
            yMinText.text = "yMin = " + yPos;
        }
    }
}
