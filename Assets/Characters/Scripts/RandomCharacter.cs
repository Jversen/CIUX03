using UnityEngine;

public class RandomCharacter : MonoBehaviour
{
    private Object[] chests;
    private Object[] heads;
    private Object[] legsLeft;
    private Object[] legsRight;
    private Object[] armsLeft;
    private Object[] armsRight;

    private Object chestPrefab;
    private Object headPrefab;
    private Object leftLegPrefab;
    private Object rightLegPrefab;
    private Object leftArmPrefab;
    private Object rightArmPrefab;

    // GameObjects
    private GameObject chest;
    private GameObject head;
    private GameObject leftLeg;
    private GameObject rightLeg;
    private GameObject leftArm;
    private GameObject rightArm;

    void Start()
    {
        chest = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Chests"), transform);
        head = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Heads"), chest.transform);
        leftLeg = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Legs Left"), chest.transform);
        rightLeg = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Legs Right"), chest.transform);
        leftArm = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Arms Left"), chest.transform);
        rightArm = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Arms Right"), chest.transform);

        chest.transform.localPosition = Vector3.zero;

        attach(head, chest, chest.transform.Find("Head Socket"));
        attach(leftLeg, chest, chest.transform.Find("Left Leg Socket"));
        attach(rightLeg, chest, chest.transform.Find("Right Leg Socket"));
        attach(leftArm, chest, chest.transform.Find("Left Arm Socket"));
        attach(rightArm, chest, chest.transform.Find("Right Arm Socket"));

        // Freeze chest position for testing
        //chest.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

    void Update()
    {

    }

    private Object RandomPrefab(string folder)
    {
        Object[] objects = Resources.LoadAll(folder);
        return objects[Random.Range(0, objects.Length)];
    }

    private GameObject InstantiateBodyPart(Object bodyPart, Transform transform)
    {
        GameObject bodyPartInst = Instantiate((GameObject)bodyPart);
        bodyPartInst.transform.parent = transform;
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