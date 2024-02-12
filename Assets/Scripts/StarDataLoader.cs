using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;

public class StarDataLoader : MonoBehaviour
{
    public GameObject star_prefab;
    public GameObject text_prefab;
    public TextAsset stardata_file;

    public List<LoadData> starList = new List<LoadData>();
    public Dictionary<string,LoadData> const_stars = new Dictionary<string, LoadData>();

    public GameObject linePrefab;
    public TextAsset const_file;

    public bool show_exo = false;

    public int count_constStars = 0;
    private int star_count = 1;
    private float e = 2.71828f;
    public Color color_arr;
    Dictionary<string, int> spectral_types = new Dictionary<string, int>() { { "O", 0}, { "B", 1 }, { "A", 2 }, { "F", 3 }, { "G", 4 }, { "K", 5 }, { "M", 6 } };
    
    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("Hello");
        string allData = stardata_file.text;
        string[] lineData = allData.Split('\n');
        for (var i=1; i < lineData.Length-1; i++)
        {
            Debug.Log("Hello");
            string[] eachline = lineData[i].Split(',');
            var star = Instantiate(star_prefab, new Vector3(float.Parse(eachline[2]), float.Parse(eachline[3]), float.Parse(eachline[4])), Quaternion.identity, gameObject.transform);
            star.name = "S" + star_count;
            star_count++;
            Debug.Log("Hello");
            LoadData star1 = new LoadData();
            //star1.absoluteMagnitude = float.Parse(eachline[5]);
            //star1.distance = float.Parse(eachline[1]);
            star1.position = new Vector3(float.Parse(eachline[2]), float.Parse(eachline[3]), float.Parse(eachline[4]));
            star1.hip_no = eachline[0];
            starList.Add(star1);
        }
        Constellations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Constellations()
    {
        
        //private readonly List<(int[], int[])> constellations = new()
        string allData2 = const_file.text;
        string[] lineData2 = allData2.Split('\n');
        //string firstLine = ;
        GameObject mainConst = transform.gameObject;

        
        string[] eachLine2 = lineData2[2].Split(' ');
        //Debug.Log(eachLine2[0]);
        var constellation = new GameObject();
        constellation.name = eachLine2[0];
        constellation.transform.parent = mainConst.transform;


        //public List<string> hip_nos = new List<string>();    

        /*for (int i = 2; i<=eachLine2.Length; i+=2)
        {
            string[] hip_nos = eachLine2[i];
        }*/
        //Debug.Log(eachLine2.Length);

        foreach (LoadData star in starList)
        {
            string includedstar = star.hip_no.ToString();
            //Debug.Log(eachLine2.Any(element => element.Trim() == includedstar.Trim()));
            if(eachLine2.Any(element => element.Trim() == includedstar.Trim()))
            {
                const_stars.Add(includedstar,star);
                count_constStars += 1;
                //Debug.Log("Helloo");
            }
        }


        /*for (var j=2; j <= eachLine2.Length; j++)
        {
            
            foreach (LoadData star in starList)
            {
                
                if (eachLine2[j] == star.hip_no.ToString())
                {
                    //Vector3 temp_pos1 = star.position(x, y, z);
                    const_stars.Add(star);
                    count_constStars += 1;
                    Debug.Log("Hello");
                }
            }*/

        //var line1 = Instantiate(linePrefab, constellation.transform);
        //var line1Renderer = line1.GetComponent<LineRenderer>();
        //line1Renderer.positionCount = 2;
        //line1Renderer.SetPosition(0, new Vector3(float.Parse(eachLine2[(j - 1) * 6 + 1]), float.Parse(eachLine2[(j - 1) * 6 + 2]), float.Parse(eachLine2[(j - 1) * 6 + 3])));
        //line1Renderer.SetPosition(1, new Vector3(float.Parse(eachLine2[(j - 1) * 6 + 4]), float.Parse(eachLine2[(j - 1) * 6 + 5]), float.Parse(eachLine2[(j - 1) * 6 + 6])));
        //Vector3 pos1 = starList[eachLine2[j] - 1].transform.position;
        //Vector3 pos2 = starList[eachLine2[j] + 1].transform.position;

        //Vector3 dir = (pos2 - pos1).normalized*3;
        //line1Renderer.SetPosition(0, pos1 + dir);
        //line1Renderer.SetPosition(1, pos2 - dir);
        //}
        //for (int i = 1; i < eachLine2.Length - 1; i += 2)
        //{
        //    Debug.Log(eachLine2[i]);
        //}
        for (int i=3; i< eachLine2.Length; i+=2)
        {
            
            var line1 = Instantiate(linePrefab, constellation.transform);
            var line1Renderer = line1.GetComponent<LineRenderer>();
            line1Renderer.positionCount = 2;
            
            Vector3 position1 = const_stars[eachLine2[i].Trim()].position;
            Vector3 position2 = const_stars[eachLine2[i+1].Trim()].position;
            //Vector3 dirr = (position2 - position1).normalized*3;
            //line1Renderer.SetPosition(0, position1 + dirr);
            //line1Renderer.SetPosition(1, position2 - dirr);
            line1Renderer.SetPosition(0, position1);
            line1Renderer.SetPosition(1, position2);
        }

    }
}
