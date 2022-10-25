using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class
/// </summary>
public static class U
{

    /// <summary>Managed method for getting a component. Warns the user if the Component isn't pre-cached in the inspector. Uses TryGetComponent() to attempt to set the component reference, and warns the user if this is unsuccessful.</summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="component_passed">Component that needs setting.</param>
    /// <param name="gameobject_passed">GameObject that should contain the component.</param>
    public static void GetComponent_managed<TComponent>(ref TComponent component_passed, GameObject gameobject_passed = null) where TComponent : Component
    {
        //DbgMethodStart($"Get_managed(go: '{gameobject_passed.name}')");

        if (component_passed != null)
        {
            return;
        }

        if (component_passed == null)
        {
            Debug.LogWarning($"WARNING! passed component reference for GameObject: '{gameobject_passed.name}' was null and had to be found in the heirarchy.  This could be optimized by pre-assigning in the inspector!");
            if (gameobject_passed != null && !gameobject_passed.TryGetComponent(out component_passed))
            {
                Debug.LogWarning($"WARNING! Couldn't get passed component from passed gameobject: '{gameobject_passed.name}'");
            }
        }
    }
}
