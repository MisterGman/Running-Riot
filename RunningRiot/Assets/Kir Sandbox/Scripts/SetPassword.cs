using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPassword : MonoBehaviour {
    public Image[,] allImages;

    public RandomImage passImage;

    float oldRR;
    float oldRCL;
    float oldRCR;

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
    // Use this for initialization
    void Start () {
        oldRR = 0;
        oldRCL = 0;
        oldRCR= 0;

        allImages = new Image[5,10];

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

        allImages[1, 0] = image10;
        allImages[1, 1] = image11;
        allImages[1, 2] = image12;
        allImages[1, 3] = image13;
        allImages[1, 4] = image14;
        allImages[1, 5] = image15;
        allImages[1, 6] = image16;
        allImages[1, 7] = image17;
        allImages[1, 8] = image18;
        allImages[1, 9] = image19;

        allImages[2, 0] = image20;
        allImages[2, 1] = image21;
        allImages[2, 2] = image22;
        allImages[2, 3] = image23;
        allImages[2, 4] = image24;
        allImages[2, 5] = image25;
        allImages[2, 6] = image26;
        allImages[2, 7] = image27;
        allImages[2, 8] = image28;
        allImages[2, 9] = image29;

        allImages[3, 0] = image30;
        allImages[3, 1] = image31;
        allImages[3, 2] = image32;
        allImages[3, 3] = image33;
        allImages[3, 4] = image34;
        allImages[3, 5] = image35;
        allImages[3, 6] = image36;
        allImages[3, 7] = image37;
        allImages[3, 8] = image38;
        allImages[3, 9] = image39;

        allImages[4, 0] = image40;
        allImages[4, 1] = image41;
        allImages[4, 2] = image42;
        allImages[4, 3] = image43;
        allImages[4, 4] = image44;
        allImages[4, 5] = image45;
        allImages[4, 6] = image46;
        allImages[4, 7] = image47;
        allImages[4, 8] = image48;
        allImages[4, 9] = image49;


        InvokeRepeating("SetPass", 2.0f, 5.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void SetPass()
    {
        int randomRow = UnityEngine.Random.Range(0, allImages.GetLength(0));
        if (randomRow == oldRR)
        {
            randomRow = UnityEngine.Random.Range(0, allImages.GetLength(0));
        }
        else
        {
            oldRR = randomRow;
        }

        int randomColLeft = UnityEngine.Random.Range(0, 2/*allImages.GetLength(1) - 7*/);
        if(randomColLeft == oldRCL)
        {
            randomColLeft = UnityEngine.Random.Range(0, 2/*allImages.GetLength(1) - 7*/);
        }
        else
        {
            oldRCL = randomColLeft;
        }

        int randomColRight = UnityEngine.Random.Range(5, 7/*allImages.GetLength(1) - 2*/);
        if(randomColRight == oldRCR)
        {
            randomColRight = UnityEngine.Random.Range(5, 7/*allImages.GetLength(1) - 2*/);
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
        allImages[randomRow, randomColRight + 2].GetComponent<RandomImage>().changeToPass();
        
    }
}
