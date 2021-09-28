using UnityEngine;
using System.Collections;


public class HandController : MonoBehaviour {

	public OVRInput.Controller Hand;
    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}
	
	void Update () {
       animator.SetBool("isGrabbing", OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, Hand));
	}
}
