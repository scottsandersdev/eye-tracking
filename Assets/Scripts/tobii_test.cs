using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tobii.Gaming;

public class tobii_test : MonoBehaviour
{
	public GameObject GazePoint;
	private MeshRenderer _meshRenderer;
	private Color _indigo = new Color32(75, 0, 130, 255);

	TextMesh text;
	
	void Start()
	{
		text = GameObject.Find("Text").GetComponent<TextMesh>();
		_meshRenderer = GameObject.Find("Plane").GetComponent<MeshRenderer>();
	}

	void Update()
	{
		GazePoint gazePoint = TobiiAPI.GetGazePoint();
		if (gazePoint.IsValid) {
			Vector2 gazePosition = gazePoint.Screen;
			Vector2 gazePositionRounded = new Vector2(Mathf.RoundToInt(gazePosition.x), Mathf.RoundToInt(gazePosition.y));
			// print(gazePositionRounded.x + " , " + gazePositionRounded.y);
			if (gazePositionRounded.x < 795) {
				text.text = "Red";
				_meshRenderer.material.color = Color.red;
			}
			if (gazePositionRounded.x > 795 && gazePositionRounded.x < 1590) {
				text.text = "Pink";
				_meshRenderer.material.color = Color.magenta;
			}
			if (gazePositionRounded.x > 1590 && gazePositionRounded.x < 2385) {
				text.text = "Purple";
				_meshRenderer.material.color = _indigo;
			}
		}
	}
}
