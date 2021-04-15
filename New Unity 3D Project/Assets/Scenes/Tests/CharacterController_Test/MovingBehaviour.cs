using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingType 
{
    CharacterController, TransformPlusEquals, Translate,
}

public class MovingBehaviour : MonoBehaviour
{
    public MovingType myType;

    public float Speed;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (myType)
        {
            case MovingType.CharacterController:
                controller.Move(transform.forward * Speed * Time.deltaTime);
                break;
            case MovingType.TransformPlusEquals:
                transform.position += transform.forward * Speed * Time.deltaTime;
                break;
            case MovingType.Translate:
                transform.Translate(transform.forward * Speed * Time.deltaTime);
                break;           
        }
    }
}
