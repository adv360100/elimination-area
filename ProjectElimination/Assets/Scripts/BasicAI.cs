using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(AICharacterControl))]
public class BasicAI : MonoBehaviour {

	private AICharacterControl pAICC;

	// Use this for initialization
	void Start () {
		pAICC = GetComponent<AICharacterControl> ();
	}

}
