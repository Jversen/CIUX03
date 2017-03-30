using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Init : MonoBehaviour {
    public GameObject walkingTable;
    private float prevTime;
    public GameObject variableHolder;
    public GameObject variableHolderPrefab;
    private ArrayList tables;
    private int n = 250;
    private int d = 15;
    private int frames = 0;
    private bool doneBool = false;
    public float deltaA, deltaW, deltaF;
    private float a1, w1, a2, w2, a3, w3, a4, w4, a5, w5, a6, w6, a7, w7, a8, w8, a9, w9, a10, w10, a11, w11, a12, w12, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12;
    public float a1e, w1e, a2e, w2e, a3e, w3e, a4e, w4e, a5e, w5e, a6e, w6e, a7e, w7e, a8e, w8e, a9e, w9e, a10e, w10e, a11e, w11e, a12e, w12e, a1r, w1r, a2r, w2r, a3r, w3r, a4r, w4r, a5r, w5r, a6r, w6r, a7r, w7r, a8r, w8r, a9r, w9r, a10r, w10r, a11r, w11r, a12r, w12r,
        a1t, w1t, a2t, w2t, a3t, w3t, a4t, w4t, a5t, w5t, a6t, w6t, a7t, w7t, a8t, w8t, a9t, w9t, a10t, w10t, a11t, w11t, a12t, w12t, a1y, w1y, a2y, w2y, a3y, w3y, a4y, w4y, a5y, w5y, a6y, w6y, a7y, w7y, a8y, w8y, a9y, w9y, a10y, w10y, a11y, w11y, a12y, w12y,
        a1u, w1u, a2u, w2u, a3u, w3u, a4u, w4u, a5u, w5u, a6u, w6u, a7u, w7u, a8u, w8u, a9u, w9u, a10u, w10u, a11u, w11u, a12u, w12u, a1i, w1i, a2i, w2i, a3i, w3i, a4i, w4i, a5i, w5i, a6i, w6i, a7i, w7i, a8i, w8i, a9i, w9i, a10i, w10i, a11i, w11i, a12i, w12i,
        a1o, w1o, a2o, w2o, a3o, w3o, a4o, w4o, a5o, w5o, a6o, w6o, a7o, w7o, a8o, w8o, a9o, w9o, a10o, w10o, a11o, w11o, a12o, w12o, a1p, w1p, a2p, w2p, a3p, w3p, a4p, w4p, a5p, w5p, a6p, w6p, a7p, w7p, a8p, w8p, a9p, w9p, a10p, w10p, a11p, w11p, a12p, w12p,
        f1e, f2e, f3e, f4e, f5e, f6e, f7e, f8e, f9e, f10e, f11e, f12e, f1r, f2r, f3r, f4r, f5r, f6r, f7r, f8r, f9r, f10r, f11r, f12r, f1t, f2t, f3t, f4t, f5t, f6t, f7t, f8t, f9t, f10t, f11t, f12t, f1y, f2y, f3y, f4y, f5y, f6y, f7y, f8y, f9y, f10y, f11y, f12y,
        f1u, f2u, f3u, f4u, f5u, f6u, f7u, f8u, f9u, f10u, f11u, f12u, f1i, f2i, f3i, f4i, f5i, f6i, f7i, f8i, f9i, f10i, f11i, f12i, f1o, f2o, f3o, f4o, f5o, f6o, f7o, f8o, f9o, f10o, f11o, f12o, f1p, f2p, f3p, f4p, f5p, f6p, f7p, f8p, f9p, f10p, f11p, f12p;
    // Use this for initialization
    void Start() {
        variableHolder = GameObject.Find("VariableHolder(Clone)");
        if (variableHolder == null)
        {
            variableHolder = Instantiate(variableHolderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        tables = new ArrayList();
        getValues();
        for (int i = 0; i < n; i++)
        {
            int parent = Random.Range(1, 9);

            setValues(parent);

            GameObject table = Instantiate(walkingTable, new Vector3(-(d * n / 2) + i * d, 5, -40), Quaternion.identity);
            MuscleBehaviour tableScript = table.GetComponent<MuscleBehaviour>();
            tableScript.a1 = Random.Range(a1 - deltaA, a1 + deltaA);
            tableScript.a2 = Random.Range(a2 - deltaA, a2 + deltaA);
            tableScript.a3 = Random.Range(a3 - deltaA, a3 + deltaA);
            tableScript.a4 = Random.Range(a4 - deltaA, a4 + deltaA);
            tableScript.a5 = Random.Range(a5 - deltaA, a5 + deltaA);
            tableScript.a6 = Random.Range(a6 - deltaA, a6 + deltaA);
            tableScript.a7 = Random.Range(a7 - deltaA, a7 + deltaA);
            tableScript.a8 = Random.Range(a8 - deltaA, a8 + deltaA);
            tableScript.a9 = Random.Range(a9 - deltaA, a9 + deltaA);
            tableScript.a10 = Random.Range(a10 - deltaA, a10 + deltaA);
            tableScript.a11 = Random.Range(a11 - deltaA, a11 + deltaA);
            tableScript.a12 = Random.Range(a12 - deltaA, a12 + deltaA);

            tableScript.w1 = Random.Range(w1 - deltaW, w1 + deltaW);
            tableScript.w2 = Random.Range(w2 - deltaW, w2 + deltaW);
            tableScript.w3 = Random.Range(w3 - deltaW, w3 + deltaW);
            tableScript.w4 = Random.Range(w4 - deltaW, w4 + deltaW);
            tableScript.w5 = Random.Range(w5 - deltaW, w5 + deltaW);
            tableScript.w6 = Random.Range(w6 - deltaW, w6 + deltaW);
            tableScript.w7 = Random.Range(w7 - deltaW, w7 + deltaW);
            tableScript.w8 = Random.Range(w8 - deltaW, w8 + deltaW);
            tableScript.w9 = Random.Range(w9 - deltaW, w9 + deltaW);
            tableScript.w10 = Random.Range(w10 - deltaW, w10 + deltaW);
            tableScript.w11 = Random.Range(w11 - deltaW, w11 + deltaW);
            tableScript.w12 = Random.Range(w12 - deltaW, w12 + deltaW);

            tableScript.f1 = Random.Range(f1 - deltaF, f1 + deltaF);
            tableScript.f2 = Random.Range(f2 - deltaF, f2 + deltaF);
            tableScript.f3 = Random.Range(f3 - deltaF, f3 + deltaF);
            tableScript.f4 = Random.Range(f4 - deltaF, f4 + deltaF);
            tableScript.f5 = Random.Range(f5 - deltaF, f5 + deltaF);
            tableScript.f6 = Random.Range(f6 - deltaF, f6 + deltaF);
            tableScript.f7 = Random.Range(f7 - deltaF, f7 + deltaF);
            tableScript.f8 = Random.Range(f8 - deltaF, f8 + deltaF);
            tableScript.f9 = Random.Range(f9 - deltaF, f9 + deltaF);
            tableScript.f10 = Random.Range(f10 - deltaF, f10 + deltaF);
            tableScript.f11 = Random.Range(f11 - deltaF, f11 + deltaF);
            tableScript.f12 = Random.Range(f12 - deltaF, f12 + deltaF);

            tables.Add(table);
        }


    }


    // Update is called once per frame
    void Update()
    {
        /*
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("obj"))
        {
            Rigidbody rigid = obj.GetComponent<Rigidbody>();
            if (rigid != null && rigid.velocity.magnitude>10)
            {
                obj.SetActive(false);
            }
        }
        */
        frames++;
        if ((Time.fixedTime - prevTime)>12 && !doneBool)
        {
            for (int i = 1; i < 9; i++)
            {
                float maxZ = -100;
                GameObject maxTable = (GameObject) tables[0];
                MuscleBehaviour maxScript = maxTable.GetComponent<MuscleBehaviour>();
                foreach (GameObject table in tables)
                {
                    MuscleBehaviour tableScript = table.GetComponent<MuscleBehaviour>();
                    float z = tableScript.body.transform.position.z;
                    if (z > maxZ)
                    {
                        maxZ = z;
                        maxScript = tableScript;
                        maxTable = table;
                    }
                }
                if (maxZ > 0)
                {
                    maxTable.name = "Winner";
                    Debug.LogError("finished");
                }
                tables.Remove(maxTable);
                setValues(i, maxScript);    
            }
            
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    private void setValues(int i, MuscleBehaviour script)
    {
        Variables vriblScript = variableHolder.GetComponent<Variables>();
        vriblScript.prevTime = Time.fixedTime;
        float a1 = script.a1;
        float a2 = script.a2;
        float a3 = script.a3;
        float a4 = script.a4;
        float a5 = script.a5;
        float a6 = script.a6;
        float a7 = script.a7;
        float a8 = script.a8;
        float a9 = script.a9;
        float a10 = script.a10;
        float a11 = script.a11;
        float a12 = script.a12;

        float w1 = script.w1;
        float w2 = script.w2;
        float w3 = script.w3;
        float w4 = script.w4;
        float w5 = script.w5;
        float w6 = script.w6;
        float w7 = script.w7;
        float w8 = script.w8;
        float w9 = script.w9;
        float w10 = script.w10;
        float w11 = script.w11;
        float w12 = script.w12;

        float f1 = script.f1;
        float f2 = script.f2;
        float f3 = script.f3;
        float f4 = script.f4;
        float f5 = script.f5;
        float f6 = script.f6;
        float f7 = script.f7;
        float f8 = script.f8;
        float f9 = script.f9;
        float f10 = script.f10;
        float f11 = script.f11;
        float f12 = script.f12;
        if (i == 1)
        {
            vriblScript.a1e = a1;
            vriblScript.a2e = a2;
            vriblScript.a3e = a3;
            vriblScript.a4e = a4;
            vriblScript.a5e = a5;
            vriblScript.a6e = a6;
            vriblScript.a7e = a7;
            vriblScript.a8e = a8;
            vriblScript.a9e = a9;
            vriblScript.a10e = a10;
            vriblScript.a11e = a11;
            vriblScript.a12e = a12;

            vriblScript.w1e = w1;
            vriblScript.w2e = w2;
            vriblScript.w3e = w3;
            vriblScript.w4e = w4;
            vriblScript.w5e = w5;
            vriblScript.w6e = w6;
            vriblScript.w7e = w7;
            vriblScript.w8e = w8;
            vriblScript.w9e = w9;
            vriblScript.w10e = w10;
            vriblScript.w11e = w11;
            vriblScript.w12e = w12;

            vriblScript.f1e = f1;
            vriblScript.f2e = f2;
            vriblScript.f3e = f3;
            vriblScript.f4e = f4;
            vriblScript.f5e = f5;
            vriblScript.f6e = f6;
            vriblScript.f7e = f7;
            vriblScript.f8e = f8;
            vriblScript.f9e = f9;
            vriblScript.f10e = f10;
            vriblScript.f11e = f11;
            vriblScript.f12e = f12;
        }
        else if (i == 2)
        {
            vriblScript.a1r = a1;
            vriblScript.a2r = a2;
            vriblScript.a3r = a3;
            vriblScript.a4r = a4;
            vriblScript.a5r = a5;
            vriblScript.a6r = a6;
            vriblScript.a7r = a7;
            vriblScript.a8r = a8;
            vriblScript.a9r = a9;
            vriblScript.a10r = a10;
            vriblScript.a11r = a11;
            vriblScript.a12r = a12;

            vriblScript.w1r = w1;
            vriblScript.w2r = w2;
            vriblScript.w3r = w3;
            vriblScript.w4r = w4;
            vriblScript.w5r = w5;
            vriblScript.w6r = w6;
            vriblScript.w7r = w7;
            vriblScript.w8r = w8;
            vriblScript.w9r = w9;
            vriblScript.w10r = w10;
            vriblScript.w11r = w11;
            vriblScript.w12r = w12;

            vriblScript.f1r = f1;
            vriblScript.f2r = f2;
            vriblScript.f3r = f3;
            vriblScript.f4r = f4;
            vriblScript.f5r = f5;
            vriblScript.f6r = f6;
            vriblScript.f7r = f7;
            vriblScript.f8r = f8;
            vriblScript.f9r = f9;
            vriblScript.f10r = f10;
            vriblScript.f11r = f11;
            vriblScript.f12r = f12;
        }
        else if(i == 3)
        {
            vriblScript.a1t = a1;
            vriblScript.a2t = a2;
            vriblScript.a3t = a3;
            vriblScript.a4t = a4;
            vriblScript.a5t = a5;
            vriblScript.a6t = a6;
            vriblScript.a7t = a7;
            vriblScript.a8t = a8;
            vriblScript.a9t = a9;
            vriblScript.a10t = a10;
            vriblScript.a11t = a11;
            vriblScript.a12t = a12;

            vriblScript.w1t = w1;
            vriblScript.w2t = w2;
            vriblScript.w3t = w3;
            vriblScript.w4t = w4;
            vriblScript.w5t = w5;
            vriblScript.w6t = w6;
            vriblScript.w7t = w7;
            vriblScript.w8t = w8;
            vriblScript.w9t = w9;
            vriblScript.w10t = w10;
            vriblScript.w11t = w11;
            vriblScript.w12t = w12;

            vriblScript.f1t = f1;
            vriblScript.f2t = f2;
            vriblScript.f3t = f3;
            vriblScript.f4t = f4;
            vriblScript.f5t = f5;
            vriblScript.f6t = f6;
            vriblScript.f7t = f7;
            vriblScript.f8t = f8;
            vriblScript.f9t = f9;
            vriblScript.f10t = f10;
            vriblScript.f11t = f11;
            vriblScript.f12t = f12;
        }
        else if (i == 4)
        {
            vriblScript.a1y = a1;
            vriblScript.a2y = a2;
            vriblScript.a3y = a3;
            vriblScript.a4y = a4;
            vriblScript.a5y = a5;
            vriblScript.a6y = a6;
            vriblScript.a7y = a7;
            vriblScript.a8y = a8;
            vriblScript.a9y = a9;
            vriblScript.a10y = a10;
            vriblScript.a11y = a11;
            vriblScript.a12y = a12;

            vriblScript.w1y = w1;
            vriblScript.w2y = w2;
            vriblScript.w3y = w3;
            vriblScript.w4y = w4;
            vriblScript.w5y = w5;
            vriblScript.w6y = w6;
            vriblScript.w7y = w7;
            vriblScript.w8y = w8;
            vriblScript.w9y = w9;
            vriblScript.w10y = w10;
            vriblScript.w11y = w11;
            vriblScript.w12y = w12;

            vriblScript.f1y = f1;
            vriblScript.f2y = f2;
            vriblScript.f3y = f3;
            vriblScript.f4y = f4;
            vriblScript.f5y = f5;
            vriblScript.f6y = f6;
            vriblScript.f7y = f7;
            vriblScript.f8y = f8;
            vriblScript.f9y = f9;
            vriblScript.f10y = f10;
            vriblScript.f11y = f11;
            vriblScript.f12y = f12;
        }
        else if (i == 5)
        {
            vriblScript.a1u = a1;
            vriblScript.a2u = a2;
            vriblScript.a3u = a3;
            vriblScript.a4u = a4;
            vriblScript.a5u = a5;
            vriblScript.a6u = a6;
            vriblScript.a7u = a7;
            vriblScript.a8u = a8;
            vriblScript.a9u = a9;
            vriblScript.a10u = a10;
            vriblScript.a11u = a11;
            vriblScript.a12u = a12;

            vriblScript.w1u = w1;
            vriblScript.w2u = w2;
            vriblScript.w3u = w3;
            vriblScript.w4u = w4;
            vriblScript.w5u = w5;
            vriblScript.w6u = w6;
            vriblScript.w7u = w7;
            vriblScript.w8u = w8;
            vriblScript.w9u = w9;
            vriblScript.w10u = w10;
            vriblScript.w11u = w11;
            vriblScript.w12u = w12;

            vriblScript.f1u = f1;
            vriblScript.f2u = f2;
            vriblScript.f3u = f3;
            vriblScript.f4u = f4;
            vriblScript.f5u = f5;
            vriblScript.f6u = f6;
            vriblScript.f7u = f7;
            vriblScript.f8u = f8;
            vriblScript.f9u = f9;
            vriblScript.f10u = f10;
            vriblScript.f11u = f11;
            vriblScript.f12u = f12;
        }
        else if (i == 6)
        {
            vriblScript.a1i = a1;
            vriblScript.a2i = a2;
            vriblScript.a3i = a3;
            vriblScript.a4i = a4;
            vriblScript.a5i = a5;
            vriblScript.a6i = a6;
            vriblScript.a7i = a7;
            vriblScript.a8i = a8;
            vriblScript.a9i = a9;
            vriblScript.a10i = a10;
            vriblScript.a11i = a11;
            vriblScript.a12i = a12;

            vriblScript.w1i = w1;
            vriblScript.w2i = w2;
            vriblScript.w3i = w3;
            vriblScript.w4i = w4;
            vriblScript.w5i = w5;
            vriblScript.w6i = w6;
            vriblScript.w7i = w7;
            vriblScript.w8i = w8;
            vriblScript.w9i = w9;
            vriblScript.w10i = w10;
            vriblScript.w11i = w11;
            vriblScript.w12i = w12;

            vriblScript.f1i = f1;
            vriblScript.f2i = f2;
            vriblScript.f3i = f3;
            vriblScript.f4i = f4;
            vriblScript.f5i = f5;
            vriblScript.f6i = f6;
            vriblScript.f7i = f7;
            vriblScript.f8i = f8;
            vriblScript.f9i = f9;
            vriblScript.f10i = f10;
            vriblScript.f11i = f11;
            vriblScript.f12i = f12;
        }
        else if (i == 7)
        {
            vriblScript.a1o = a1;
            vriblScript.a2o = a2;
            vriblScript.a3o = a3;
            vriblScript.a4o = a4;
            vriblScript.a5o = a5;
            vriblScript.a6o = a6;
            vriblScript.a7o = a7;
            vriblScript.a8o = a8;
            vriblScript.a9o = a9;
            vriblScript.a10o = a10;
            vriblScript.a11o = a11;
            vriblScript.a12o = a12;

            vriblScript.w1o = w1;
            vriblScript.w2o = w2;
            vriblScript.w3o = w3;
            vriblScript.w4o = w4;
            vriblScript.w5o = w5;
            vriblScript.w6o = w6;
            vriblScript.w7o = w7;
            vriblScript.w8o = w8;
            vriblScript.w9o = w9;
            vriblScript.w10o = w10;
            vriblScript.w11o = w11;
            vriblScript.w12o = w12;

            vriblScript.f1o = f1;
            vriblScript.f2o = f2;
            vriblScript.f3o = f3;
            vriblScript.f4o = f4;
            vriblScript.f5o = f5;
            vriblScript.f6o = f6;
            vriblScript.f7o = f7;
            vriblScript.f8o = f8;
            vriblScript.f9o = f9;
            vriblScript.f10o = f10;
            vriblScript.f11o = f11;
            vriblScript.f12o = f12;
        }
        else if (i == 8)
        {
            vriblScript.a1p = a1;
            vriblScript.a2p = a2;
            vriblScript.a3p = a3;
            vriblScript.a4p = a4;
            vriblScript.a5p = a5;
            vriblScript.a6p = a6;
            vriblScript.a7p = a7;
            vriblScript.a8p = a8;
            vriblScript.a9p = a9;
            vriblScript.a10p = a10;
            vriblScript.a11p = a11;
            vriblScript.a12p = a12;

            vriblScript.w1p = w1;
            vriblScript.w2p = w2;
            vriblScript.w3p = w3;
            vriblScript.w4p = w4;
            vriblScript.w5p = w5;
            vriblScript.w6p = w6;
            vriblScript.w7p = w7;
            vriblScript.w8p = w8;
            vriblScript.w9p = w9;
            vriblScript.w10p = w10;
            vriblScript.w11p = w11;
            vriblScript.w12p = w12;

            vriblScript.f1p = f1;
            vriblScript.f2p = f2;
            vriblScript.f3p = f3;
            vriblScript.f4p = f4;
            vriblScript.f5p = f5;
            vriblScript.f6p = f6;
            vriblScript.f7p = f7;
            vriblScript.f8p = f8;
            vriblScript.f9p = f9;
            vriblScript.f10p = f10;
            vriblScript.f11p = f11;
            vriblScript.f12p = f12;
        }
    }
    private void setValues(int parent)
    {
        if (parent == 1)
        {
            a1 = a1e;
            a2 = a2e;
            a3 = a3e;
            a4 = a4e;
            a5 = a5e;
            a6 = a6e;
            a7 = a7e;
            a8 = a8e;
            a9 = a9e;
            a10 = a10e;
            a11 = a11e;
            a12 = a12e;

            w1 = w1e;
            w2 = w2e;
            w3 = w3e;
            w4 = w4e;
            w5 = w5e;
            w6 = w6e;
            w7 = w7e;
            w8 = w8e;
            w9 = w9e;
            w10 = w10e;
            w11 = w11e;
            w12 = w12e;

            f1 = f1e;
            f2 = f2e;
            f3 = f3e;
            f4 = f4e;
            f5 = f5e;
            f6 = f6e;
            f7 = f7e;
            f8 = f8e;
            f9 = f9e;
            f10 = f10e;
            f11 = f11e;
            f12 = f12e;
        }
        else if (parent == 2)
        {
            a1 = a1r;
            a2 = a2r;
            a3 = a3r;
            a4 = a4r;
            a5 = a5r;
            a6 = a6r;
            a7 = a7r;
            a8 = a8r;
            a9 = a9r;
            a10 = a10r;
            a11 = a11r;
            a12 = a12r;

            w1 = w1r;
            w2 = w2r;
            w3 = w3r;
            w4 = w4r;
            w5 = w5r;
            w6 = w6r;
            w7 = w7r;
            w8 = w8r;
            w9 = w9r;
            w10 = w10r;
            w11 = w11r;
            w12 = w12r;

            f1 = f1r;
            f2 = f2r;
            f3 = f3r;
            f4 = f4r;
            f5 = f5r;
            f6 = f6r;
            f7 = f7r;
            f8 = f8r;
            f9 = f9r;
            f10 = f10r;
            f11 = f11r;
            f12 = f12r;
        }
        else if (parent == 3)
        {
            a1 = a1t;
            a2 = a2t;
            a3 = a3t;
            a4 = a4t;
            a5 = a5t;
            a6 = a6t;
            a7 = a7t;
            a8 = a8t;
            a9 = a9t;
            a10 = a10t;
            a11 = a11t;
            a12 = a12t;

            w1 = w1t;
            w2 = w2t;
            w3 = w3t;
            w4 = w4t;
            w5 = w5t;
            w6 = w6t;
            w7 = w7t;
            w8 = w8t;
            w9 = w9t;
            w10 = w10t;
            w11 = w11t;
            w12 = w12t;

            f1 = f1t;
            f2 = f2t;
            f3 = f3t;
            f4 = f4t;
            f5 = f5t;
            f6 = f6t;
            f7 = f7t;
            f8 = f8t;
            f9 = f9t;
            f10 = f10t;
            f11 = f11t;
            f12 = f12t;
        }
        else if (parent == 4)
        {
            a1 = a1y;
            a2 = a2y;
            a3 = a3y;
            a4 = a4y;
            a5 = a5y;
            a6 = a6y;
            a7 = a7y;
            a8 = a8y;
            a9 = a9y;
            a10 = a10y;
            a11 = a11y;
            a12 = a12y;

            w1 = w1y;
            w2 = w2y;
            w3 = w3y;
            w4 = w4y;
            w5 = w5y;
            w6 = w6y;
            w7 = w7y;
            w8 = w8y;
            w9 = w9y;
            w10 = w10y;
            w11 = w11y;
            w12 = w12y;

            f1 = f1y;
            f2 = f2y;
            f3 = f3y;
            f4 = f4y;
            f5 = f5y;
            f6 = f6y;
            f7 = f7y;
            f8 = f8y;
            f9 = f9y;
            f10 = f10y;
            f11 = f11y;
            f12 = f12y;
        }
        else if (parent == 5)
        {
            a1 = a1u;
            a2 = a2u;
            a3 = a3u;
            a4 = a4u;
            a5 = a5u;
            a6 = a6u;
            a7 = a7u;
            a8 = a8u;
            a9 = a9u;
            a10 = a10u;
            a11 = a11u;
            a12 = a12u;

            w1 = w1u;
            w2 = w2u;
            w3 = w3u;
            w4 = w4u;
            w5 = w5u;
            w6 = w6u;
            w7 = w7u;
            w8 = w8u;
            w9 = w9u;
            w10 = w10u;
            w11 = w11u;
            w12 = w12u;

            f1 = f1u;
            f2 = f2u;
            f3 = f3u;
            f4 = f4u;
            f5 = f5u;
            f6 = f6u;
            f7 = f7u;
            f8 = f8u;
            f9 = f9u;
            f10 = f10u;
            f11 = f11u;
            f12 = f12u;
        }
        else if (parent == 6)
        {
            a1 = a1i;
            a2 = a2i;
            a3 = a3i;
            a4 = a4i;
            a5 = a5i;
            a6 = a6i;
            a7 = a7i;
            a8 = a8i;
            a9 = a9i;
            a10 = a10i;
            a11 = a11i;
            a12 = a12i;

            w1 = w1i;
            w2 = w2i;
            w3 = w3i;
            w4 = w4i;
            w5 = w5i;
            w6 = w6i;
            w7 = w7i;
            w8 = w8i;
            w9 = w9i;
            w10 = w10i;
            w11 = w11i;
            w12 = w12i;

            f1 = f1i;
            f2 = f2i;
            f3 = f3i;
            f4 = f4i;
            f5 = f5i;
            f6 = f6i;
            f7 = f7i;
            f8 = f8i;
            f9 = f9i;
            f10 = f10i;
            f11 = f11i;
            f12 = f12i;
        }
        else if (parent == 7)
        {
            a1 = a1o;
            a2 = a2o;
            a3 = a3o;
            a4 = a4o;
            a5 = a5o;
            a6 = a6o;
            a7 = a7o;
            a8 = a8o;
            a9 = a9o;
            a10 = a10o;
            a11 = a11o;
            a12 = a12o;

            w1 = w1o;
            w2 = w2o;
            w3 = w3o;
            w4 = w4o;
            w5 = w5o;
            w6 = w6o;
            w7 = w7o;
            w8 = w8o;
            w9 = w9o;
            w10 = w10o;
            w11 = w11o;
            w12 = w12o;

            f1 = f1o;
            f2 = f2o;
            f3 = f3o;
            f4 = f4o;
            f5 = f5o;
            f6 = f6o;
            f7 = f7o;
            f8 = f8o;
            f9 = f9o;
            f10 = f10o;
            f11 = f11o;
            f12 = f12o;
        }
        else if (parent == 8)
        {
            a1 = a1p;
            a2 = a2p;
            a3 = a3p;
            a4 = a4p;
            a5 = a5p;
            a6 = a6p;
            a7 = a7p;
            a8 = a8p;
            a9 = a9p;
            a10 = a10p;
            a11 = a11p;
            a12 = a12p;

            w1 = w1p;
            w2 = w2p;
            w3 = w3p;
            w4 = w4p;
            w5 = w5p;
            w6 = w6p;
            w7 = w7p;
            w8 = w8p;
            w9 = w9p;
            w10 = w10p;
            w11 = w11p;
            w12 = w12p;

            f1 = f1p;
            f2 = f2p;
            f3 = f3p;
            f4 = f4p;
            f5 = f5p;
            f6 = f6p;
            f7 = f7p;
            f8 = f8p;
            f9 = f9p;
            f10 = f10p;
            f11 = f11p;
            f12 = f12p;
        }
    }
    private void getValues()
    {
        Variables vriblScript = variableHolder.GetComponent<Variables>();
        prevTime = vriblScript.prevTime;
        a1e = vriblScript.a1e;
        a2e = vriblScript.a2e;
        a3e = vriblScript.a3e;
        a4e = vriblScript.a4e;
        a5e = vriblScript.a5e;
        a6e = vriblScript.a6e;
        a7e = vriblScript.a7e;
        a8e = vriblScript.a8e;
        a9e = vriblScript.a9e;
        a10e = vriblScript.a10e;
        a11e = vriblScript.a11e;
        a12e = vriblScript.a12e;

        a1r = vriblScript.a1r;
        a2r = vriblScript.a2r;
        a3r = vriblScript.a3r;
        a4r = vriblScript.a4r;
        a5r = vriblScript.a5r;
        a6r = vriblScript.a6r;
        a7r = vriblScript.a7r;
        a8r = vriblScript.a8r;
        a9r = vriblScript.a9r;
        a10r = vriblScript.a10r;
        a11r = vriblScript.a11r;
        a12r = vriblScript.a12r;

        a1t = vriblScript.a1t;
        a2t = vriblScript.a2t;
        a3t = vriblScript.a3t;
        a4t = vriblScript.a4t;
        a5t = vriblScript.a5t;
        a6t = vriblScript.a6t;
        a7t = vriblScript.a7t;
        a8t = vriblScript.a8t;
        a9t = vriblScript.a9t;
        a10t = vriblScript.a10t;
        a11t = vriblScript.a11t;
        a12t = vriblScript.a12t;

        a1y = vriblScript.a1y;
        a2y = vriblScript.a2y;
        a3y = vriblScript.a3y;
        a4y = vriblScript.a4y;
        a5y = vriblScript.a5y;
        a6y = vriblScript.a6y;
        a7y = vriblScript.a7y;
        a8y = vriblScript.a8y;
        a9y = vriblScript.a9y;
        a10y = vriblScript.a10y;
        a11y = vriblScript.a11y;
        a12y = vriblScript.a12y;

        a1u = vriblScript.a1u;
        a2u = vriblScript.a2u;
        a3u = vriblScript.a3u;
        a4u = vriblScript.a4u;
        a5u = vriblScript.a5u;
        a6u = vriblScript.a6u;
        a7u = vriblScript.a7u;
        a8u = vriblScript.a8u;
        a9u = vriblScript.a9u;
        a10u = vriblScript.a10u;
        a11u = vriblScript.a11u;
        a12u = vriblScript.a12u;

        a1i = vriblScript.a1i;
        a2i = vriblScript.a2i;
        a3i = vriblScript.a3i;
        a4i = vriblScript.a4i;
        a5i = vriblScript.a5i;
        a6i = vriblScript.a6i;
        a7i = vriblScript.a7i;
        a8i = vriblScript.a8i;
        a9i = vriblScript.a9i;
        a10i = vriblScript.a10i;
        a11i = vriblScript.a11i;
        a12i = vriblScript.a12i;

        a1o = vriblScript.a1o;
        a2o = vriblScript.a2o;
        a3o = vriblScript.a3o;
        a4o = vriblScript.a4o;
        a5o = vriblScript.a5o;
        a6o = vriblScript.a6o;
        a7o = vriblScript.a7o;
        a8o = vriblScript.a8o;
        a9o = vriblScript.a9o;
        a10o = vriblScript.a10o;
        a11o = vriblScript.a11o;
        a12o = vriblScript.a12o;

        a1p = vriblScript.a1p;
        a2p = vriblScript.a2p;
        a3p = vriblScript.a3p;
        a4p = vriblScript.a4p;
        a5p = vriblScript.a5p;
        a6p = vriblScript.a6p;
        a7p = vriblScript.a7p;
        a8p = vriblScript.a8p;
        a9p = vriblScript.a9p;
        a10p = vriblScript.a10p;
        a11p = vriblScript.a11p;
        a12p = vriblScript.a12p;

        w1e = vriblScript.w1e;
        w2e = vriblScript.w2e;
        w3e = vriblScript.w3e;
        w4e = vriblScript.w4e;
        w5e = vriblScript.w5e;
        w6e = vriblScript.w6e;
        w7e = vriblScript.w7e;
        w8e = vriblScript.w8e;
        w9e = vriblScript.w9e;
        w10e = vriblScript.w10e;
        w11e = vriblScript.w11e;
        w12e = vriblScript.w12e;

        w1r = vriblScript.w1r;
        w2r = vriblScript.w2r;
        w3r = vriblScript.w3r;
        w4r = vriblScript.w4r;
        w5r = vriblScript.w5r;
        w6r = vriblScript.w6r;
        w7r = vriblScript.w7r;
        w8r = vriblScript.w8r;
        w9r = vriblScript.w9r;
        w10r = vriblScript.w10r;
        w11r = vriblScript.w11r;
        w12r = vriblScript.w12r;

        w1t = vriblScript.w1t;
        w2t = vriblScript.w2t;
        w3t = vriblScript.w3t;
        w4t = vriblScript.w4t;
        w5t = vriblScript.w5t;
        w6t = vriblScript.w6t;
        w7t = vriblScript.w7t;
        w8t = vriblScript.w8t;
        w9t = vriblScript.w9t;
        w10t = vriblScript.w10t;
        w11t = vriblScript.w11t;
        w12t = vriblScript.w12t;

        w1y = vriblScript.w1y;
        w2y = vriblScript.w2y;
        w3y = vriblScript.w3y;
        w4y = vriblScript.w4y;
        w5y = vriblScript.w5y;
        w6y = vriblScript.w6y;
        w7y = vriblScript.w7y;
        w8y = vriblScript.w8y;
        w9y = vriblScript.w9y;
        w10y = vriblScript.w10y;
        w11y = vriblScript.w11y;
        w12y = vriblScript.w12y;

        w1u = vriblScript.w1u;
        w2u = vriblScript.w2u;
        w3u = vriblScript.w3u;
        w4u = vriblScript.w4u;
        w5u = vriblScript.w5u;
        w6u = vriblScript.w6u;
        w7u = vriblScript.w7u;
        w8u = vriblScript.w8u;
        w9u = vriblScript.w9u;
        w10u = vriblScript.w10u;
        w11u = vriblScript.w11u;
        w12u = vriblScript.w12u;

        w1i = vriblScript.w1i;
        w2i = vriblScript.w2i;
        w3i = vriblScript.w3i;
        w4i = vriblScript.w4i;
        w5i = vriblScript.w5i;
        w6i = vriblScript.w6i;
        w7i = vriblScript.w7i;
        w8i = vriblScript.w8i;
        w9i = vriblScript.w9i;
        w10i = vriblScript.w10i;
        w11i = vriblScript.w11i;
        w12i = vriblScript.w12i;

        w1o = vriblScript.w1o;
        w2o = vriblScript.w2o;
        w3o = vriblScript.w3o;
        w4o = vriblScript.w4o;
        w5o = vriblScript.w5o;
        w6o = vriblScript.w6o;
        w7o = vriblScript.w7o;
        w8o = vriblScript.w8o;
        w9o = vriblScript.w9o;
        w10o = vriblScript.w10o;
        w11o = vriblScript.w11o;
        w12o = vriblScript.w12o;

        w1p = vriblScript.w1p;
        w2p = vriblScript.w2p;
        w3p = vriblScript.w3p;
        w4p = vriblScript.w4p;
        w5p = vriblScript.w5p;
        w6p = vriblScript.w6p;
        w7p = vriblScript.w7p;
        w8p = vriblScript.w8p;
        w9p = vriblScript.w9p;
        w10p = vriblScript.w10p;
        w11p = vriblScript.w11p;
        w12p = vriblScript.w12p;

        f1e = vriblScript.f1e;
        f2e = vriblScript.f2e;
        f3e = vriblScript.f3e;
        f4e = vriblScript.f4e;
        f5e = vriblScript.f5e;
        f6e = vriblScript.f6e;
        f7e = vriblScript.f7e;
        f8e = vriblScript.f8e;
        f9e = vriblScript.f9e;
        f10e = vriblScript.f10e;
        f11e = vriblScript.f11e;
        f12e = vriblScript.f12e;

        f1r = vriblScript.f1r;
        f2r = vriblScript.f2r;
        f3r = vriblScript.f3r;
        f4r = vriblScript.f4r;
        f5r = vriblScript.f5r;
        f6r = vriblScript.f6r;
        f7r = vriblScript.f7r;
        f8r = vriblScript.f8r;
        f9r = vriblScript.f9r;
        f10r = vriblScript.f10r;
        f11r = vriblScript.f11r;
        f12r = vriblScript.f12r;

        f1t = vriblScript.f1t;
        f2t = vriblScript.f2t;
        f3t = vriblScript.f3t;
        f4t = vriblScript.f4t;
        f5t = vriblScript.f5t;
        f6t = vriblScript.f6t;
        f7t = vriblScript.f7t;
        f8t = vriblScript.f8t;
        f9t = vriblScript.f9t;
        f10t = vriblScript.f10t;
        f11t = vriblScript.f11t;
        f12t = vriblScript.f12t;

        f1y = vriblScript.f1y;
        f2y = vriblScript.f2y;
        f3y = vriblScript.f3y;
        f4y = vriblScript.f4y;
        f5y = vriblScript.f5y;
        f6y = vriblScript.f6y;
        f7y = vriblScript.f7y;
        f8y = vriblScript.f8y;
        f9y = vriblScript.f9y;
        f10y = vriblScript.f10y;
        f11y = vriblScript.f11y;
        f12y = vriblScript.f12y;

        f1u = vriblScript.f1u;
        f2u = vriblScript.f2u;
        f3u = vriblScript.f3u;
        f4u = vriblScript.f4u;
        f5u = vriblScript.f5u;
        f6u = vriblScript.f6u;
        f7u = vriblScript.f7u;
        f8u = vriblScript.f8u;
        f9u = vriblScript.f9u;
        f10u = vriblScript.f10u;
        f11u = vriblScript.f11u;
        f12u = vriblScript.f12u;

        f1i = vriblScript.f1i;
        f2i = vriblScript.f2i;
        f3i = vriblScript.f3i;
        f4i = vriblScript.f4i;
        f5i = vriblScript.f5i;
        f6i = vriblScript.f6i;
        f7i = vriblScript.f7i;
        f8i = vriblScript.f8i;
        f9i = vriblScript.f9i;
        f10i = vriblScript.f10i;
        f11i = vriblScript.f11i;
        f12i = vriblScript.f12i;

        f1o = vriblScript.f1o;
        f2o = vriblScript.f2o;
        f3o = vriblScript.f3o;
        f4o = vriblScript.f4o;
        f5o = vriblScript.f5o;
        f6o = vriblScript.f6o;
        f7o = vriblScript.f7o;
        f8o = vriblScript.f8o;
        f9o = vriblScript.f9o;
        f10o = vriblScript.f10o;
        f11o = vriblScript.f11o;
        f12o = vriblScript.f12o;

        f1p = vriblScript.f1p;
        f2p = vriblScript.f2p;
        f3p = vriblScript.f3p;
        f4p = vriblScript.f4p;
        f5p = vriblScript.f5p;
        f6p = vriblScript.f6p;
        f7p = vriblScript.f7p;
        f8p = vriblScript.f8p;
        f9p = vriblScript.f9p;
        f10p = vriblScript.f10p;
        f11p = vriblScript.f11p;
        f12p = vriblScript.w12p;
    }
}

