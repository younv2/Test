using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;
    public int cardCnt = 16;
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x=> Random.Range(0,1f)).ToArray();
        for(int i = 0; i < cardCnt; i++) 
        {
            GameObject go = Instantiate(card,this.transform);

            float x = (i % 4)*1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            go.transform.position = new Vector2(x,y);
            go.GetComponent<Card>().Setting(arr[i]);

        }
        GameManager.instance.cardCount = arr.Length;
    }
}
