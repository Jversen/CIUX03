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

    private GameObject chest;
    private GameObject head;
    private GameObject leftLeg;
    private GameObject rightLeg;
    private GameObject leftArm;
    private GameObject rightArm;

    void Start()
    {
        GameObject chest = Instantiate((GameObject)RandomPrefab("Prefabs/Body Parts/Chests"));
        GameObject head = Instantiate((GameObject)RandomPrefab("Prefabs/Body Parts/Heads"));
        GameObject leftLeg = Instantiate((GameObject)RandomPrefab("Prefabs/Body Parts/Legs Left"));
        GameObject rightLeg = Instantiate((GameObject)RandomPrefab("Prefabs/Body Parts/Legs Right"));
        GameObject leftArm = Instantiate((GameObject)RandomPrefab("Prefabs/Body Parts/Arms Left"));
        GameObject rightArm = Instantiate((GameObject)RandomPrefab("Prefabs/Body Parts/Arms Right"));
    }

    void Update()
    {

    }

    private Object RandomPrefab(string folder)
    {
        Object[] objects = Resources.LoadAll(folder);
        return objects[Random.Range(0, objects.Length)];
    }

}