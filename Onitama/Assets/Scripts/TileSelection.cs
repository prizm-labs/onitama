using UnityEngine;
using System.Collections;

public class TileSelection : MonoBehaviour {

	public GameObject selectedPawn;
	public GameObject tileSelectionTarget;
	public GameObject pawnSelectionTarget;
	private bool IsPawnSelected = false; [SerializeField]

	// Use this for initialization
	void Start () {
	
		hideSelectionTargets ();

	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown (0)){ 
			


			// if pawn not selected 
			if (IsPawnSelected==false) {
				selectPawn ();
			} else {
				selectTile ();
			}

			// if selected pawn


		}


	}

	public void showPawnSelectionIndicator() {
		pawnSelectionTarget.transform.GetChild(0).GetComponent<Renderer> ().enabled = true;
	}

	public void showTileSelectionIndicator() {
		tileSelectionTarget.transform.GetChild(0).GetComponent<Renderer> ().enabled = true;
	}

	public void hideSelectionTargets() {
		tileSelectionTarget.transform.GetChild(0).GetComponent<Renderer> ().enabled = false;
		pawnSelectionTarget.transform.GetChild(0).GetComponent<Renderer> ().enabled = false;
	}

	public void selectPawn () {
		RaycastHit hit; 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
		var tileGridLayerMask = 1 << 9;

		if (Physics.Raycast (ray, out hit, 100.0f, tileGridLayerMask)) {
			GameObject parent = hit.transform.parent.gameObject;
			Vector3 worldPosition = parent.transform.position;

			pawnSelectionTarget.transform.position = new Vector3(worldPosition.x,worldPosition.y,pawnSelectionTarget.transform.position.z);

			showPawnSelectionIndicator ();


			selectedPawn = parent;
			IsPawnSelected = true;
		}
	}

	public void selectTile () {

		RaycastHit hit; 
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
		var tileGridLayerMask = 1 << 8;

		if ( Physics.Raycast (ray,out hit,100.0f,tileGridLayerMask)) {




			StartCoroutine(ScaleMe(hit.transform));
			Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object

			GameObject parent = hit.transform.parent.gameObject;

			if (parent) {
				Debug.Log("You selected the " + parent.name); // ensure you picked right object


				if (parent.layer == 8) { // in TileGird layer
					Vector3 worldPosition = parent.transform.position;


					tileSelectionTarget.transform.position = new Vector3(worldPosition.x,worldPosition.y,tileSelectionTarget.transform.position.z);
					showTileSelectionIndicator ();

					// automatically confirm tile selection
					confirmTileSelection();
				}
			}
		}


	}


	public void confirmTileSelection() {

		hideSelectionTargets ();

		selectedPawn.transform.position = new Vector3 (tileSelectionTarget.transform.position.x, tileSelectionTarget.transform.position.y, selectedPawn.transform.position.z);

		selectedPawn = null;
		IsPawnSelected = false;
	}

	IEnumerator ScaleMe(Transform objTr) {
		objTr.localScale *= 1.2f;
		yield return new WaitForSeconds(0.5f);
		objTr.localScale /= 1.2f;
	}
}
