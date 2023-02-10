using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int height = 6;
    public int width = 6;

    public HexCell[] cellPrefabList;
    public float[] cellWeight;

    public GameObject EmptySpaceCell;
    public int EmptySpaceCellWidth;
    public int EmptySpaceCellHeight;

    public GameObject InitialRoot;
    HexCell[] cells;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateCells();
        CreateEmptySpaceCell();
        CreateInitialRoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateCells()
    {
        cells = new HexCell[width * height];
        for (int z = 0, i = 0; z < width; z++)
        {
            for (int x = 0; x < height; x++)
            {
                CreateOneCell(x, z, i++);
            }
        }
    }

    void CreateOneCell(int x, int y, int i)
    {
        Vector3 position;
        position.y = (x + y * 0.5f - y /2) *(Hex.innerRadius * 2f);
        position.x = y * Hex.outerRadius * 1.5f;
        position.z = 0 ;

        int cellIndex = RandomCell();
        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefabList[cellIndex]);
        
            
        cell.name = y.ToString() + " " + x.ToString();
        Canvas canvas = cell.GetComponentInChildren<Canvas>();
        TMP_Text text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = x.ToString() + "\n" + y.ToString();
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
    }

    int RandomCell()
    {
        float curmin = 0;
        float curmax = 0;
        int rand = Random.Range(0, 101);

        for (int i = 0; i < cellPrefabList.Length; i++) {

            curmax += cellWeight[i];
            if (curmin <= rand && rand < curmax)
            {
                return i; //·µ»ØµØ¿éÐòºÅ
            }
            curmin = curmax;
        }

        return cellPrefabList.Length - 1;
    }

    void CreateEmptySpaceCell()
    {
        int index = (EmptySpaceCellWidth) * height + EmptySpaceCellHeight;
        GameObject Emptycell = Instantiate<GameObject>(EmptySpaceCell);
        Emptycell.transform.SetParent(transform, false);
        Emptycell.transform.position = cells[index].transform.position;
    }

    void CreateInitialRoot()
    {
        //8,5
        if (InitialRoot == null)
            return;
        int index = (9) * 7 + 5;
        GameObject root = Instantiate<GameObject>(InitialRoot);
        root.transform.SetParent(transform, false);
        root.transform.position = cells[index].transform.position + new Vector3(0,Hex.innerRadius*2,0);
        root.GetComponent<RootFinish>().CloseCanvas1();
    }

}
