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


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setPawns() {
		PawnStudent1.transform.position = new Vector3(startTileStudent1.transform.position.x,startTileStudent1.transform.position.y,PawnStudent1.transform.position.z);
		PawnStudent2.transform.position = new Vector3(startTileStudent2.transform.position.x,startTileStudent2.transform.position.y,PawnStudent2.transform.position.z);
		PawnStudent3.transform.position = new Vector3(startTileStudent3.transform.position.x,startTileStudent3.transform.position.y,PawnStudent3.transform.position.z);
		PawnStudent4.transform.position = new Vector3(startTileStudent4.transform.position.x,startTileStudent4.transform.position.y,PawnStudent4.transform.position.z);
		PawnMaster.transform.position = new Vector3(startTileMaster.transform.position.x,startTileMaster.transform.position.y,PawnMaster.transform.position.z);
	}

	public void setCards () {

		MovementCard1.transform.position = new Vector3(MovementCardPosition1.transform.position.x,MovementCardPosition1.transform.position.y,MovementCard1.transform.position.z);

		MovementCard2.transform.position = new Vector3(MovementCardPosition2.transform.position.x,MovementCardPosition2.transform.position.y,MovementCard2.transform.position.z);

		MovementCard1.transform.eulerAngles = new Vector3 (MovementCard1.transform.eulerAngles.x, MovementCard1.transform.eulerAngles.y, transform.eulerAngles.z);
		MovementCard2.transform.eulerAngles = new Vector3 (MovementCard2.transform.eulerAngles.x, MovementCard2.transform.eulerAngles.y, transform.eulerAngles.z);

		if (MovementCardNext != null) {
			MovementCardNext.transform.position = new Vector3(MovementCardPositionNext.transform.position.x,MovementCardPositionNext.transform.position.y,MovementCardNext.transform.position.z);
			MovementCardNext.transform.eulerAngles = new Vector3 (MovementCardNext.transform.eulerAngles.x, MovementCardNext.transform.eulerAngles.y, transform.eulerAngles.z);
		}

	}

	public void shiftCards() {

	}

	public void receiveCard() {

	}
}
