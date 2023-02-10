using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootRevert : MonoBehaviour
{
    [HideInInspector] public GameObject branchUI;
    [HideInInspector] public GameObject rock;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Revertbranch);
    }

    void Revertbranch()
    {
        branchUI.SetActive(true);
        rock.SetActive(true);
        Destroy(this.gameObject);
        branchUI.GetComponent<UIBranch>().isPutIn = false;
    }
}
