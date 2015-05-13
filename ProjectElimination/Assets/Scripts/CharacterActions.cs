using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class CharacterActions : MonoBehaviour {

	private Rigidbody pRigidBody;
	private Queue<Actions> pActionQueue = new Queue<Actions> ();

	public delegate void ActionDelegate();
	public float ShoveForce = 100f;
	public class Actions {
		public ActionDelegate myAction;
	
		// Pushes 
//		public void Shove() {
//			pRigidBody.AddForce (transform.forward * ShoveForce, ForceMode.Impulse);
//		}
	}

	// Use this for initialization
	void Start () {
		pRigidBody = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		Actions[] actionList = new Actions[pActionQueue.Count];
		pActionQueue.CopyTo (actionList, 0);
		pActionQueue.Clear ();
		foreach (Actions act in actionList) {
			act.myAction();
		}
	}

	public void Shove() {
		pRigidBody.AddForce (transform.forward * ShoveForce, ForceMode.Impulse);
	}

	//Queues action
	public void PerformAction(Actions act) {
		pActionQueue.Enqueue (act);
	}
}
