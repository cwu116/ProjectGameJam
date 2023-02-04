
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    public List<GameObject> branchList;
    public List<Transform> branchUIPos;
    public List<Branch> branchUIList;
    // Start is called before the first frame update
    void Start()
    {
        CreateNewBranch(0);
        CreateNewBranch(1);
        CreateNewBranch(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNewBranch(int branchindex)
    {
        int rand = Random.Range(0, 4);
        Branch uiBranch = Instantiate<Branch>(branchUIList[rand]);
        uiBranch .branchUIindex = branchindex;
        uiBranch.transform.SetParent(branchUIPos[branchindex]);
        uiBranch.transform.position = branchUIPos[branchindex].position;
        uiBranch.transform.localScale = branchUIPos[branchindex].localScale;

    }


}

