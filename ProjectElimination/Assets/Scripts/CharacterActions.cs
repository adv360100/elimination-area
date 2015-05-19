using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class CharacterActions : MonoBehaviour {

	//private Rigidbody pRigidBody;
	private Queue<Actions> pActionQueue = new Queue<Actions> ();

	public float ShoveForce = 100f;
	public Collider Hitbox;
	public bool IsPlayerControlled = true;

	public abstract class Actions {	
		// performs action 
		public abstract void PerformAction ();
	}
	public class ShoveAction : Actions {
		public Vector3 shoveDir;
		public float shovePower;

		private Rigidbody targetBody;

		// constructor that takes in the target of the shove action
		public ShoveAction(Rigidbody target) {
			targetBody = target;
		}

		public override void PerformAction() {
			targetBody.AddForce (shoveDir * shovePower, ForceMode.Impulse);
		}
	}


	// Use this for initialization
	void Start () {
		//pRigidBody = GetComponent<Rigidbody> ();
		Hitbox.gameObject.SetActive(false);
	}

	void Update() {
		if (IsPlayerControlled == false)
			return;

		if (Input.GetButtonDown ("Fire1")) {
			// todo: animate

			Hitbox.gameObject.SetActive(true);
		} else {
			Hitbox.gameObject.SetActive(false);
		}
	}
	
	void FixedUpdate () {
		Actions[] actionList = new Actions[pActionQueue.Count];
		pActionQueue.CopyTo (actionList, 0);
		pActionQueue.Clear ();
		foreach (Actions act in actionList) {
			act.PerformAction();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" || other.tag == "Enemy") {
			Rigidbody target = other.GetComponent<Rigidbody> ();
			Vector3 dir = other.transform.position - Hitbox.transform.position;
			dir.y = 0f;
			dir.Normalize();
			ShoveTarget (target, dir, ShoveForce);
		}
	}

	//Queues action
	public void ShoveTarget(Rigidbody target, Vector3 shoveDir, float power) {
		ShoveAction act = new ShoveAction (target);
		act.shoveDir = shoveDir;
		act.shovePower = power;
		pActionQueue.Enqueue (act);
	}
}
