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
        // "умный" ресайз и репозиционирование элементов татйл скрина (гейм татйтл и пункты меню) 
        //титла располагается в верхней трети экрана прямо под его верхней границей
        //пункты меню располагаются в остальных 2/3 окна
        //каждый пункт занимает одинаковую высоту
        //размеры шрифта высчитываются относительно высоыт которую должен занимать текст
        //экран начинается в лвеом нижнем углу (0, 0)
        Rect txtSize; // размер татйлы игры в пикселах на экране
        GUIText gameTitle = GameObject.Find("gameTitle").GetComponent<GUIText>(); //получаем татйтлу
        txtSize = gameTitle.GetScreenRect();//потом получаем её размеры
        double fhRatio = (double)(gameTitle.fontSize / txtSize.height); //расчитываем коэффициент зависимости размера шрифта от реальной высоты в пикселах

        int thirdPartOfScreen = Screen.height / 3; //расчитываем высоту третти экрана (в ней у нас будет титла игры)

        float result = (float)(thirdPartOfScreen * fhRatio); // получаем размер шрифта который заполнит эту высоту полностью, в формате числа с плавающей точкой
        int aimFontSize = (int) Mathf.Round(result); //приводим этот размер к целому формату
        gameTitle.fontSize = aimFontSize; // устанавливаем необходимый шрифт
        txtSize = gameTitle.GetScreenRect(); // обновляем размеры после изменения шрифта
        float scrnTop = Screen.height - txtSize.yMax; // вычисляем значение на которое надо сместить титлу вверх экрана что бы она была точно вверху
        float MarginTop = 15; // отступ сверху для текста
        scrnTop -= MarginTop; // отступаем на величину отступа от верха экрана
        gameTitle.pixelOffset = new Vector2(0, this.ToEngineCoords(-scrnTop)); // задаём новый оффсет текста ToEngineCoords преобразует координаты в формат движка
        //float endOfGtitle = txtSize.yMin; // сохраняем нижнюю границу титлы для дальнейшей проверки пунктов меню

        float availableTop = Screen.height - thirdPartOfScreen; // вычисляем высоту от нуля оставшихся 2/3 экрана в которых будут пункты меню
        availableTop -= MarginTop; // вычитаем из доступной высоты верхний мэрджин
        float perMenuItemHeight = Mathf.Round(availableTop / 3); // считаем высоту одного пункта меню в пикселях в пределах 2/3 экрана
        result = perMenuItemHeight * (float)(fhRatio); // получаем размер шрифта для пункта меню в виде числа с плавающей точкой
        aimFontSize = (int)Mathf.Round(result); // приводим размер шрифта к целому числу

        GUIText nG = GameObject.Find("newGame").GetComponent<GUIText>(); // получаем пункт меню новая игра
        nG.fontSize = aimFontSize; // устанавливаем ему высчитанный размер шрифта
        Rect ngSize = nG.GetScreenRect(); // получаем размеры в пикселях пункта меню новая игра

        GUIText mPlayer = GameObject.Find("multiplayer").GetComponent<GUIText>(); // получаем пункт меню сетевая игра
        mPlayer.fontSize = aimFontSize; // устанавливаем ему высчитанный размер шрифта
        Rect mpSize = mPlayer.GetScreenRect(); // получаем размеры в пикселях пункта меню сетевая игра
        mPlayer.pixelOffset = new Vector2(0, this.ToEngineCoords((mpSize.height))); // устаналиваем пунтку смещение равное его высоте, что бы избавится от наложения пунктов друг на друга

        GUIText eG = GameObject.Find("exitGame").GetComponent<GUIText>(); // получаем пункт меню выход из игры
        eG.fontSize = aimFontSize; // устаналвиваем ему высчитанный размер шрифта
        Rect egSize = eG.GetScreenRect(); // получаем его размеры в пикселях
        eG.pixelOffset = new Vector2(0, this.ToEngineCoords(mpSize.height + egSize.height)); // устанавливаем ему смещение равное высоте предидущего пункта(игра по сети) и его высоте
    }

    float ToEngineCoords(float val)
    {
        val = -1 * val;
        return val;
    }
}
