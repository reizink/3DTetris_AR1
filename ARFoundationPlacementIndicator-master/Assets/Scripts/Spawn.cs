using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //block array to add blocks
    //blocks in prefabs
    [Header("Blocks")]
    public GameObject[] blocks;

    //shadows
    public GameObject[] shadows;

    //spawn & shadow pos
    public Vector3 spawnPos; //5,10,5
    public Vector3 shadowPos; //5,10,5
    public bool isPlay;

    private Vector3 CurPos;
    private Vector3 GridScale;
    private Vector3 GridRot;
    private Quaternion RotTest;

    // Start is called before the first frame update
    void Start()
    {
        CurPos = gameObject.transform.position;
        CurPos = new Vector3(CurPos.x + 0.25f, CurPos.y + 1, CurPos.z + 0.25f);
        //CurPos = new Vector3 (CurPos.x + 0.25f, CurPos.y + 1, CurPos.z + 0.25f);
        shadowPos = CurPos;

        //PlayerPrefs.SetString("GameBoard", CurPos.ToString());

        GridScale = gameObject.transform.parent.transform.localScale;
        //GridRot = new Vector3(gameObject.transform.parent.localRotation.x, gameObject.transform.parent.localRotation.y, gameObject.transform.parent.localRotation.z);

        Debug.Log("Current Spawn Pos: " + CurPos);
        Debug.Log("Current Grid Scale: " + GridScale);
        //Debug.Log("Get Grid: " + gameObject.transform.parent.name);
    }

    public void Update()
    {
        if (isPlay == true)
        {
            spawnBlock();
            isPlay = false;
        }
    }

    // spawn from list
    public void spawnBlock()
    {
        // choose random and spawn
        int randomBlock = Random.Range(0, blocks.Length);

        GameObject block = Instantiate(blocks[randomBlock], CurPos, Quaternion.identity) as GameObject; //spawnPos to CurPos
        block.tag = "CurrentBlock";
        block.transform.localScale = GridScale; //new Vector3(0.33f, 0.33f, 0.33f);
        Debug.Log("Spawned Block: " + block.name);

        //set parent
        //block.transform.parent = GameObject.FindGameObjectWithTag("GameParent").transform;
        //Debug.Log("Set Parent: " + GameObject.FindGameObjectWithTag("GameParent").name);

        // generate shadow
        GameObject shadowBlock = Instantiate(shadows[randomBlock], shadowPos, Quaternion.identity) as GameObject;
        shadowBlock.transform.localScale = GridScale;
        //shadowBlock.transform.parent = GameObject.FindGameObjectWithTag("GameParent").transform;
    }

    public void setPlay()
    {
        isPlay = true;
    }
}
