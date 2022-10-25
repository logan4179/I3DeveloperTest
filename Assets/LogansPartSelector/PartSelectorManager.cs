using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PartSelectorManager : MonoBehaviour
{
    private List<PartSelector> partSelectors;
    public List<PartSelector> PartSelectors => partSelectors;

    private PartSelector activePartSelector;
    /// <summary>
    /// The currently-active part selector for this manager.
    /// </summary>
    public PartSelector ActivePartSelector
    {
        get { return activePartSelector; }
        set 
        { 
            if( activePartSelector != value )
            {
                labelText.text = value.name.ToString();
            }
            activePartSelector = value; 
        }
    }

    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private GameObject partList;
    [SerializeField] private Button partButtonPrefab;

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Initializes the manager. Runs through all added partselectors and instantiates a button for them on the canvas.
    /// </summary>
    public void Init()
    {
        labelText.text = "";

        if( partList != null )
        {
            if( partSelectors != null && partSelectors.Count > 0 )
            {
                if( partButtonPrefab == null )
                {
                    Debug.LogWarning($"PartSelectorManager WARNING! partButton prefab not provided.");
                    return;
                }
                foreach( PartSelector partSelector in partSelectors )
                {
                    if( partSelector.Mgr == null )
                    {
                        partSelector.Mgr = this;
                    }
                    Button btn = Instantiate(partButtonPrefab, partList.transform);
                    btn.onClick.AddListener(partSelector.SelectMe);
                    btn.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = partSelector.name;

                }
            }
        }
        else
        {
            Debug.LogWarning($"PartSelectorManager WARNING! partList reference wasn't set!");
        }
    }

    /// <summary>
    /// Logs a partselector to the manager
    /// </summary>
    /// <param name="partSelector_passed"></param>
    public void AddPartSelector( PartSelector partSelector_passed )
    {
        if( partSelectors != null )
        {
            if ( !partSelectors.Contains(partSelector_passed) )
            {
                partSelectors.Add(partSelector_passed);
            }
        }
        else
        {
            partSelectors = new List<PartSelector>() { partSelector_passed };
        }
    }
}
