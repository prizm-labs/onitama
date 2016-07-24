using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playerAControls;
	public GameObject playerBControls;

	public GameObject activePlayer;
	public GameObject passivePlayer;

	public GameObject selectedCard;
	public GameObject selectedCardSlot;
	public GameObject nextCard;

	public GameObject selectedCardIndicator;
	public GameObject activePlayerIndicator;

	int movementCardSlotLayerMask = 1 << 10;
	int movementCardLayerMask = 1 << 11;

	// Use this for initialization
	void Start () {

		selectRandomCards ();

		setActivePlayer ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp (0)) { 
			// select card for active player

			if (selectedCard == null) {
				
				selectCardSlot ();


			} else {

				// TODO enable pawn selection
				// confirm move target

				// get event on confirm UI

				// automatically confirm move
				confirmMove ();
			}



		}


	}

	public void selectRandomCards() {

	}

	public void resetGame() {

	}


	public void setActivePlayer() {

		Vector3 p = activePlayer.GetComponent<PlayerControls> ().activeIndicatorPosition.transform.position;
		activePlayerIndicator.transform.position = new Vector3 (p.x, p.y, activePlayerIndicator.transform.position.z);

		// hide selectedCardIndicator
		selectedCardIndicator.transform.GetChild(0).GetComponent<Renderer> ().enabled = false;
		selectedCardIndicator.transform.GetChild(1).GetComponent<Renderer> ().enabled = false;
	}

	public void selectCardSlot() {

		RaycastHit hit; 
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 

		if (Physics.Raycast (ray, out hit, 100.0f, movementCardSlotLayerMask)) {

			GameObject parent = hit.transform.parent.gameObject;

			if (parent.transform.CompareTag("CardSlot1") || parent.transform.CompareTag("CardSlot2") ) {

				selectedCardSlot = parent;

				Vector3 worldPosition = parent.transform.position;

				selectedCardIndicator.transform.position = new Vector3 (worldPosition.x, worldPosition.y, selectedCardIndicator.transform.position.z);

				// show selectedCardIndicator
				selectedCardIndicator.transform.GetChild(0).GetComponent<Renderer> ().enabled = true;
				selectedCardIndicator.transform.GetChild(1).GetComponent<Renderer> ().enabled = true;

				selectCard ();
			}

		}
	}

	public void selectCard() {



		RaycastHit hit; 
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); 

		if (Physics.Raycast (ray, out hit, 100.0f, movementCardLayerMask)) {

			GameObject parent = hit.transform.parent.gameObject;

			Debug.Log("You selected the " + parent.name); // ensure you picked right object

			selectedCard = parent;
		}

		
	}

	public void confirmMove() {



		passivePlayer.GetComponent<PlayerControls> ().receiveCard ();

		// move selected card to passive player's next card
		Vector3 p = passivePlayer.GetComponent<PlayerControls> ().MovementCardPositionNext.transform.position;

		selectedCard.transform.position = new Vector3(p.x,p.y,selectedCard.transform.position.z);
		passivePlayer.GetComponent<PlayerControls> ().MovementCardNext = selectedCard;


		// move next card to active players' current cards
		activePlayer.GetComponent<PlayerControls> ().shiftCards ();

		Vector3 p2 = activePlayer.GetComponent<PlayerControls> ().MovementCardNext.transform.position;
		activePlayer.GetComponent<PlayerControls> ().MovementCardNext.transform.position = new Vector3 (selectedCardSlot.transform.position.x,selectedCardSlot.transform.position.y,p2.z);

		if (selectedCardSlot.transform.CompareTag("CardSlot1") ){
			activePlayer.GetComponent<PlayerControls> ().MovementCard1 = activePlayer.GetComponent<PlayerControls> ().MovementCardNext;
		} else if (selectedCardSlot.transform.CompareTag("CardSlot2") ) {
			activePlayer.GetComponent<PlayerControls> ().MovementCard2 = activePlayer.GetComponent<PlayerControls> ().MovementCardNext;
		}

		activePlayer.GetComponent<PlayerControls> ().MovementCardNext = null;


		selectedCard = null;
		selectedCardSlot = null;



		// swtich active players
		switchActivePlayer ();
	}

	public void switchActivePlayer() {

		var oldActivePlayer = activePlayer;
		var oldPassivePlayer = passivePlayer;

		activePlayer = oldPassivePlayer;
		passivePlayer = oldActivePlayer;

		setActivePlayer ();
	}
}
