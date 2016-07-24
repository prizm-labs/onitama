using UnityEngine;
using System.Collections;

public class TileSelection : MonoBehaviour {




	public GameObject selectedPawn;
	public GameObject threatenedPawn;
	public GameObject tileSelectionTarget;
	public GameObject pawnSelectionTarget;
	public GameObject capturedPawnLocation;

	[SerializeField] public bool IsPawnSelected = false; 
	[SerializeField] public bool IsPawnThreatened = false; 


	int pawnsLayerMask = 1 << 9;
	int tileGridLayerMask = 1 << 8;

	// Use this for initialization
	void Start () {
	
		hideSelectionTargets ();

	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonUp (0)){ 
			


			// if pawn not selected 
			if (IsPawnSelected==false) {
				selectPawn ();
			} else {
				selectTile ();
			}
				
		}


	}

	public void setPawnsAtStartingPositions () {

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


		if (Physics.Raycast (ray, out hit, 100.0f, pawnsLayerMask)) {
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


		// find pawn at tile
		if (Physics.Raycast (ray, out hit, 100.0f, pawnsLayerMask)) {
			GameObject parent = hit.transform.parent.gameObject;

			Debug.Log("You threatened the " + parent.name); // ensure you picked right object

			if (parent != selectedPawn) {

				threatenedPawn = parent;
				IsPawnThreatened = true;
			}

		}

		// get tile clicked
		if ( Physics.Raycast (ray,out hit,100.0f,tileGridLayerMask)) {

			StartCoroutine(ScaleMe(hit.transform));
			//Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object

			GameObject parent = hit.transform.parent.gameObject;

			if (parent) {

				Vector3 worldPosition = parent.transform.position;

				tileSelectionTarget.transform.position = new Vector3(worldPosition.x,worldPosition.y,tileSelectionTarget.transform.position.z);
				showTileSelectionIndicator ();

				// automatically confirm tile selection
				confirmTileSelection();

			}
		}
			
	}


	public void confirmTileSelection() {

		hideSelectionTargets ();

		// if pawn threatened, remove it
		if (IsPawnThreatened) {

			threatenedPawn.transform.position = capturedPawnLocation.transform.position;
			threatenedPawn = null;
			IsPawnThreatened = false;
		}

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
