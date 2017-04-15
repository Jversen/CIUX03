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

        // Freeze chest position for testing
        //chest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
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
}