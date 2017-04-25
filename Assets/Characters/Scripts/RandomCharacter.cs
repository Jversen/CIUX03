using System.IO;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    enum BodyParts { Chest, Head, LeftUpperArm, RightUpperArm, LeftLowerArm, RightLowerArm, LeftHand, RightHand, LeftUpperLeg, RightUpperLeg, LeftLowerLeg, RightLowerLeg, LeftFoot, RightFoot, Tail };

	// Body Parts
    private string charactersDir = "Prefabs/Characters/";
    private string characterPath;
    private string[] characterDirs;

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

	private GameObject leftFistHome;
	private GameObject leftFistAway;

    void Start()
    {

        characterPath = Application.dataPath + "/Resources/" + charactersDir;
        characterDirs = Directory.GetDirectories(characterPath);

        // Instantiate objects
        chest = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Chest), gameObject);
        head = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Head), chest);
        leftUpperArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperArm), chest);
        rightUpperArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperArm), chest);
        leftLowerArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftLowerArm), leftUpperArm);
        rightLowerArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightLowerArm), rightUpperArm);
        leftHand = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftHand), leftLowerArm);
        rightHand = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightHand), rightLowerArm);
        leftUpperLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperLeg), chest);
        rightUpperLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperLeg), chest);
        leftLowerLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftLowerLeg), leftUpperLeg);
        rightLowerLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightLowerLeg), rightUpperLeg);
        leftFoot = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftFoot), leftLowerLeg);
        rightFoot = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightFoot), rightLowerLeg);

        Object tailObj = LoadRandomBodyPart(BodyParts.Tail);
        if (tailObj != null)
        {
            tail = InstantiateBodyPart(tailObj, chest);
        }

        chest.transform.localPosition = Vector3.zero;

        // Attach joints
        attach(head, chest, chest.transform.Find("Head Socket"));
        attach(leftUpperArm, chest, chest.transform.Find("Left Arm Socket"));
        attach(rightUpperArm, chest, chest.transform.Find("Right Arm Socket"));
        attachSliceable(leftLowerArm, leftUpperArm, leftUpperArm.transform.Find("Lower Arm Socket"));
		attachSliceable(rightLowerArm, rightUpperArm, rightUpperArm.transform.Find("Lower Arm Socket"));
		attachSliceable(leftHand, leftLowerArm, leftLowerArm.transform.Find("Hand Socket"));
		attachSliceable(rightHand, rightLowerArm, rightLowerArm.transform.Find("Hand Socket"));
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
			
		EnablePunching ();
		EnableUserInput ();
		EnableFootPlacement ();


        // Freeze chest position for testing
		//chest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

	//Create two regions near left arm of character for using as spring anchors when punching
	private void EnablePunching(){
		leftFistHome = InstantiateBodyPart (Resources.Load(charactersDir + "TriggerRegion"),chest);
		leftFistAway = InstantiateBodyPart (Resources.Load(charactersDir + "TriggerRegion"),chest);
		PunchingScript punchingScript = leftFistAway.AddComponent<PunchingScript> ();

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

    private Object LoadRandomBodyPart(BodyParts bodyPart)
    {
		string character = Path.GetFileName(characterDirs[Random.Range(0, characterDirs.Length)]);
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
}