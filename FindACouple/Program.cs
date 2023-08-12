using System;
using SFML.Learning;
using SFML.Window;

class Program : Game
{
    static string background = LoadTexture("background.png");
    static string backgroundMenu = LoadTexture("backgroundMenu.png");
    static string backCard = LoadTexture("backCard.png");
    static string blueCard = LoadTexture("blueCard.png");
    static string cyanCard = LoadTexture("cyanCard.png");
    static string darkBlueCard = LoadTexture("darkBlueCard.png");
    static string darkOrangeCard = LoadTexture("darkOrangeCard.png");
    static string darkVioletCard = LoadTexture("darkVioletCard.png");
    static string darkYellowCard = LoadTexture("darkYellowCard.png");
    static string greenCard = LoadTexture("greenCard.png");
    static string lightBlueCard = LoadTexture("lightBlueCard.png");
    static string lightGreenCard = LoadTexture("lightGreenCard.png");
    static string lightRedCard = LoadTexture("lightRedCard.png");
    static string orangeCard = LoadTexture("orangeCard.png");
    static string pinkCard = LoadTexture("pinkCard.png");
    static string redCard = LoadTexture("redCard.png");
    static string violetCard = LoadTexture("VioletCard.png");
    static string yellowCard = LoadTexture("yellowCard.png");

    static string clickedSound = LoadSound("clickedSound.wav");
    static string winSound = LoadSound("winSound.wav");
    static string defeatSound = LoadSound("defeatSound.wav");
    static string backgroundMusic = LoadMusic("backgroundMusic.wav");

    static Random rnd = new Random();
    static int[,] cards;
    static int cardID = 1;
    static int level = 0;
    static int numberOfOpenCards = 0;
    static int openCardIndex1;
    static int openCardIndex2;
    static int timer = 0;
    static int mainTimer = 0;
    static bool startGame = false;
    static int menuOption = 0;
    static bool isDefeat = false;

    static int cardAmount = 12;                             // Количество карт
    static int cardState = 1;                               // Состояние карт. -1 - скрыта, 0 - закрыта, 1 - открыта
    static int leftOffset = 250;                             // Отступ слева
    static int topOffset = 10;                              // Отступ сверху
    static int spaceBetweenCardsX = 100;                     // Пробел между картами по X
    static int spaceBetweenCardsY = 100;                     // Пробел медлу картами по Y  
    static int countPerLine = 4;                            // Количество карт в строке
    static int cardWidth = 135;                             // Ширина карты
    static int cardHeight = 180;                            // Высота карты

    static void InitializationCards()
    {
        cards = new int[cardAmount, 6];

        for (int i = 0; i < cardAmount; i++)
        {
            if (i != 0 && i % 2 == 0) cardID++;

            cards[i, 0] = cardState;                                                                // 0 - Cостояние
            cards[i, 1] = (i % countPerLine) * (cardWidth + spaceBetweenCardsX) + leftOffset;       // 1 - Позиция по X
            cards[i, 2] = (i / countPerLine) * (cardHeight + spaceBetweenCardsY) + topOffset;       // 2 - Позиция по Y
            cards[i, 3] = cardWidth;                                                                // 3 - Ширина карты
            cards[i, 4] = cardHeight;                                                               // 4 - Высота карты
            cards[i, 5] = cardID;                                                                   // 5 - ID карты

            if (i == cardAmount - 1) cardID = 1;
        }
    }

    static void DrawCards()
    {
        for (int i = 0; i < cardAmount; i++)
        {
            if (cards[i, 0] == 0) DrawSprite(backCard, cards[i, 1], cards[i, 2]);

            if (cards[i, 0] == 1)
            {
                if (cards[i, 5] == 1) DrawSprite(blueCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 2) DrawSprite(greenCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 3) DrawSprite(redCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 4) DrawSprite(pinkCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 5) DrawSprite(orangeCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 6) DrawSprite(yellowCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 7) DrawSprite(violetCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 8) DrawSprite(cyanCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 9) DrawSprite(lightGreenCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 10) DrawSprite(lightRedCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 11) DrawSprite(lightBlueCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 12) DrawSprite(darkYellowCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 13) DrawSprite(darkVioletCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 14) DrawSprite(darkOrangeCard, cards[i, 1], cards[i, 2]);
                if (cards[i, 5] == 15) DrawSprite(darkBlueCard, cards[i, 1], cards[i, 2]);
            }
        }
    }

    static void MixingCards(int[,] Mix)
    {
        for (int t = 0; t < 10; t++)
        {
            for (int i = 0; i < cardAmount; i++)
            {
                int random = Mix[rnd.Next(0, cardAmount), 5];
                int cash = Mix[random, 5];
                Mix[random, 5] = Mix[i, 5];
                Mix[i, 5] = cash;
            }
        }
    }

    static int GetIndexCardByMousePosition()
    {
        for (int i = 0; i < cardAmount; i++)
        {
            if (MouseX > cards[i, 1] && MouseX < cards[i, 1] + cardWidth && MouseY > cards[i, 2] && MouseY < cards[i, 2] + cardHeight) return i;
        }
        return -1;
    }

    static void SetStateToAllCard(int state)
    {
        for (int i = 0; i < cardAmount; i++)
        {
            cards[i, 0] = state;
        }
    }

    static void Main(string[] args)
    {
        SetFont("Caveat-Regular.ttf");

        InitWindow(1600, 900, "Find a Couple");

        while (true)
        {
            DispatchEvents();

            ClearWindow();

            PlayMusic(backgroundMusic, 20);

            DrawSprite(backgroundMenu, 0, 0);

            if (menuOption == 0)
            {
                if (MouseX > 600 && MouseX < 1000 && MouseY > 400 && MouseY < 480 && GetMouseButtonUp(0) == true) menuOption = 1;
                if (MouseX > 600 && MouseX < 1000 && MouseY > 550 && MouseY < 630 && GetMouseButtonUp(0) == true) break;

                SetFillColor(216, 191, 216);
                FillRectangle(600, 400, 400, 80);
                FillRectangle(600, 550, 400, 80);

                SetFillColor(105, 105, 105);
                DrawText(650, 390, "Начать игру", 70);
                DrawText(720, 540, "Выход", 70);
            }

            if (menuOption == 1)
            {
                if (MouseX > 600 && MouseX < 1000 && MouseY > 100 && MouseY < 180 && GetMouseButtonUp(0) == true) { level = 1; startGame = true; }
                if (MouseX > 600 && MouseX < 1000 && MouseY > 250 && MouseY < 330 && GetMouseButtonUp(0) == true) { level = 2; startGame = true; }
                if (MouseX > 600 && MouseX < 1000 && MouseY > 550 && MouseY < 630 && GetMouseButtonUp(0) == true) { level = 3; startGame = true; }
                if (MouseX > 600 && MouseX < 1000 && MouseY > 700 && MouseY < 780 && GetMouseButtonUp(0) == true) { level = 4; startGame = true; }
                if (MouseX > 1100 && MouseX < 1500 && MouseY > 800 && MouseY < 880 && GetMouseButtonUp(0) == true) menuOption = 0;

                SetFillColor(216, 191, 216);
                FillRectangle(600, 100, 400, 80);
                FillRectangle(600, 250, 400, 80);
                FillRectangle(600, 550, 400, 80);
                FillRectangle(600, 700, 400, 80);
                FillRectangle(1100, 800, 400, 80);

                SetFillColor(105, 105, 105);
                DrawText(680, 90, "Уровень 1", 70);
                DrawText(680, 240, "Уровень 2", 70);
                DrawText(680, 540, "Уровень 3", 70);
                DrawText(680, 690, "Уровень 4", 70);
                DrawText(1230, 790, "Меню", 70);
            }

            DisplayWindow();

            Delay(1);

            if (startGame == true)
            {
                if (level == 1)
                {
                    cardAmount = 12;                             // Количество карт
                    leftOffset = 380;                             // Отступ слева
                    topOffset = 120;                              // Отступ сверху
                    spaceBetweenCardsX = 100;                     // Пробел между картами по X
                    spaceBetweenCardsY = 60;                     // Пробел медлу картами по Y  
                    countPerLine = 4;                            // Количество карт в строке
                }

                if (level == 2)
                {
                    cardAmount = 18;                             // Количество карт
                    leftOffset = 290;                             // Отступ слева
                    topOffset = 120;                              // Отступ сверху
                    spaceBetweenCardsX = 60;                     // Пробел между картами по X
                    spaceBetweenCardsY = 60;                     // Пробел медлу картами по Y  
                    countPerLine = 6;                            // Количество карт в строке
                }

                if (level == 3)
                {
                    cardAmount = 24;                             // Количество карт
                    leftOffset = 290;                             // Отступ слева
                    topOffset = 45;                              // Отступ сверху
                    spaceBetweenCardsX = 60;                     // Пробел между картами по X
                    spaceBetweenCardsY = 30;                     // Пробел медлу картами по Y  
                    countPerLine = 6;                            // Количество карт в строке
                }

                if (level == 4)
                {
                    cardAmount = 30;                             // Количество карт
                    leftOffset = 290;                             // Отступ слева
                    topOffset = 0;                              // Отступ сверху
                    spaceBetweenCardsX = 60;                     // Пробел между картами по X
                    spaceBetweenCardsY = 0;                     // Пробел медлу картами по Y  
                    countPerLine = 6;                            // Количество карт в строке
                }

                int cardsLeft = cardAmount;
                InitializationCards();

                MixingCards(cards);

                SetStateToAllCard(1);
                DrawSprite(background, 0, 0);
                DrawCards();
                DisplayWindow();
                Delay(5000);

                SetStateToAllCard(0);

                while (true)
                {
                    DispatchEvents();

                    // Очистка
                    ClearWindow();

                    // Рассчёт
                    timer++;
                    mainTimer++;
                    PlayMusic(backgroundMusic, 20);

                    if (numberOfOpenCards == 2 && timer == 50)     // Когда открыты 2 карты
                    {
                        if (cards[openCardIndex1, 5] == (cards[openCardIndex2, 5]))     // Если ID карт совпадают
                        {
                            cards[openCardIndex1, 0] = -1;
                            cards[openCardIndex2, 0] = -1;
                            cardsLeft -= 2;
                            if (cardsLeft == 0) PlaySound(winSound);
                        }
                        else
                        {
                            cards[openCardIndex1, 0] = 0;
                            cards[openCardIndex2, 0] = 0;
                        }

                        numberOfOpenCards = 0;
                    }

                    if (GetMouseButtonUp(0) == true && GetIndexCardByMousePosition() != -1 && cards[GetIndexCardByMousePosition(), 0] != -1 && isDefeat == false)                 // Если нажата карта
                    {
                        if (numberOfOpenCards < 2)                                                          // Если кол-во открытых карт < 2 - карта открывается
                        {
                            cards[GetIndexCardByMousePosition(), 0] = 1;
                            PlaySound(clickedSound);

                            if (numberOfOpenCards == 0)                                                     // Картам присваивается индекс
                            {
                                openCardIndex1 = GetIndexCardByMousePosition();
                                numberOfOpenCards++;
                            }
                            if (numberOfOpenCards == 1 && openCardIndex1 != GetIndexCardByMousePosition())  // Если индекс 2 карты != 1 карте
                            {
                                openCardIndex2 = GetIndexCardByMousePosition();
                                numberOfOpenCards++;
                                timer = 0;
                            }
                        }
                    }

                    if (GetKeyDown(Keyboard.Key.Escape))
                    {
                        menuOption = 0;
                        level = 0;
                        startGame = false;
                        numberOfOpenCards = 0;
                        openCardIndex1 = -1;
                        openCardIndex2 = -2;
                        timer = 0;
                        mainTimer = 0;
                        isDefeat = false;
                        break;
                    }

                    if (GetKeyDown(Keyboard.Key.R))
                    {
                        if (isDefeat == true || cardsLeft == 0)
                        {
                            openCardIndex1 = -1;
                            openCardIndex2 = -2;
                            numberOfOpenCards = 0;
                            timer = 0;
                            mainTimer = 0;
                            isDefeat = false;
                            break;
                        }
                    }

                    // Отрисовка
                    DrawSprite(background, 0, 0);

                    DrawCards();

                    if (cardsLeft == 0)                 // Победа
                    {
                        SetFillColor(0, 0, 0);
                        DrawText(250, 300, "Победа! Нажми \"R\" чтобы перезапустить уровень", 70);
                        DrawText(280, 400, "\"Space\" - следующий уровень, \"Esc\" - меню", 70);

                        if (GetKeyDown(Keyboard.Key.Space))
                        {
                            if (level <= 4) level++;
                            openCardIndex1 = -1;
                            openCardIndex2 = -2;
                            mainTimer = 0;
                            break;
                        }
                    }

                    SetFillColor(0, 0, 0);
                    if (level == 1) DrawText(0, 800, "Оставшееся время: " + (30 - (mainTimer / 60)), 70);
                    if (level == 2) DrawText(0, 800, "Оставшееся время: " + (60 - (mainTimer / 60)), 70);
                    if (level == 3) DrawText(0, 800, "Оставшееся время: " + (90 - (mainTimer / 60)), 70);
                    if (level == 4) DrawText(0, 800, "Оставшееся время: " + (120 - (mainTimer / 60)), 70);

                    if ((mainTimer == 1800 && level == 1 && cardsLeft != 0) || (mainTimer == 3600 && level == 2 && cardsLeft != 0) || (mainTimer == 5400 && level == 3 && cardsLeft != 0) || (mainTimer == 7200 && level == 4 && cardsLeft != 0))
                    {
                        isDefeat = true;
                        PlaySound(defeatSound);
                    }

                    if (isDefeat == true)
                    {
                        DrawText(250, 300, "Поражение! Нажми \"R\" чтобы перезапустить уровень", 70);
                    }

                    DisplayWindow();

                    // Ожидание
                    Delay(1);
                }
            }
        }
    }
}
