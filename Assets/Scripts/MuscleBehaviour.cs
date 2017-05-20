using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MuscleBehaviour : MonoBehaviour
{
    public Rigidbody body, rightBackFoot, leftBackFoot;

    public Rigidbody rightBackUpperHamstring, rightBackLowerHamstring, leftBackUpperHamstring, leftBackLowerHamstring;

    public Rigidbody rightBackUpperChest, rightBackLowerChest,
       leftBackUpperChest, leftBackLowerChest;

    public Rigidbody rightBackUpperGluteus, rightBackLowerGluteus,
        leftBackUpperGluteus, leftBackLowerGluteus;

    public Rigidbody rightBackUpperGastro, rightBackLowerGastro,leftBackUpperGastro, leftBackLowerGastro;

    public Rigidbody rightBackUpperTibialis, rightBackLowerTibialis,
        leftBackUpperTibialis, leftBackLowerTibialis;

    private Muscle rightBackHamstring, leftBackHamstring, rightBackChest, leftBackChest,
      	rightBackGluteus, leftBackGluteus, rightBackGastro, leftBackGastro,
        rightBackTibialis, leftBackTibialis, rightBackAbductor, leftBackAbductor, 
        rightBackAdductor, leftBackAdductor;

	public float hip;
	public float knee;
	public float ankle;

    // Use this for initialization
    void Start()
    {
        
        rightBackHamstring = new Muscle(rightBackUpperHamstring, rightBackLowerHamstring);
        leftBackHamstring = new Muscle(leftBackUpperHamstring, leftBackLowerHamstring);

        rightBackChest = new Muscle(rightBackUpperChest, rightBackLowerChest);
        leftBackChest = new Muscle(leftBackUpperChest, leftBackLowerChest);

        rightBackGluteus = new Muscle(rightBackUpperGluteus, rightBackLowerGluteus);
        leftBackGluteus = new Muscle(leftBackUpperGluteus, leftBackLowerGluteus);

        rightBackGastro = new Muscle(rightBackUpperGastro, rightBackLowerGastro);
        leftBackGastro = new Muscle(leftBackUpperGastro, leftBackLowerGastro);

        rightBackTibialis = new Muscle(rightBackUpperTibialis, rightBackLowerTibialis);
        leftBackTibialis = new Muscle(leftBackUpperTibialis, leftBackLowerTibialis);
	
    }
    // Skriv t.ex rightBackChest.MoveMuscle(1000) om du vill kontrahera rightBackChest med kraft 1000.
    // Om det istället hade stått leftBackGluteus.MoveMuscle(-2000) hade leftBackGluteus sträckts ut med kraft 2000.
    void Update(){
		
		if(Input.GetKey("h")){
			rightBackGluteus.MoveMuscle (hip);
			leftBackGluteus.MoveMuscle (hip);
			rightBackChest.MoveMuscle (-hip);
			leftBackChest.MoveMuscle (-hip);
		}else if(Input.GetKey("j")){
			rightBackHamstring.MoveMuscle (knee);
			leftBackHamstring.MoveMuscle (knee);
		}
		else if(Input.GetKey("k")){
			rightBackGastro.MoveMuscle (ankle);
			leftBackGastro.MoveMuscle (ankle);
			rightBackTibialis.MoveMuscle (-ankle);
			leftBackTibialis.MoveMuscle (-ankle);
		}

		if(Input.GetKey("y")){
			rightBackGluteus.MoveMuscle (-hip);
			leftBackGluteus.MoveMuscle (-hip);
			rightBackChest.MoveMuscle (hip);
			leftBackChest.MoveMuscle (hip);
		}else if(Input.GetKey("u")){
			rightBackHamstring.MoveMuscle (-knee);
			leftBackHamstring.MoveMuscle (-knee);
		}
		else if(Input.GetKey("i")){
			rightBackGastro.MoveMuscle (-ankle);
			leftBackGastro.MoveMuscle (-ankle);
			rightBackTibialis.MoveMuscle (ankle);
			leftBackTibialis.MoveMuscle (ankle);
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
}
