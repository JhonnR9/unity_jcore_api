using System;
using UnityEngine;

/// <summary>
/// This class acts as a bridge between Unity and the JCore API,
/// offering methods to allow other classes to manipulate Unity objects.
/// All classes in the Unity scene should inherit from this class.
/// </summary>
public partial class Controller : MonoBehaviour
{
    protected partial class Data{}
    protected Data data;

    protected virtual void Start()
    {
        if (UsePhysics)
        {
            StartPhysics();
        }
        StartVisual();

    }
    protected void Awake()
    {
        data = new Data();
        GetComponents();
    }

    protected virtual void GetComponents()
    {

        GetPhysicsComponents();
        GetVisualComponents();
    }

    void ShowMissingComponentError(string componentName)
    {
        Debug.LogError($"{gameObject.name} is missing a {componentName} component.");
    }
    

}

