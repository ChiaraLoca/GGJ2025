using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class MessageHelper
{
    private static string LayoutInizioPath = "LayoutInizio.html";
    private static string LayoutEstrattoConto = "LayoutEstrattoConto.html";
    private static string LayoutScomunica = "LayoutScomunica.html";
    private static string LayoutPartialErrors = "LayoutPartialErrors.html";
    private static string LayoutVittoria = "LayoutVittoria.html";
    private static string LayoutSconfitta = "LayoutSconfitta.html";

    public static string GetMailTextGameStart(string NomePlayer)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutInizioPath);
            return htmlContent.Replace("{{{PlayerName}}}", NomePlayer);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    public static string GetMailTextEstrattoConto(PlayerModel player, int errors)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutEstrattoConto);
            htmlContent.Replace("{{{PlayerName}}}", player.Name);
            htmlContent.Replace("{{{Horses}}}", player.Score.Horses.ToString());
            htmlContent.Replace("{{{Coppers}}}", player.Score.Coppers.ToString());
            htmlContent.Replace("{{{Iron}}}", player.Score.Irons.ToString());
            htmlContent.Replace("{{{Wheat}}}", player.Score.Wheat.ToString());
            htmlContent.Replace("{{{Salt}}}", player.Score.Salt.ToString());
            htmlContent.Replace("{{{Errors}}}", getLayoutErrori(errors));
            return htmlContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    private static string getLayoutErrori(int errors)
    {
        if (errors == 0)
        {
            return "";
        }
        return File.ReadAllText(LayoutPartialErrors).Replace("{{{Errors}}}", errors.ToString());
    }

    public static string GetMailTextScomunica(string NomePlayer)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutScomunica);
            return htmlContent.Replace("{{{PlayerName}}}", NomePlayer);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    public static string GetMailTextVittoria(string NomePlayer)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutVittoria);
            return htmlContent.Replace("{{{PlayerName}}}", NomePlayer);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    public static string GetMailTextSconfitta(string NomePlayer)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutSconfitta);
            return htmlContent.Replace("{{{PlayerName}}}", NomePlayer);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }




}
