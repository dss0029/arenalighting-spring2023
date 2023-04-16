using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovementScrollView : MonoBehaviour
{
    [SerializeField]
    List<GameObject> cameraMovementPanels;

    [SerializeField]
    GameObject cameraMovementContent;

    [SerializeField]
    GameObject cameraMovementPanelPrefab;

    [SerializeField]
    List<ICameraMovement> cameraMovements = new List<ICameraMovement>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < cameraMovements.Count; i++)
        {
            GameObject cameraMovementPanel = Instantiate(cameraMovementPanelPrefab, cameraMovementContent.transform);
            CameraMovementPanelManager cameraMovementManager = cameraMovementPanel.GetComponent<CameraMovementPanelManager>();
            cameraMovementManager.UpdateCameraMovementPanel(i, cameraMovements[i]);
            cameraMovementPanels.Add(cameraMovementPanel);
        }
    }

    void Update()
    {
        
    }

    public void UpdateCameraMovementList(List<ICameraMovement> cameraMovements)
    {
        this.cameraMovements = cameraMovements;

        if (cameraMovements.Count < cameraMovementPanels.Count)
        {
            for (int i = 0; i < cameraMovementPanels.Count - cameraMovements.Count; i++)
            {
                GameObject cameraMovementPanel = cameraMovementPanels[0];
                cameraMovementPanels[0] = null;
                Destroy(cameraMovementPanel);
                cameraMovementPanels.RemoveAt(0);
            }
        }
        else if (cameraMovements.Count > cameraMovementPanels.Count) {

            for (int i = 0; i < cameraMovements.Count - cameraMovementPanels.Count; i++)
            {
                GameObject cameraMovementPanel = Instantiate(cameraMovementPanelPrefab, cameraMovementContent.transform);
                cameraMovementPanels.Add(cameraMovementPanel);
            }
        }

        for (int i = 0; i < cameraMovementPanels.Count; i++)
        {
            GameObject cameraMovementPanel = cameraMovementPanels[i];
            CameraMovementPanelManager cameraMovementPanelManager = cameraMovementPanel.GetComponent<CameraMovementPanelManager>();
            cameraMovementPanelManager.UpdateCameraMovementPanel(i, cameraMovements[i]);

        }
    }

    public void OnRemoveRequested(int movementID)
    {
        GameObject cameraMovementPanel = cameraMovementPanels[movementID];
        cameraMovementPanels[movementID] = null;
        Destroy(cameraMovementPanel);
        cameraMovementPanels.RemoveAt(movementID);
        cameraMovements.RemoveAt(movementID);
    }
}
