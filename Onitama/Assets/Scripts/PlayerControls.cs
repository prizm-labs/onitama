using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public GameObject startTileStudent1;
	public GameObject startTileStudent2;
	public GameObject startTileStudent3;
	public GameObject startTileStudent4;
	public GameObject startTileMaster;

	public GameObject PawnStudent1;
	public GameObject PawnStudent2;
	public GameObject PawnStudent3;
	public GameObject PawnStudent4;
	public GameObject PawnMaster;

	public GameObject MovementCard1;
	public GameObject MovementCard2;
	public GameObject MovementCardNext;

	public GameObject MovementCardPosition1;
	public GameObject MovementCardPosition2;
	public GameObject MovementCardPositionNext;

	public GameObject activeIndicatorPosition;

	// Use this for initialization
	void Start () {

		setCards ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCards () {

		MovementCard1.transform.position = new Vector3(MovementCardPosition1.transform.position.x,MovementCardPosition1.transform.position.y,MovementCard1.transform.position.z);

		MovementCard2.transform.position = new Vector3(MovementCardPosition2.transform.position.x,MovementCardPosition2.transform.position.y,MovementCard2.transform.position.z);


		if (MovementCardNext != null) {
			MovementCardNext.transform.position = new Vector3(MovementCardPositionNext.transform.position.x,MovementCardPositionNext.transform.position.y,MovementCardNext.transform.position.z);
		}

	}

	public void shiftCards() {

	}

	public void receiveCard() {

	}
}
