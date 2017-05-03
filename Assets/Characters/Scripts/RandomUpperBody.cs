using System.IO;
using UnityEngine;

public class RandomUpperBody : MonoBehaviour
{
	enum BodyParts { Chest, Head, LeftUpperArm, RightUpperArm, LeftLowerArm, RightLowerArm, LeftHand, RightHand};
	public float bodyPartMass;
	private bool isQuadruped;
	// Body Parts
	private string charactersDir = "Prefabs/Characters/";
	private string characterPath;
	private string[] characterDirs;

	public bool rotate;
	public string punchKey;

	public string horizontalAxis;
	public string verticalAxis;

	public GameObject lowerBody;

	private GameObject chest;
	private GameObject head;
	private GameObject leftUpperArm;
	private GameObject rightUpperArm;
	private GameObject leftLowerArm;
	private GameObject rightLowerArm;
	private GameObject leftHand;
	private GameObject rightHand;

	private GameObject leftFistHome;
	private GameObject leftFistAway;

	void Start()
	{

		transform.position = lowerBody.transform.position + new Vector3 (0, 1.5f, 0);
		if (rotate) {
			transform.Rotate (new Vector3 (0, -90, 0));
		} else {
			transform.Rotate (new Vector3 (0, 90, 0));
		}
		//Mass should not be lower than .1;
		if (bodyPartMass < 0.1f) {
			bodyPartMass = 0.1f;
		}
		characterPath = Application.dataPath + "/Resources/" + charactersDir;
		characterDirs = Directory.GetDirectories(characterPath);

		// Instantiate objects
		string chestPath = LoadRandomBodyPath ();
		isQuadruped = chestPath.Equals ("Horse");
		chest = InstantiateBodyPart(Resources.Load(charactersDir + chestPath + "/" + BodyParts.Chest), gameObject);
		chest.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
		head = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Head), chest);
		string upperArmPath = "Human"; /*LoadRandomBodyPath ();
		while (upperArmPath.Equals ("T-Rex")) {
			upperArmPath = LoadRandomBodyPath ();
		}*/
		leftUpperArm = InstantiateBodyPart(Resources.Load(charactersDir + upperArmPath + "/" + BodyParts.LeftUpperArm), chest);
		rightUpperArm = InstantiateBodyPart(Resources.Load(charactersDir + upperArmPath + "/" + BodyParts.RightUpperArm), chest);
		string lowerArmPath = "Horse";
		leftLowerArm = InstantiateBodyPart(Resources.Load(charactersDir + lowerArmPath + "/" + BodyParts.LeftLowerArm), leftUpperArm);
		rightLowerArm = InstantiateBodyPart(Resources.Load(charactersDir + lowerArmPath + "/" + BodyParts.RightLowerArm), rightUpperArm);
		string footPath = "Human";
		leftHand = InstantiateBodyPart(Resources.Load(charactersDir + footPath + "/" + BodyParts.LeftHand), leftLowerArm);
		rightHand = InstantiateBodyPart(Resources.Load(charactersDir + footPath + "/" + BodyParts.RightHand), rightLowerArm);
		leftHand.tag = "Hand";

		//TODO: Instantiate lower body

		//Set the mass of the upper body to bodyPartMass
		ChangeMassOfUpperBody();

		UpperBodyPID upperBodyPID = chest.AddComponent<UpperBodyPID> ();
		upperBodyPID.isQuadruped = isQuadruped;

		chest.transform.localPosition = Vector3.zero;

		// Attach joints
		attach(head, chest, chest.transform.Find("Head Socket"));
		attach(leftUpperArm, chest, chest.transform.Find("Left Arm Socket"));
		attach(rightUpperArm, chest, chest.transform.Find("Right Arm Socket"));
		attachSliceable(leftLowerArm, leftUpperArm, leftUpperArm.transform.Find("Lower Arm Socket"));
		attachSliceable(rightLowerArm, rightUpperArm, rightUpperArm.transform.Find("Lower Arm Socket"));
		attachSliceable(leftHand, leftLowerArm, leftLowerArm.transform.Find("Hand Socket"));
		attachSliceable(rightHand, rightLowerArm, rightLowerArm.transform.Find("Hand Socket"));

		Joint joint = lowerBody.GetComponent<Joint>();
		joint.connectedBody = chest.GetComponent<Rigidbody>();




		EnablePunching ();
		//EnableUserInput ();


		// Freeze chest position for testing
		//chest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
	}

	private void ChangeMassOfUpperBody(){

		chest.GetComponent<Rigidbody> ().mass = bodyPartMass;
		head.GetComponent<Rigidbody> ().mass = bodyPartMass;

		//If it is not a quadruped it's arms are part of the upper body and thus their masses should be changed;
		if (!isQuadruped) {
			leftUpperArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			rightUpperArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			leftLowerArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			rightLowerArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			leftHand.GetComponent<Rigidbody> ().mass = bodyPartMass;
			rightHand.GetComponent<Rigidbody> ().mass = bodyPartMass;
		}

	}

	//Create two regions near left arm of character for using as spring anchors when punching
	private void EnablePunching(){
		leftFistHome = InstantiateBodyPart (Resources.Load(charactersDir + "TriggerRegion"),chest);
		leftFistAway = InstantiateBodyPart (Resources.Load(charactersDir + "TriggerRegion"),chest);
		PunchingScript punchingScript = leftFistAway.AddComponent<PunchingScript> ();
		punchingScript.key = punchKey;

		punchingScript.fist = leftHand.GetComponent <Rigidbody>();
		punchingScript.home = leftFistHome.GetComponent<Rigidbody>();

		float upperArmLength = GetLongest (leftUpperArm.GetComponent<Renderer> ().bounds.size);
		float armLength = GetLongest (leftLowerArm.GetComponent<Renderer> ().bounds.size) + upperArmLength;

		leftHand.GetComponent<Rigidbody> ().mass = 0.1f;
		leftUpperArm.GetComponent<Rigidbody> ().mass = 0.1f;
		leftLowerArm.GetComponent<Rigidbody> ().mass = 0.1f;

		Vector3 leftShoulderPos = chest.transform.Find ("Left Arm Socket").position;
		leftFistHome.transform.position = leftShoulderPos + leftFistHome.transform.forward * upperArmLength - leftFistHome.transform.up * 0.1f;
		leftFistAway.transform.position = leftShoulderPos + leftFistAway.transform.forward * armLength - leftFistAway.transform.up * 0.1f;

		Vector3 leftHandPos = leftHand.transform.position;
		leftHand.transform.position = leftFistHome.transform.position;
		SpringJoint leftHandSpring = leftHand.AddComponent<SpringJoint> ();
		leftHandSpring.connectedBody = leftFistHome.GetComponent<Rigidbody> ();
		leftHandSpring.enableCollision = true;
		leftHandSpring.spring = 8f;
		leftHand.transform.position = leftHandPos;
		//TODO: Fine tune punch region positions
	}

	private void EnableUserInput(){
		InputControl controller = gameObject.AddComponent<InputControl> ();
		controller.characterChest = chest.GetComponent<Rigidbody> ();
		controller.forceConstant = 1000;
		controller.rotationSpeed = 1000;
		controller.horizontalAxis = horizontalAxis;
		controller.verticalAxis = verticalAxis;
	}

	void Update()
	{
		if (head.transform.position.y < 3) {
			Ragdoll.MakeRagdoll (chest.transform.parent.parent.gameObject);
		}
	}

	private string LoadRandomBodyPath()
	{
		return Path.GetFileName(characterDirs[Random.Range (0, characterDirs.Length)]);
	}

	private Object LoadRandomBodyPart(BodyParts bodyPart)
	{	
		string character = LoadRandomBodyPath ();
		print ("character = " + character + ", bodyPart = " + bodyPart);
		return Resources.Load(charactersDir + character + "/" + bodyPart);
	}

	private GameObject InstantiateBodyPart(Object bodyPart, GameObject parent)
	{
		if (bodyPart == null)
		{
			return null;
		}
		GameObject bodyPartInst = Instantiate((GameObject)bodyPart);
		bodyPartInst.transform.parent = parent.transform;
		bodyPartInst.transform.rotation = parent.transform.rotation;
		return bodyPartInst;
	}

	private void attach(GameObject objectToAttach, GameObject attachToObject, Transform attachToTransform)
	{
		Joint joint = objectToAttach.GetComponent<Joint>();
		objectToAttach.GetComponent<Rigidbody> ().mass = 0.001f;
		objectToAttach.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
		joint.connectedBody = attachToObject.GetComponent<Rigidbody>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedAnchor = attachToTransform.localPosition;

		objectToAttach.transform.localPosition = attachToTransform.localPosition - joint.anchor / 2;
	}

	private void attachSliceable(GameObject objectToAttach, GameObject attachToSliceable, Transform attachToTransform)
	{
		attach (objectToAttach, attachToSliceable, attachToTransform);
		attachToSliceable.AddComponent<Sliceable> ();
		attachToSliceable.AddComponent<JointDestroyerSliceable> ();
		attachToSliceable.GetComponent<JointDestroyerSliceable> ().connector = objectToAttach;
	}

	public float GetLongest(Vector3 vector){
		float longest = vector.x;
		if (vector.y > longest) {
			longest = vector.y;
		}
		if (vector.z > longest) {
			longest = vector.z;
		}
		return longest;
	}

	public bool IsQuadruped(){
		return isQuadruped;
	}
}