﻿using UnityEngine;
//using System;
using System.Collections;
using System.Collections.Generic;

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

		SetupGame ();
	}

	void SetupGame() {
		setGamePieces ();

		chooseFirstPlayer ();
		selectRandomCards ();
		setActivePlayer ();

		activePlayer.GetComponent<PlayerControls>().setCards ();
		passivePlayer.GetComponent<PlayerControls>().setCards ();
	}

	void returnCardsToDeck() {


		SpriteRenderer[] cardsInDeck = GameObject.Find("MovementCardsDeck").GetComponentsInChildren<SpriteRenderer>();
		Debug.Log ("cards in deck: "+cardsInDeck.Length);

		foreach (SpriteRenderer card in cardsInDeck) {
			card.gameObject.transform.parent.gameObject.transform.position = new Vector3 (100, 100, card.gameObject.transform.parent.gameObject.transform.position.z);
		}

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


		var selectionCount = 5;


		var source = new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14};
		var selection = new List<int> ();

		while (selection.Count < selectionCount) {
			var MyIndex = Random.Range(0,source.Count);
			selection.Add(source[MyIndex]);

			Debug.Log (source[MyIndex]);

			source.Remove (source [MyIndex]);
		}

		// pick 5 random cards
		SpriteRenderer[] cardsInDeck = GameObject.Find("MovementCardsDeck").GetComponentsInChildren<SpriteRenderer>();
		Debug.Log ("cards in deck: "+cardsInDeck.Length);
		// assign first 2 to playerA 
		activePlayer.GetComponent<PlayerControls>().MovementCard1 = cardsInDeck[selection[0]].gameObject.transform.parent.gameObject;
		activePlayer.GetComponent<PlayerControls>().MovementCard2 = cardsInDeck[selection[1]].gameObject.transform.parent.gameObject;

		// assign next 2 to playerB
		passivePlayer.GetComponent<PlayerControls>().MovementCard1 = cardsInDeck[selection[2]].gameObject.transform.parent.gameObject;
		passivePlayer.GetComponent<PlayerControls>().MovementCard2 = cardsInDeck[selection[3]].gameObject.transform.parent.gameObject;

		// assign last to next card
		activePlayer.GetComponent<PlayerControls>().MovementCardNext = cardsInDeck[selection[4]].gameObject.transform.parent.gameObject;
	}

	public void chooseFirstPlayer() {

		// coin toss?

	}

	public void setGamePieces() {
		activePlayer.GetComponent<PlayerControls> ().setPawns ();
		passivePlayer.GetComponent<PlayerControls> ().setPawns ();
	}

	public void resetGame() {
		Debug.Log ("reset game");

		returnCardsToDeck ();

		SetupGame ();
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
//		Vector3 p = passivePlayer.GetComponent<PlayerControls> ().MovementCardPositionNext.transform.position;
//
//		selectedCard.transform.position = new Vector3(p.x,p.y,selectedCard.transform.position.z);
		passivePlayer.GetComponent<PlayerControls> ().MovementCardNext = selectedCard;
		passivePlayer.GetComponent<PlayerControls> ().setCards ();


		// move next card to active players' current cards
		activePlayer.GetComponent<PlayerControls> ().shiftCards ();

//		Vector3 p2 = activePlayer.GetComponent<PlayerControls> ().MovementCardNext.transform.position;
//		activePlayer.GetComponent<PlayerControls> ().MovementCardNext.transform.position = new Vector3 (selectedCardSlot.transform.position.x,selectedCardSlot.transform.position.y,p2.z);

		if (selectedCardSlot.transform.CompareTag("CardSlot1") ){
			activePlayer.GetComponent<PlayerControls> ().MovementCard1 = activePlayer.GetComponent<PlayerControls> ().MovementCardNext;
		} else if (selectedCardSlot.transform.CompareTag("CardSlot2") ) {
			activePlayer.GetComponent<PlayerControls> ().MovementCard2 = activePlayer.GetComponent<PlayerControls> ().MovementCardNext;
		}

		activePlayer.GetComponent<PlayerControls> ().MovementCardNext = null;

		activePlayer.GetComponent<PlayerControls> ().setCards ();




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
