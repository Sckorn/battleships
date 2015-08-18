using UnityEngine;
using System.Collections;
using System;

public class itemsRelocation : MonoBehaviour {
    private int currentHour;
	// Use this for initialization
	void Start () {
        this.currentHour = DateTime.Now.Hour;

        if (this.currentHour <= 7 || this.currentHour >= 21)
        {
            GameObject.Find("Nighttime Simple Water").SetActive(true);
            GameObject.Find("Daylight Simple Water").SetActive(false);
        }
        else if (this.currentHour >= 7 && this.currentHour <= 20)
        {
            GameObject.Find("Nighttime Simple Water").SetActive(false);
            GameObject.Find("Daylight Simple Water").SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    void Awake()
    {
        // "�����" ������ � ������������������ ��������� ����� ������ (���� ������ � ������ ����) 
        //����� ������������� � ������� ����� ������ ����� ��� ��� ������� ��������
        //������ ���� ������������� � ��������� 2/3 ����
        //������ ����� �������� ���������� ������
        //������� ������ ������������� ������������ ������ ������� ������ �������� �����
        //����� ���������� � ����� ������ ���� (0, 0)
        Rect txtSize; // ������ ������ ���� � �������� �� ������
        GUIText gameTitle = GameObject.Find("gameTitle").GetComponent<GUIText>(); //�������� �������
        txtSize = gameTitle.GetScreenRect();//����� �������� � �������
        double fhRatio = (double)(gameTitle.fontSize / txtSize.height); //����������� ����������� ����������� ������� ������ �� �������� ������ � ��������

        int thirdPartOfScreen = Screen.height / 3; //����������� ������ ������ ������ (� ��� � ��� ����� ����� ����)

        float result = (float)(thirdPartOfScreen * fhRatio); // �������� ������ ������ ������� �������� ��� ������ ���������, � ������� ����� � ��������� ������
        int aimFontSize = (int) Mathf.Round(result); //�������� ���� ������ � ������ �������
        gameTitle.fontSize = aimFontSize; // ������������� ����������� �����
        txtSize = gameTitle.GetScreenRect(); // ��������� ������� ����� ��������� ������
        float scrnTop = Screen.height - txtSize.yMax; // ��������� �������� �� ������� ���� �������� ����� ����� ������ ��� �� ��� ���� ����� ������
        float MarginTop = 15; // ������ ������ ��� ������
        scrnTop -= MarginTop; // ��������� �� �������� ������� �� ����� ������
        gameTitle.pixelOffset = new Vector2(0, this.ToEngineCoords(-scrnTop)); // ����� ����� ������ ������ ToEngineCoords ����������� ���������� � ������ ������
        //float endOfGtitle = txtSize.yMin; // ��������� ������ ������� ����� ��� ���������� �������� ������� ����

        float availableTop = Screen.height - thirdPartOfScreen; // ��������� ������ �� ���� ���������� 2/3 ������ � ������� ����� ������ ����
        availableTop -= MarginTop; // �������� �� ��������� ������ ������� �������
        float perMenuItemHeight = Mathf.Round(availableTop / 3); // ������� ������ ������ ������ ���� � �������� � �������� 2/3 ������
        result = perMenuItemHeight * (float)(fhRatio); // �������� ������ ������ ��� ������ ���� � ���� ����� � ��������� ������
        aimFontSize = (int)Mathf.Round(result); // �������� ������ ������ � ������ �����

        GUIText nG = GameObject.Find("newGame").GetComponent<GUIText>(); // �������� ����� ���� ����� ����
        nG.fontSize = aimFontSize; // ������������� ��� ����������� ������ ������
        Rect ngSize = nG.GetScreenRect(); // �������� ������� � �������� ������ ���� ����� ����

        GUIText mPlayer = GameObject.Find("multiplayer").GetComponent<GUIText>(); // �������� ����� ���� ������� ����
        mPlayer.fontSize = aimFontSize; // ������������� ��� ����������� ������ ������
        Rect mpSize = mPlayer.GetScreenRect(); // �������� ������� � �������� ������ ���� ������� ����
        mPlayer.pixelOffset = new Vector2(0, this.ToEngineCoords((mpSize.height))); // ������������ ������ �������� ������ ��� ������, ��� �� ��������� �� ��������� ������� ���� �� �����

        GUIText eG = GameObject.Find("exitGame").GetComponent<GUIText>(); // �������� ����� ���� ����� �� ����
        eG.fontSize = aimFontSize; // ������������� ��� ����������� ������ ������
        Rect egSize = eG.GetScreenRect(); // �������� ��� ������� � ��������
        eG.pixelOffset = new Vector2(0, this.ToEngineCoords(mpSize.height + egSize.height)); // ������������� ��� �������� ������ ������ ����������� ������(���� �� ����) � ��� ������
    }

    float ToEngineCoords(float val)
    {
        val = -1 * val;
        return val;
    }
}
