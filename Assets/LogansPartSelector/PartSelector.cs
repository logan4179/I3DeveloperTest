using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class PartSelector : MonoBehaviour
{
    [SerializeField] private PartSelectorManager mgr;
    public PartSelectorManager Mgr
    {
        get { return mgr; }
        set { mgr = value; }
    }
    [Space(10)]

    [Header("INTERNAL REFERENCE")]
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    public MeshRenderer MeshRenderer => meshRenderer;
    [SerializeField] private Material normalMaterial;
    [Space(10)]


    [Header("OTHER")]
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Transform cameraFocusTransform;
    public Transform CameraFocusTransform => cameraFocusTransform;
    private bool mouseIsOverMe;

    void Awake()
    {
        FetchReferencesFromGameObject();

        if( mgr != null )
        {
            mgr.AddPartSelector( this );
        }
    }

    /// <summary>
    /// Allows you to quickly fetch references residing on this gameobject from the inspector with a contextmenu rather than manually placing each of them.
    /// </summary>
    [ContextMenu("call FetchReferencesFromGameObject()")]
    public void FetchReferencesFromGameObject()
    {
        U.GetComponent_managed(ref meshCollider, gameObject);
        U.GetComponent_managed(ref meshRenderer, gameObject);
        if (normalMaterial == null)
        {
            Debug.LogWarning($"PART SELECTOR WARNING! Member variable normalMaterial for object: <b><color=green>'{name}'</color></b> was null on Awake(). This could be optimized by pre-caching in the inspector.");
            if (meshRenderer == null)
            {
                Debug.LogWarning($"PART SELECTOR WARNING! normalMaterial for and meshRenderer was null, so the material will not be able to be fetched...");
            }
            else
            {
                normalMaterial = meshRenderer.sharedMaterial;
            }
        }

        if (highlightMaterial == null)
        {
            Debug.LogWarning($"PART SELECTOR WARNING! Member variable normalMaterial for object: <b><color=green>'{name}'</color></b> was null on Awake(). Part selector won't be able to highlight...");
        }

        if( mgr != null && mgr.PartSelectors != null && !mgr.PartSelectors.Contains(this) )
        {
            mgr.PartSelectors.Add(this);
        }
    }

    public void SelectMe()
    {
        mgr.ActivePartSelector = this;
    }

    private void OnMouseEnter()
    {
        mouseIsOverMe = true;
        if( meshRenderer != null )
        {
            meshRenderer.material = highlightMaterial;
        }

    }

    /// <summary>
    /// Happens once when you have clicked on an object, hold it down, then let go of the mouse click. Fires even if you have moved the mouse off the object while holding down LMB then let go.
    /// </summary>
    private void OnMouseUp()
    {
        if (mouseIsOverMe)
        {
            SelectMe();
        }
    }

    /// <summary>
    /// Happens once when the mouse is moved off the object
    /// </summary>
    private void OnMouseExit()
    {
        mouseIsOverMe = false;
        if (meshRenderer != null)
        {
            meshRenderer.material = normalMaterial;
        }
    }

    private GUIStyle gst = null;
    Transform t = null;

    private void OnDrawGizmos()
    {
        if ( !Application.isEditor )
        {
            return;
        }
        if( t == null )
        {
            t = GetComponent<Transform>();
        }
        if( gst == null )
        {
            gst = new GUIStyle();
            gst.normal.textColor = Color.red;
            return;
        }


        if( meshCollider == null || meshRenderer == null || normalMaterial == null || highlightMaterial == null)
        {
            UnityEditor.Handles.Label( (meshRenderer == null ? t.position : meshRenderer.bounds.center), "Missing a reference", gst );
        }
    }
}
