using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	Button myButton;

	void Awake()
	{
		myButton = GetComponent<Button>(); // <-- you get access to the button component here

		myButton.onClick.AddListener( () => {myFunctionForOnClickEvent("stringValue", 4.5f);} );  // <-- you assign a method to the button OnClick event here
		myButton.onClick.AddListener(() => {myAnotherFunctionForOnClickEvent("stringValue", 3);}); // <-- you can assign multiple methods
	}


	void myFunctionForOnClickEvent(string argument1, float argument2)
	{
		// your code goes here
		print(argument1 + ", " + argument2.ToString());
	}

	void myAnotherFunctionForOnClickEvent(string argument1, int argument2)
	{
		// your code goes here
		print (argument1 + ", " + argument2.ToString ());
	}
}
