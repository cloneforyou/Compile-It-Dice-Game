﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CompileItCLI
{
    class Suicide
    {
        public void SuicideScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(@"                                                   |
               ...                                 |
             ;::::;                                |
           ;::::; :;                               |
         ;:::::'   :;                              |
        ;:::::;     ;.                             |
       ,:::::'       ;           OOO\              |
       ::::::;       ;          OOOOO\             |
       ;:::::;       ;         OOOOOOOO            |
      ,;::::::;     ;'         / OOOOOOO           |
    ;:::::::::`. ,,,;.        /  / DOOOOOO         |
  .';:::::::::::::::::;,     /  /     DOOOO        |
 ,::::::;::::::;;;;::::;,   /  /        DOOO       |
;`::::::`'::::::;;;::::: ,#/  /          DOOO      |
:`:::::::`;::::::;;::: ;::#  /            DOOO     |
::`:::::::`;:::::::: ;::::# /              DOO     |
`:`:::::::`;:::::: ;::::::#/               DOO     |
 :::`:::::::`;; ;:::::::::##                OO     |
 ::::`:::::::`;::::::::;:::#                OO     |
 `:::::`::::::::::::;'`:;::#                O      |
  `:::::`::::::::;' /  / `:#                       |
   ::::::`:::::;'  /  /   `#                       |");

            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
