using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour {

	private Camera _camera;

	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		GUI.contentColor = Color.white;
	}

	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight / 2 - size / 2;
		GUI.Label (new Rect(posX, posY, size, size), "*"); 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 point = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0); // middle of the screen
			Ray ray = _camera.ScreenPointToRay (point); // the line for the ray
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) { // do the raycast
				//Debug.Log ("Hit " + hit.point); // retrieve coords where the ray hit
				GameObject hitObject = hit.transform.gameObject;
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget> ();
				if (target != null) {
					target.ReactToHit ();
				} else {
					StartCoroutine (SphereIndicator(hit.point)); // launch coroutine in response to a ht
				}
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 pos) {
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = pos;

		yield return new WaitForSeconds (1); // yield tells coroutines where to pause

		Destroy (sphere);
	}
}
