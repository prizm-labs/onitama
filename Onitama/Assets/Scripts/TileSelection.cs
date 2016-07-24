using UnityEngine;
using System.Collections;

public class TileSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown (0)){ 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if ( Physics.Raycast (ray,out hit,100.0f)) {




				StartCoroutine(ScaleMe(hit.transform));
				Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object

				GameObject parent = hit.transform.parent.gameObject;

				if (parent) {
					Debug.Log("You selected the " + parent.name); // ensure you picked right object

					if (parent.layer == 8) { // in TileGird layer
						Vector3 worldPosition = parent.transform.position;

						GameObject target = GameObject.Find ("SelectionTarget");
						target.transform.position = new Vector3(worldPosition.x,worldPosition.y,target.transform.position.z);

					}
				}
			}
		}


	}

	IEnumerator ScaleMe(Transform objTr) {
		objTr.localScale *= 1.2f;
		yield return new WaitForSeconds(0.5f);
		objTr.localScale /= 1.2f;
	}
}
