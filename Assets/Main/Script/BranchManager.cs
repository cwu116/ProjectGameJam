using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    public List<GameObject> branchList;
    public List<Transform> branchUIPos;
    public List<GameObject> branchUIList;
    // Start is called before the first frame update
    void Start()
    {
        CreateNewBranch(1);
        CreateNewBranch(2);
        CreateNewBranch(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewBranch(int branchindex)
    {
        branchindex -= 1;
        GameObject uiBranch = Instantiate<GameObject>(branchUIList[branchindex]);
        uiBranch.transform.SetParent(branchUIPos[branchindex]);
        uiBranch.transform.position = branchUIPos[branchindex].position;
        uiBranch.transform.localScale = branchUIPos[branchindex].localScale;

    }


}
