using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MuscleBehaviour : MonoBehaviour
{
    public Rigidbody body, rightFrontFoot, rightBackFoot, leftFrontFoot, leftBackFoot;

    public Rigidbody rightFrontUpperHamstring, rightFrontLowerHamstring, rightBackUpperHamstring, rightBackLowerHamstring,
        leftFrontUpperHamstring, leftFrontLowerHamstring, leftBackUpperHamstring, leftBackLowerHamstring;

    public Rigidbody rightFrontUpperChest, rightFrontLowerChest, rightBackUpperChest, rightBackLowerChest,
        leftFrontUpperChest, leftFrontLowerChest, leftBackUpperChest, leftBackLowerChest;

    public Rigidbody rightFrontUpperGluteus, rightFrontLowerGluteus, rightBackUpperGluteus, rightBackLowerGluteus,
        leftFrontUpperGluteus, leftFrontLowerGluteus, leftBackUpperGluteus, leftBackLowerGluteus;

    public Rigidbody rightFrontUpperGastro, rightFrontLowerGastro, rightBackUpperGastro, rightBackLowerGastro,
        leftFrontUpperGastro, leftFrontLowerGastro, leftBackUpperGastro, leftBackLowerGastro;

    public Rigidbody rightFrontUpperTibialis, rightFrontLowerTibialis, rightBackUpperTibialis, rightBackLowerTibialis,
        leftFrontUpperTibialis, leftFrontLowerTibialis, leftBackUpperTibialis, leftBackLowerTibialis;

    public Rigidbody rightFrontUpperAbductor, rightFrontLowerAbductor, rightBackUpperAbductor, rightBackLowerAbductor,
        leftFrontUpperAbductor, leftFrontLowerAbductor, leftBackUpperAbductor, leftBackLowerAbductor;

    public Rigidbody rightFrontUpperAdductor, rightFrontLowerAdductor, rightBackUpperAdductor, rightBackLowerAdductor,
        leftFrontUpperAdductor, leftFrontLowerAdductor, leftBackUpperAdductor, leftBackLowerAdductor;

    private Muscle rightFrontHamstring, rightBackHamstring, leftFrontHamstring, leftBackHamstring, rightFrontChest, rightBackChest, leftFrontChest, leftBackChest,
        rightFrontGluteus, rightBackGluteus, leftFrontGluteus, leftBackGluteus, rightFrontGastro, rightBackGastro, leftFrontGastro, leftBackGastro,
        rightFrontTibialis, rightBackTibialis, leftFrontTibialis, leftBackTibialis, rightFrontAbductor, rightBackAbductor, leftFrontAbductor, leftBackAbductor, 
        rightFrontAdductor, rightBackAdductor, leftFrontAdductor, leftBackAdductor;

    private MuscleGroup hamstring, chest, gluteus, gastro, tibialis, abductor, adductor;

	public float hip;
	public float ankle;

    // Use this for initialization
    void Start()
    {
        
        rightFrontHamstring = new Muscle(rightFrontUpperHamstring, rightFrontLowerHamstring);
        rightBackHamstring = new Muscle(rightBackUpperHamstring, rightBackLowerHamstring);
        leftFrontHamstring = new Muscle(leftFrontUpperHamstring, leftFrontLowerHamstring);
        leftBackHamstring = new Muscle(leftBackUpperHamstring, leftBackLowerHamstring);

        rightFrontChest = new Muscle(rightFrontUpperChest, rightFrontLowerChest);
        rightBackChest = new Muscle(rightBackUpperChest, rightBackLowerChest);
        leftFrontChest = new Muscle(leftFrontUpperChest, leftFrontLowerChest);
        leftBackChest = new Muscle(leftBackUpperChest, leftBackLowerChest);

        rightFrontGluteus = new Muscle(rightFrontUpperGluteus, rightFrontLowerGluteus);
        rightBackGluteus = new Muscle(rightBackUpperGluteus, rightBackLowerGluteus);
        leftFrontGluteus = new Muscle(leftFrontUpperGluteus, leftFrontLowerGluteus);
        leftBackGluteus = new Muscle(leftBackUpperGluteus, leftBackLowerGluteus);

        rightFrontGastro = new Muscle(rightFrontUpperGastro, rightFrontLowerGastro);
        rightBackGastro = new Muscle(rightBackUpperGastro, rightBackLowerGastro);
        leftFrontGastro = new Muscle(leftFrontUpperGastro, leftFrontLowerGastro);
        leftBackGastro = new Muscle(leftBackUpperGastro, leftBackLowerGastro);

        rightFrontTibialis = new Muscle(rightFrontUpperTibialis, rightFrontLowerTibialis);
        rightBackTibialis = new Muscle(rightBackUpperTibialis, rightBackLowerTibialis);
        leftFrontTibialis = new Muscle(leftFrontUpperTibialis, leftFrontLowerTibialis);
        leftBackTibialis = new Muscle(leftBackUpperTibialis, leftBackLowerTibialis);

        rightFrontAbductor = new Muscle(rightFrontUpperAbductor, rightFrontLowerAbductor);
        rightBackAbductor = new Muscle(rightBackUpperAbductor, rightBackLowerAbductor);
        leftFrontAbductor = new Muscle(leftFrontUpperAbductor, leftFrontLowerAbductor);
        leftBackAbductor = new Muscle(leftBackUpperAbductor, leftBackLowerAbductor);

        rightFrontAdductor = new Muscle(rightFrontUpperAdductor, rightFrontLowerAdductor);
        rightBackAdductor = new Muscle(rightBackUpperAdductor, rightBackLowerAdductor);
        leftFrontAdductor = new Muscle(leftFrontUpperAdductor, leftFrontLowerAdductor);
        leftBackAdductor = new Muscle(leftBackUpperAdductor, leftBackLowerAdductor);

        hamstring = new MuscleGroup(rightFrontHamstring, rightBackHamstring, leftFrontHamstring, leftBackHamstring);
        chest = new MuscleGroup(rightFrontChest, rightBackChest, leftFrontChest, leftBackChest);
        gluteus = new MuscleGroup(rightFrontGluteus, rightBackGluteus, leftFrontGluteus, leftBackGluteus);
        gastro = new MuscleGroup(rightFrontGastro, rightBackGastro, leftFrontGastro, leftBackGastro);
        tibialis = new MuscleGroup(rightFrontTibialis, rightBackTibialis, leftFrontTibialis, leftBackTibialis);
        abductor = new MuscleGroup(rightFrontAbductor, rightBackAbductor, leftFrontAbductor, leftBackAbductor);
        adductor = new MuscleGroup(rightFrontAdductor, rightBackAdductor, leftFrontAdductor, leftBackAdductor);

	
    }
    // Skriv t.ex rightBackChest.MoveMuscle(1000) om du vill kontrahera rightBackChest med kraft 1000.
    // Om det istället hade stått leftBackGluteus.MoveMuscle(-2000) hade leftBackGluteus sträckts ut med kraft 2000.
    void Update()
    {

	MoveHip(hip, "right_front");
        MoveHip(hip, "right_back");
        MoveHip(hip, "left_front");
        MoveHip(hip, "left_back");

	MoveAnkle(ankle, "right_front");
        MoveAnkle(ankle, "right_back");
        MoveAnkle(ankle, "left_front");
        MoveAnkle(ankle, "left_back");
    }

    private void MoveHip(float force, string side)
    {
        if (side == "right_front")
        {
            if (force < 0)
            {
                rightFrontGluteus.MoveMuscle(-force);
            }
            else
            {
                rightFrontChest.MoveMuscle(force);
            }
        }
        else if (side == "right_back")
        {
            if (force < 0)
            {
                rightBackGluteus.MoveMuscle(-force);
            }
            else
            {
                rightBackChest.MoveMuscle(force);
            }
        }
        else if (side == "left_front")
        {
            if (force < 0)
            {
                leftFrontGluteus.MoveMuscle(-force);
            }
            else
            {
                leftFrontChest.MoveMuscle(force);
            }
        }
        else if (side == "left_back")
        {
            if (force < 0)
            {
                leftBackGluteus.MoveMuscle(-force);
            }
            else
            {
                leftBackChest.MoveMuscle(force);
            }
        }
    }
    private void MoveAdductor(float force, string side)
    {
        if (side == "right_front")
        {
            if (force < 0)
            {
                rightFrontAbductor.MoveMuscle(-force);
            }
            else
            {
                rightFrontAdductor.MoveMuscle(force);
            }
        }
        else if (side == "right_back")
        {
            if (force < 0)
            {
                rightBackAbductor.MoveMuscle(-force);
            }
            else
            {
                rightBackAdductor.MoveMuscle(force);
            }
        }
        else if (side == "left_front")
        {
            if (force < 0)
            {
                leftFrontAbductor.MoveMuscle(-force);
            }
            else
            {
                leftFrontAdductor.MoveMuscle(force);
            }
        }
        else if (side == "left_back")
        {
            if (force < 0)
            {
                leftBackAbductor.MoveMuscle(-force);
            }
            else
            {
                leftBackAdductor.MoveMuscle(force);
            }
        }
    }
    private void MoveAnkle(float force, string side)
    {
        if (side == "right_front")
        {
            if (force < 0)
            {
                rightFrontGastro.MoveMuscle(-force);
            }
            else
            {
                rightFrontTibialis.MoveMuscle(force);
            }
        }
        else if (side == "right_back")
        {
            if (force < 0)
            {
                rightBackGastro.MoveMuscle(-force);
            }
            else
            {
                rightBackTibialis.MoveMuscle(force);
            }
        }
        else if (side == "left_front")
        {
            if (force < 0)
            {
                leftFrontGastro.MoveMuscle(-force);
            }
            else
            {
                leftFrontTibialis.MoveMuscle(force);
            }
        }
        else if (side == "left_back")
        {
            if (force < 0)
            {
                leftBackGastro.MoveMuscle(-force);
            }
            else
            {
                leftBackTibialis.MoveMuscle(force);
            }
        }
    }
    private void MoveKnee(float force, string side)
    {
        if (side == "right_front")
        {
            rightFrontHamstring.MoveMuscle(force);
        }
        else if (side == "right_back")
        {
            rightBackHamstring.MoveMuscle(force);
        }
        else if (side == "left_front")
        {
            leftFrontHamstring.MoveMuscle(force);
        }
        else if (side == "left_back")
        {
            leftBackHamstring.MoveMuscle(force);
        }
    }
}
public class Muscle{
    private Rigidbody upper, lower;
    public Muscle(Rigidbody upper, Rigidbody lower)
    {
        this.upper = upper;
        this.lower = lower;
    }
    public void MoveMuscle(float force)
    {
        Vector3 direction1 = upper.transform.position - lower.transform.position;
        direction1.Normalize();
        direction1 = direction1 * force;
        Vector3 direction2 = -direction1;
        upper.AddForce(direction2 * Time.deltaTime);
        lower.AddForce(direction1 * Time.deltaTime);
    }
}

public class MuscleGroup{

    private Muscle frontRight, backRight, frontLeft, backLeft;
    public MuscleGroup(Muscle frontRight, Muscle backRight, Muscle frontLeft, Muscle backLeft)
    {
        this.frontRight = frontRight;
        this.backRight = backRight;
        this.frontLeft = frontLeft;
        this.backLeft = backLeft;
    }
    public void MoveAll(float force)
    {
        frontRight.MoveMuscle(force);
        backRight.MoveMuscle(force);
        frontLeft.MoveMuscle(force);
        backLeft.MoveMuscle(force);
    }
    public void MoveFront(float force)
    {
        frontRight.MoveMuscle(force);
        frontLeft.MoveMuscle(force);
    }
    public void MoveBack(float force)
    {
        backRight.MoveMuscle(force);
        backLeft.MoveMuscle(force);
    }
    public void MoveRight(float force)
    {
        frontRight.MoveMuscle(force);
        backRight.MoveMuscle(force);
    }
    public void MoveLeft(float force)
    {
        frontLeft.MoveMuscle(force);
        backLeft.MoveMuscle(force);
    }
}

