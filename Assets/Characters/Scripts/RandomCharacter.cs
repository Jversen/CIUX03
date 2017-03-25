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

    // Joints
    Joint leftLegJoint;
    Joint rightLegJoint;

    void Start()
    {
        chest = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Chests"));
        head = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Heads"));
        leftLeg = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Legs Left"));
        rightLeg = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Legs Right"));
        leftArm = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Arms Left"));
        rightArm = InstantiateBodyPart(RandomPrefab("Prefabs/Body Parts/Arms Right"));

        chest.transform.localPosition = Vector3.zero;

        leftLegJoint = leftLeg.GetComponent<Joint>();
        rightLegJoint = rightLeg.GetComponent<Joint>();

        leftLegJoint.connectedBody = chest.GetComponent<Rigidbody>();
        Transform leftLegSocket = chest.transform.Find("Left Leg Socket");
        leftLegJoint.connectedAnchor = leftLegSocket.localPosition;

        rightLegJoint.connectedBody = chest.GetComponent<Rigidbody>();
        Transform rightLegSocket = chest.transform.Find("Right Leg Socket");
        rightLegJoint.connectedAnchor = rightLegSocket.localPosition;
    }

    void Update()
    {

    }

    private Object RandomPrefab(string folder)
    {
        Object[] objects = Resources.LoadAll(folder);
        return objects[Random.Range(0, objects.Length)];
    }

    private GameObject InstantiateBodyPart(Object bodyPart)
    {
        GameObject bodyPartInst = Instantiate((GameObject)bodyPart);
        bodyPartInst.transform.parent = transform;
        return bodyPartInst;
    }

}