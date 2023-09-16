using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoManager : MonoBehaviour
{

    public static FondoManager instance;
    public GameObject fondoPrefab;
    [SerializeField]FondoController primerFondo;

    [SerializeField] List<Sprite> tipos = new List<Sprite>();
    float sizeY;

    public float SizeY { get => sizeY; set => sizeY = value; }
    public FondoController PrimerFondo { get => primerFondo; set => primerFondo = value; }

    private void Awake()
    {
        instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        var obj = Instantiate(fondoPrefab, this.transform);
        PrimerFondo = obj.GetComponent<FondoController>();
        SetSprite(obj);
        SizeY = PrimerFondo.ResizeSpriteToScreen();
        print(SizeY);
        var obj2 = Instantiate(fondoPrefab,this.transform);
        obj2.transform.position += new Vector3(0, -SizeY,0);
        SetSprite(obj2);
        var obj3 = Instantiate(fondoPrefab,  this.transform);
        obj3.transform.position += new Vector3(0, -SizeY-SizeY, 0);
        SetSprite(obj3);
        var obj4 = Instantiate(fondoPrefab, this.transform);
        obj4.transform.position += new Vector3(0, -SizeY - SizeY - SizeY, 0);
        SetSprite(obj4);




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().sprite = tipos[Random.Range(0, tipos.Count)];
    }

}
