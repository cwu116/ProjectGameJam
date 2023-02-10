
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    public List<GameObject> branchList;
    public List<Transform> branchUIPos;
    public List<UIBranch> branchUIList;
    public static BranchManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateNewBranch(0);
        CreateNewBranch(1);
        CreateNewBranch(2);
    }


    public void CreateNewBranch(int branchindex)
    {
        int rand = Random.Range(0, 4);
        UIBranch uiBranch = Instantiate<UIBranch>(branchUIList[rand]);
        uiBranch .branchUIindex = branchindex;
        uiBranch.transform.SetParent(branchUIPos[branchindex]);
        uiBranch.transform.position = branchUIPos[branchindex].position;
        Vector3 temp = branchUIPos[branchindex].localScale;
        uiBranch.transform.localScale = new Vector3(temp.x * 1.5f, temp.y * 1.5f, temp.z);

    }


}

