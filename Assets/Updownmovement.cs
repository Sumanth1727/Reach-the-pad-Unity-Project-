using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updownmovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed=0;
    [SerializeField] int TimeFrame =100;
    int a = 0;
    bool j = true;
    [SerializeField] enum MotionDirection {x,y,z};

    [SerializeField] MotionDirection MotionDirec = MotionDirection.x;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        a = a + 1;
        if(a% TimeFrame == 0)
        {
            j = !j;
        }
        if(j)
        {
            switch(MotionDirec)
            {
                case MotionDirection.x:
                    transform.Translate(MovementSpeed * Time.deltaTime,0, 0);
                    break;
                case MotionDirection.y:
                    transform.Translate(0, MovementSpeed * Time.deltaTime, 0);
                    break;
                case MotionDirection.z:
                    transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
                    break;

            }
            
        }
        else
        {
            switch (MotionDirec)
            {
                case MotionDirection.x:
                    transform.Translate(-MovementSpeed * Time.deltaTime, 0, 0);
                    break;
                case MotionDirection.y:
                    transform.Translate(0, -MovementSpeed * Time.deltaTime, 0);
                    break;
                case MotionDirection.z:
                    transform.Translate(0, 0, -MovementSpeed * Time.deltaTime);
                    break;

            }
        }
    }
}
