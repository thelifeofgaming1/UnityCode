using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
[AddComponentMenu ("Character/FPS Input Controller")]

public class MouseLookScript : MonoBehaviour {
	public float looksensitivity = 5f;
	[HideInInspector]
	public float yrotation;
	[HideInInspector]
	public float xrotation;
	[HideInInspector]
	public float Currentyrotation;
	[HideInInspector]
	public float Currentxrotation;
	[HideInInspector]
	public float yRotaionV;
	[HideInInspector]
	public float xRotationV;
	public bool IsNormal = true;
	public float lookSmoothDamp = 0.1f;

	public GameObject CameraObject;
	public GameObject LeftArmRoot;
	public GameObject RightArmRoot;

	private CharacterMotor motor;

	void Start(){
		motor = GetComponent<CharacterMotor>();
	}

	void Awake () {
		motor = GetComponent<CharacterMotor>();
	}

	void Update () {
		if (IsNormal) {
			yrotation += Input.GetAxis ("Mouse X") * looksensitivity;
			xrotation -= Input.GetAxis ("Mouse Y") * looksensitivity;
			xrotation = Mathf.Clamp (xrotation, -90, 90);
			Currentxrotation = Mathf.SmoothDamp (Currentxrotation, xrotation, ref xRotationV, lookSmoothDamp);
			Currentyrotation = Mathf.SmoothDamp (Currentyrotation, yrotation, ref yRotaionV, lookSmoothDamp);
			transform.rotation = Quaternion.Euler (0, yrotation, 0);
			CameraObject.transform.rotation = Quaternion.Euler (xrotation, yrotation, 0);


		} else {

			yrotation += Input.GetAxis ("Mouse X") * looksensitivity;
			xrotation -= Input.GetAxis ("Mouse Y") * looksensitivity;
			xrotation = Mathf.Clamp (xrotation, -90, 90);
			Currentxrotation = Mathf.SmoothDamp (Currentxrotation, xrotation, ref xRotationV, lookSmoothDamp);
			Currentyrotation = Mathf.SmoothDamp (Currentyrotation, yrotation, ref yRotaionV, lookSmoothDamp);
			CameraObject.transform.rotation = Quaternion.Euler (xrotation, yrotation, 0);
		}


		var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if (directionVector != Vector3.zero) {
			var directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			directionLength = Mathf.Min(1, directionLength);
			directionLength = directionLength * directionLength;
			directionVector = directionVector * directionLength;
		}
		//this.transform.Translate(directionVector);   //this was me just testing, dont mind it
		motor.inputMoveDirection = transform.rotation * directionVector;
		motor.inputJump = Input.GetButton("Jump");
	}
}