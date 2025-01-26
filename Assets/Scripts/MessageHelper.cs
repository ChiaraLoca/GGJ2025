using System;

using System.IO;

using System.Text.RegularExpressions;



public static class MessageHelper
{
    private static string LayoutPlayerRegistration = "Assets/Scripts/Layouts/LayoutInizio.html";
    private static string LayoutInizioPath = "Assets/Scripts/Layouts/Epistola.html";
    private static string LayoutEstrattoConto = "Assets/Scripts/Layouts/LayoutEstrattoConto.html";
    private static string LayoutScomunica = "Assets/Scripts/Layouts/LayoutScomunica.html";
    private static string LayoutPartialErrors = "Assets/Scripts/Layouts/LayoutPartialErrors.html";
    private static string LayoutVittoria = "Assets/Scripts/Layouts/LayoutVittoria.html";
    private static string LayoutSconfitta = "Assets/Scripts/Layouts/LayoutSconfitta.html";

    public static string GetMailTextGameStart(string NomePlayer, string Obiettivo)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutInizioPath);
            htmlContent = htmlContent.Replace("{{{PlayerName}}}", NomePlayer);
            htmlContent = htmlContent.Replace("{{{OBIETTIVO}}}", Obiettivo);
            foreach (var item in GameModel.Risorse)
            {
                htmlContent = htmlContent.Replace("{{{"+ item.Type + "}}}", "<li>100 "+ item.Description +"</li>");
              
            }
            htmlContent = Regex.Replace(htmlContent, @"({{{[^>]+}}})", "");

            return htmlContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    public static string GetMailTextPlayerRegistration(string NomePlayer)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutPlayerRegistration);
            htmlContent = htmlContent.Replace("{{{PlayerName}}}", NomePlayer);
            return htmlContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    public static string GetMailTextEstrattoConto(PlayerModel player, int righeEseguite)
    {
        try
        {
            string htmlContent = File.ReadAllText(LayoutEstrattoConto);
            htmlContent = htmlContent.Replace("{{{PlayerName}}}", player.Name);
            foreach (var item in GameModel.Risorse)
            {
                htmlContent = htmlContent.Replace("{{{"+item.Type +"}}}",
                   " <p><b>"+ item.Description+":</b>"+ player.Score.getResource(item.Type).ToString() + "</p>" );
            }
            htmlContent = htmlContent.Replace("{{{Eseguite}}}", getLayoutRigheEseguite(righeEseguite));
            htmlContent = Regex.Replace(htmlContent, @"({{{[^>]+}}})", "");
            return htmlContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura del file: " + ex.Message);
            return "";
        }
    }

    private static string getLayoutRigheEseguite(int righeEseguite)
    {
        if (righeEseguite == 0)
        {
            return "";
        }
        return File.ReadAllText(LayoutPartialErrors).Replace("{{{Eseguite}}}", righeEseguite.ToString());
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
