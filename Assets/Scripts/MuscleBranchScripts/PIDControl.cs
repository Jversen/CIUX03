using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PIDControl : MonoBehaviour {
	 
	    public Rigidbody body;
	    public Rigidbody legWestMusclePoint, footWestMusclePoint, legEastMusclePoint, footEastMusclePoint, 
	    legSouthMusclePoint, footSouthMusclePoint, legNorthMusclePoint, footNorthMusclePoint;
	    private Muscle legWestMuscle, legEastMuscle, legSouthMuscle, legNorthMuscle;
	 
	    public int mfConstant; //Muscle force constant
	 
	    private double inVal;
	    private double inVal2;
	    private double previousInVal;
	    private double previousInVal2;
	    private double outVal;
	    private double outVal2;
	 
	    private double lastOutVal;
	    private double errSum;
	 
	    public int targetVal;
	 
	    // Use this for initialization
	    void Start () {		 
		        inVal = body.rotation.x;
		        previousInVal = inVal;
		        legWestMuscle = new Muscle(legWestMusclePoint, footWestMusclePoint);
		        legEastMuscle = new Muscle(legEastMusclePoint, footEastMusclePoint);
		        legSouthMuscle = new Muscle(legSouthMusclePoint, footSouthMusclePoint);
		        legNorthMuscle = new Muscle(legNorthMusclePoint, footNorthMusclePoint);
		    }
	 
	    // Update is called once per frame
	    void Update () {
		         //legLeftMuscle.MoveMuscle(kp);
		         //legRightMuscle.MoveMuscle(ki);
		 
		        inVal = body.rotation.x;
		        inVal2 = body.rotation.z;
		        double error = targetVal - inVal;
		        double error2 = targetVal - inVal2;
		 
		        double derivative = (inVal - previousInVal) / Time.deltaTime;
		        double derivative2 = (inVal2 - previousInVal2) / Time.deltaTime;
		 
		        outVal = pidFunction (error, Time.deltaTime) - derivative * 20;
		        outVal2 = pidFunction (error2, Time.deltaTime) - derivative2 * 20;
		 
		        /*if (inVal >= 0) {
			            legWestMuscle.MoveMuscle (-mfConstant * (float)outVal);
		        } else {
			            legEastMuscle.MoveMuscle (mfConstant * (float)outVal);
			        }
		 
		        if (inVal2 >= 0) {
			            legSouthMuscle.MoveMuscle (-mfConstant * (float)outVal2);
		        } else {
			            legNorthMuscle.MoveMuscle (mfConstant * (float)outVal2);
			        }*/
		 
		        body.AddRelativeForce(body.transform.forward * (float) outVal + body.transform.right * (float) -outVal2);
		        body.AddRelativeForce(body.transform.right * (float) -outVal2);

		        previousInVal = inVal;
		        previousInVal2 = inVal2;
		 
		    }
	 
	    private double pidFunction(double parameter, double deltaTime){
		        double result = 0;
		        /*if (parameter < 0) {
            result = 1 / (-1 - (deltaTime / parameter));
        } else {
            result = 1 / (1 + (deltaTime / parameter));
        }*/
		        result = (double) (30) * Mathf.Sin ((float) parameter);
		        return result;
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