using System.IO;
using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    enum BodyParts { Chest, Head, LeftUpperArm, RightUpperArm, LeftLowerArm, RightLowerArm, LeftHand, RightHand, LeftUpperLeg, RightUpperLeg, LeftLowerLeg, RightLowerLeg, LeftFoot, RightFoot };

    // Body Parts
    private string charactersDir = "Prefabs/Characters/";
    private string characterPath;
    private string[] characterDirs;

    private GameObject chest;
    private GameObject head;
    private GameObject leftUpperArm;
    private GameObject rightUpperArm;
    private GameObject leftUpperLeg;
    private GameObject rightUpperLeg;
    private GameObject leftLowerLeg;
    private GameObject rightLowerLeg;

    void Start()
    {
        characterPath = Application.dataPath + "/Resources/" + charactersDir;
        characterDirs = Directory.GetDirectories(characterPath);

        chest = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Chest), gameObject);
        head = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.Head), chest);
        leftUpperArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperArm), chest);
        rightUpperArm = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperArm), chest);
        leftUpperLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftUpperLeg), chest);
        rightUpperLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightUpperLeg), chest);
        leftLowerLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.LeftLowerLeg), leftUpperLeg);
        rightLowerLeg = InstantiateBodyPart(LoadRandomBodyPart(BodyParts.RightLowerLeg), rightUpperLeg);

        chest.transform.localPosition = Vector3.zero;

        attach(head, chest, chest.transform.Find("Head Socket"));
        attach(leftUpperArm, chest, chest.transform.Find("Left Arm Socket"));
        attach(rightUpperArm, chest, chest.transform.Find("Right Arm Socket"));
        attach(leftUpperLeg, chest, chest.transform.Find("Left Leg Socket"));
        attach(rightUpperLeg, chest, chest.transform.Find("Right Leg Socket"));
        attach(leftLowerLeg, leftUpperLeg, leftUpperLeg.transform.Find("Lower Leg Socket"));
        attach(rightLowerLeg, rightUpperLeg, rightUpperLeg.transform.Find("Lower Leg Socket"));

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

}