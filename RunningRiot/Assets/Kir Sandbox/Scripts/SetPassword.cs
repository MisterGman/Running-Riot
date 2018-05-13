using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPassword : MonoBehaviour
{
    public Image[,] allImages;



    float oldRR;
    float oldRC1;
    float oldRC2;


    public Image image00;
    public Image image01;
    public Image image02;
    public Image image03;
    public Image image04;
    public Image image05;
    public Image image06;
    public Image image07;
    public Image image08;
    public Image image09;

    public Image image10;
    public Image image11;
    public Image image12;
    public Image image13;
    public Image image14;
    public Image image15;
    public Image image16;
    public Image image17;
    public Image image18;
    public Image image19;

    public Image image20;
    public Image image21;
    public Image image22;
    public Image image23;
    public Image image24;
    public Image image25;
    public Image image26;
    public Image image27;
    public Image image28;
    public Image image29;

    public Image image30;
    public Image image31;
    public Image image32;
    public Image image33;
    public Image image34;
    public Image image35;
    public Image image36;
    public Image image37;
    public Image image38;
    public Image image39;

    public Image image40;
    public Image image41;
    public Image image42;
    public Image image43;
    public Image image44;
    public Image image45;
    public Image image46;
    public Image image47;
    public Image image48;
    public Image image49;

    public Image image50;
    public Image image51;
    public Image image52;
    public Image image53;
    public Image image54;
    public Image image55;
    public Image image56;
    public Image image57;
    public Image image58;
    public Image image59;

    public Image image60;
    public Image image61;
    public Image image62;
    public Image image63;
    public Image image64;
    public Image image65;
    public Image image66;
    public Image image67;
    public Image image68;
    public Image image69;

    public Image image70;
    public Image image71;
    public Image image72;
    public Image image73;
    public Image image74;
    public Image image75;
    public Image image76;
    public Image image77;
    public Image image78;
    public Image image79;

    public Image image80;
    public Image image81;
    public Image image82;
    public Image image83;
    public Image image84;
    public Image image85;
    public Image image86;
    public Image image87;
    public Image image88;
    public Image image89;
    // Use this for intialization
    void Start()
    {
        oldRR = 0;
        oldRC1 = 0;
        oldRC2 = 0;

        allImages = new Image[6, 15];

        allImages[0, 0] = image00;
        allImages[0, 1] = image01;
        allImages[0, 2] = image02;
        allImages[0, 3] = image03;
        allImages[0, 4] = image04;
        allImages[0, 5] = image05;
        allImages[0, 6] = image06;
        allImages[0, 7] = image07;
        allImages[0, 8] = image08;
        allImages[0, 9] = image09;
        allImages[0, 10] = image10;
        allImages[0, 11] = image11;
        allImages[0, 12] = image12;
        allImages[0, 13] = image13;
        allImages[0, 14] = image14;

        allImages[1, 0] = image15;
        allImages[1, 1] = image16;
        allImages[1, 2] = image17;
        allImages[1, 3] = image18;
        allImages[1, 4] = image19;
        allImages[1, 5] = image20;
        allImages[1, 6] = image21;
        allImages[1, 7] = image22;
        allImages[1, 8] = image23;
        allImages[1, 9] = image24;
        allImages[1, 10] = image25;
        allImages[1, 11] = image26;
        allImages[1, 12] = image27;
        allImages[1, 13] = image28;
        allImages[1, 14] = image29;

        allImages[2, 0] = image30;
        allImages[2, 1] = image31;
        allImages[2, 2] = image32;
        allImages[2, 3] = image33;
        allImages[2, 4] = image34;
        allImages[2, 5] = image35;
        allImages[2, 6] = image36;
        allImages[2, 7] = image37;
        allImages[2, 8] = image38;
        allImages[2, 9] = image39;
        allImages[2, 10] = image40;
        allImages[2, 11] = image41;
        allImages[2, 12] = image42;
        allImages[2, 13] = image43;
        allImages[2, 14] = image44;

        allImages[3, 0] = image45;
        allImages[3, 1] = image46;
        allImages[3, 2] = image47;
        allImages[3, 3] = image48;
        allImages[3, 4] = image49;
        allImages[3, 5] = image50;
        allImages[3, 6] = image51;
        allImages[3, 7] = image52;
        allImages[3, 8] = image53;
        allImages[3, 9] = image54;
        allImages[3, 10] = image55;
        allImages[3, 11] = image56;
        allImages[3, 12] = image57;
        allImages[3, 13] = image58;
        allImages[3, 14] = image59;

        allImages[4, 0] = image60;
        allImages[4, 1] = image61;
        allImages[4, 2] = image62;
        allImages[4, 3] = image63;
        allImages[4, 4] = image64;
        allImages[4, 5] = image65;
        allImages[4, 6] = image66;
        allImages[4, 7] = image67;
        allImages[4, 8] = image68;
        allImages[4, 9] = image69;
        allImages[4, 10] = image70;
        allImages[4, 11] = image71;
        allImages[4, 12] = image72;
        allImages[4, 13] = image73;
        allImages[4, 14] = image74;

        allImages[5, 0] = image75;
        allImages[5, 1] = image76;
        allImages[5, 2] = image77;
        allImages[5, 3] = image78;
        allImages[5, 4] = image79;
        allImages[5, 5] = image80;
        allImages[5, 6] = image81;
        allImages[5, 7] = image82;
        allImages[5, 8] = image83;
        allImages[5, 9] = image84;
        allImages[5, 10] = image85;
        allImages[5, 11] = image86;
        allImages[5, 12] = image87;
        allImages[5, 13] = image88;
        allImages[5, 14] = image89;


        InvokeRepeating("SetPass", 2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetPass()
    {
        /*int randomRow = UnityEngine.Random.Range(0, allImages.GetLength(0));
        if (randomRow == oldRR)
        {
            randomRow = UnityEngine.Random.Range(0, allImages.GetLength(0));
        }
        else
        {
            oldRR = randomRow;
        }

        int randomColLeft = UnityEngine.Random.Range(0, 4allImages.GetLength(1) - 7);
        if(randomColLeft == oldRCL)
        {
            randomColLeft = UnityEngine.Random.Range(0, 4allImages.GetLength(1) - 7);
        }
        else
        {
            oldRCL = randomColLeft;
        }

        int randomColRight = UnityEngine.Random.Range(6, 12allImages.GetLength(1) - 2);
        if(randomColRight == oldRCR)
        {
            randomColRight = UnityEngine.Random.Range(6, 12allImages.GetLength(1) - 2);
        }
        else
        {
            oldRCR = randomColRight;
        }

        allImages[randomRow, randomColLeft].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow, randomColLeft + 1].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow, randomColLeft + 2].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow, randomColRight].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow, randomColRight + 1].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow, randomColRight + 2].GetComponent<RandomImage>().changeToPass();*/


        int randomCol1 = UnityEngine.Random.Range(0, allImages.GetLength(1) - 3);
        if (randomCol1 == oldRC1)
        {
            randomCol1 = UnityEngine.Random.Range(0, allImages.GetLength(0));
        }
        else
        {
            oldRC1 = randomCol1;
        }

        int randomCol2 = UnityEngine.Random.Range(0, allImages.GetLength(1) - 3);
        if (randomCol2 == oldRC2)
        {
            randomCol2 = UnityEngine.Random.Range(0, allImages.GetLength(0));
        }
        else
        {
            oldRC2 = randomCol1;
        }

        int randomRow1 = UnityEngine.Random.Range(0, allImages.GetLength(0));
        int randomRow2 = UnityEngine.Random.Range(0, allImages.GetLength(0));
        if (randomRow2 == randomRow1)
        {
            randomRow2 = UnityEngine.Random.Range(0, allImages.GetLength(0));
        }

        allImages[randomRow1, randomCol1].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow1, randomCol1 + 1].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow1, randomCol1 + 2].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow1, randomCol1 + 3].GetComponent<RandomImage>().changeToPass();

        allImages[randomRow2, randomCol2].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow2, randomCol2 + 1].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow2, randomCol2 + 2].GetComponent<RandomImage>().changeToPass();
        allImages[randomRow2, randomCol2 + 3].GetComponent<RandomImage>().changeToPass();
    }
}