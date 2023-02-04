using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public HexCell[] cellPrefabList;
    public float[] cellWeight;
    public GameObject EmptySpaceCell;
    HexCell[] cells;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateCells();
        CreateEmptySpaceCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateCells()
    {
        cells = new HexCell[height * width];
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
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
                return i; //���صؿ����
            }
            curmin = curmax;
        }

        return cellPrefabList.Length - 1;
    }

    void CreateEmptySpaceCell()
    {
        int rand = Random.Range(0, cells.Length);
        GameObject Emptycell = Instantiate<GameObject>(EmptySpaceCell);
        Emptycell.transform.SetParent(transform, false);
        Emptycell.transform.position = cells[rand].transform.position;
    }

}