using System.IO;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    enum BodyParts { Chest, Head, LeftArm, RightArm, LeftUpperLeg, RightUpperLeg, LeftLowerLeg, RightLowerLeg, LeftFoot, RightFoot, Tail };
	public float bodyPartMass;
	private bool isQuadruped;
	// Body Parts
    private string charactersDir = "Prefabs/Characters/";
    private string characterPath;
    private string[] characterDirs;

	public string horizontalAxis;
	public string verticalAxis;

    private GameObject chest;
    private GameObject head;
    private GameObject leftUpperArm;
    private GameObject rightUpperArm;
    private GameObject leftLowerArm;
    private GameObject rightLowerArm;
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject leftUpperLeg;
    private GameObject rightUpperLeg;
    private GameObject leftLowerLeg;
    private GameObject rightLowerLeg;
    private GameObject leftFoot;
    private GameObject rightFoot;
	private GameObject tail;

	private GameObject leftArm;
	private GameObject rightArm;

	private GameObject leftParent;
	private GameObject rightParent;

	private GameObject leftFistHome;
	private GameObject leftFistAway;

    void Start()
    {
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
        head = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Head), chest);
		leftArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftArm), chest);
		rightArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightArm), chest);
        //leftUpperArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperArm), chest);
        //rightUpperArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperArm), chest);
        //leftLowerArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftLowerArm), leftUpperArm);
        //rightLowerArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightLowerArm), rightUpperArm);
        //leftHand = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftHand), leftLowerArm);
        //rightHand = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightHand), rightLowerArm);
		string upperLegPath = LoadRandomBodyPath ();
		leftUpperLeg = InstantiateBodyPart(Resources.Load(charactersDir + upperLegPath + "/" + BodyParts.LeftUpperLeg), chest);
		rightUpperLeg = InstantiateBodyPart(Resources.Load(charactersDir + upperLegPath + "/" + BodyParts.RightUpperLeg), chest);
		string lowerLegPath = LoadRandomBodyPath ();
		leftLowerLeg = InstantiateBodyPart(Resources.Load(charactersDir + lowerLegPath + "/" + BodyParts.LeftLowerLeg), leftUpperLeg);
		rightLowerLeg = InstantiateBodyPart(Resources.Load(charactersDir + lowerLegPath + "/" + BodyParts.RightLowerLeg), rightUpperLeg);
		string footPath = LoadRandomBodyPath ();
		leftFoot = InstantiateBodyPart(Resources.Load(charactersDir + footPath + "/" + BodyParts.LeftFoot), leftLowerLeg);
		rightFoot = InstantiateBodyPart(Resources.Load(charactersDir + footPath + "/" + BodyParts.RightFoot), rightLowerLeg);

		//APPLYING PUNCHING SCRIPT
		rightParent = rightArm.transform.parent.gameObject.transform.parent.gameObject;
		PunchingScript punchingScript = rightParent.AddComponent<PunchingScript>();
		//TODO: The variable 'fist' in PunchingScript should be the parent

		//Set the mass of the upper body to bodyPartMass
		ChangeMassOfUpperBody();
			
		UpperBodyPID upperBodyPID = chest.AddComponent<UpperBodyPID> ();
		upperBodyPID.isQuadruped = isQuadruped;

		Object tailObj = LoadRandomBodyPart(BodyParts.Tail);
        if (tailObj != null)
        {
            tail = InstantiateBodyPart(tailObj, chest);
			tail.GetComponent<Rigidbody> ().mass = bodyPartMass;
        }

        chest.transform.localPosition = Vector3.zero;

        // Attach joints
        attach(head, chest, chest.transform.Find("Head Socket"));
		attach(leftArm, chest, chest.transform.Find("Left Arm Socket"));
		attach(rightArm, chest, chest.transform.Find("Right Arm Socket"));
        //attach(leftUpperArm, chest, chest.transform.Find("Left Arm Socket"));
        //attach(rightUpperArm, chest, chest.transform.Find("Right Arm Socket"));
        //attachSliceable(leftLowerArm, leftUpperArm, leftUpperArm.transform.Find("Lower Arm Socket"));
		//attachSliceable(rightLowerArm, rightUpperArm, rightUpperArm.transform.Find("Lower Arm Socket"));
		//attachSliceable(leftHand, leftLowerArm, leftLowerArm.transform.Find("Hand Socket"));
		//attachSliceable(rightHand, rightLowerArm, rightLowerArm.transform.Find("Hand Socket"));
		attach(leftUpperLeg, chest, chest.transform.Find("Left Leg Socket"));
		attach(rightUpperLeg, chest, chest.transform.Find("Right Leg Socket"));
		attachSliceable(leftLowerLeg, leftUpperLeg, leftUpperLeg.transform.Find("Lower Leg Socket"));
		attachSliceable(rightLowerLeg, rightUpperLeg, rightUpperLeg.transform.Find("Lower Leg Socket"));
		attachSliceable(leftFoot, leftLowerLeg, leftLowerLeg.transform.Find("Foot Socket"));
		attachSliceable(rightFoot, rightLowerLeg, rightLowerLeg.transform.Find("Foot Socket"));

        if (tail != null)
        {
            attach(tail, chest, chest.transform.Find("Tail Socket"));
        }
		/*
		EnablePunching ();
		EnableUserInput ();
		EnableFootPlacement ();
		*/

		//Make feet rotation horizontal
		float rightFootYRot = rightFoot.transform.rotation.eulerAngles.y;
		rightFoot.transform.rotation = Quaternion.Euler(0, rightFootYRot, 0);
		float leftFootYRot = leftFoot.transform.rotation.eulerAngles.y;
		leftFoot.transform.rotation = Quaternion.Euler(0, leftFootYRot, 0);
		//Move character down to make feet touch the ground.
		Vector3 footHeight = new Vector3 (0, rightFoot.transform.position.y, 0);
		chest.transform.parent.position -= footHeight;

        // Freeze chest position for testing
		//chest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

	private void ChangeMassOfUpperBody(){

		chest.GetComponent<Rigidbody> ().mass = bodyPartMass;
		head.GetComponent<Rigidbody> ().mass = bodyPartMass;

		//If it is not a quadruped it's arms are part of the upper body and thus their masses should be changed;
		if (!isQuadruped) {
			leftArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			rightArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			//leftUpperArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			//rightUpperArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			//leftLowerArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			//rightLowerArm.GetComponent<Rigidbody> ().mass = bodyPartMass;
			//leftHand.GetComponent<Rigidbody> ().mass = bodyPartMass;
			//rightHand.GetComponent<Rigidbody> ().mass = bodyPartMass;
		}

		if (tail != null)
		{
			tail.GetComponent<Rigidbody> ().mass = bodyPartMass;
		}
		
	}
	/*
	//Create two regions near left arm of character for using as spring anchors when punching
	private void EnablePunching(){
		leftFistHome = InstantiateBodyPart (Resources.Load(charactersDir + "TriggerRegion"),chest);
		leftFistAway = InstantiateBodyPart (Resources.Load(charactersDir + "TriggerRegion"),chest);
		PunchingScript punchingScript = leftFistAway.AddComponent<PunchingScript> ();

		punchingScript.fist = leftHand.GetComponent <Rigidbody>();
		punchingScript.home = leftFistHome.GetComponent<Rigidbody>();

		float upperArmLength = GetLongest (leftArm.GetComponent<Renderer> ().bounds.size);
		float armLength = GetLongest (leftArm.GetComponent<Renderer> ().bounds.size) + upperArmLength;

		leftHand.GetComponent<Rigidbody> ().mass = 0.1f;
		leftArm.GetComponent<Rigidbody> ().mass = 0.1f;
		leftArm.GetComponent<Rigidbody> ().mass = 0.1f;

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
	*/

	private void EnableUserInput(){
		InputControl controller = gameObject.AddComponent<InputControl> ();
		controller.characterChest = chest.GetComponent<Rigidbody> ();
		controller.forceConstant = 1000;
		controller.rotationSpeed = 1000;
		controller.horizontalAxis = horizontalAxis;
		controller.verticalAxis = verticalAxis;
	}

	private void EnableFootPlacement(){
		PIDControl leftLowerLegPidControl = gameObject.AddComponent<PIDControl> ();
		leftLowerLegPidControl.body = leftLowerLeg.GetComponent<Rigidbody> ();
		leftLowerLegPidControl.mfConstant = 1500;
		PIDControl leftUpperLegPidControl = gameObject.AddComponent<PIDControl> ();
		leftUpperLegPidControl.body = leftLowerLeg.GetComponent<Rigidbody> ();
		leftUpperLegPidControl.mfConstant = 1500;
		PIDControl rightLowerLegPidControl = gameObject.AddComponent<PIDControl> ();
		rightLowerLegPidControl.body = rightLowerLeg.GetComponent<Rigidbody> ();
		rightLowerLegPidControl.mfConstant = 1500;
		PIDControl rightUpperLegPidControl = gameObject.AddComponent<PIDControl> ();
		rightUpperLegPidControl.body = rightLowerLeg.GetComponent<Rigidbody> ();
		rightUpperLegPidControl.mfConstant = 1500;

		BalanceIntelligence balanceIntelligence = gameObject.AddComponent<BalanceIntelligence> ();

		GameObject footGoalMarker = InstantiateBodyPart (Resources.Load (charactersDir + "TriggerRegion"), chest);

		FootGoalColliderScript footGoalCollider = footGoalMarker.AddComponent<FootGoalColliderScript> ();

		balanceIntelligence.footGoalMarker = footGoalMarker.GetComponent<Rigidbody>();

		balanceIntelligence.rightFoot = rightFoot.GetComponent<Rigidbody> ();
		balanceIntelligence.leftFoot = leftFoot.GetComponent<Rigidbody> ();
		balanceIntelligence.bodyCore = chest.GetComponent<Rigidbody> ();
	}

    void Update()
    {

    }

	private string LoadRandomBodyPath()
	{
		return Path.GetFileName(characterDirs[Random.Range (0, characterDirs.Length)]);
	}

    private Object LoadRandomBodyPart(BodyParts bodyPart)
    {	
		string character = LoadRandomBodyPath ();
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