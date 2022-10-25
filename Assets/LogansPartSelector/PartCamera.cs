using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCamera : MonoBehaviour
{
    [SerializeField] private PartSelectorManager mgr;

    [Header("INTERNAL REFERENCE")]
    [SerializeField] private Transform trans;

    [Header("STATS")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float lookSpeed;

    void Awake()
    {
        U.GetComponent_managed(ref trans, gameObject);

        if( movementSpeed <= 0 )
        {
            Debug.LogWarning($"WARNING! movementSpeed doesn't seem to be set for camera!");
        }

        if ( lookSpeed <= 0 )
        {
            Debug.LogWarning($"WARNING! lookSpeed doesn't seem to be set for camera!");
        }
    }

    void LateUpdate()
    {
        if( mgr == null )
        {
            return;
        }

        if( mgr.ActivePartSelector != null )
        {
            trans.position = Vector3.Lerp(trans.position, mgr.ActivePartSelector.CameraFocusTransform.position, movementSpeed * Time.deltaTime );

            Vector3 vLookGoalLocal = Vector3.zero;
            if ( mgr.ActivePartSelector.MeshRenderer != null )
            {
                vLookGoalLocal = Vector3.Normalize( mgr.ActivePartSelector.MeshRenderer.bounds.center-trans.position );
            }
            else
            {
                vLookGoalLocal = Vector3.Normalize(mgr.ActivePartSelector.transform.position - trans.position);
            }
            //Vector3 vLookGoalLocal = Vector3.Normalize( trans.position -mgr.ActivePartSelector.CameraFocusTransform.position );

            Vector3 v_lookAt = Vector3.Lerp( trans.position + trans.forward, trans.position + vLookGoalLocal, lookSpeed * Time.deltaTime ); 

            trans.LookAt( v_lookAt );
        }
    }


}
