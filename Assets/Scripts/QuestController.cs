using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class QuestController 
{
    private static List<Quest> QuestList = InitializeQuestList();

    
    public static bool Check(PlayerModel player, List<PlayerModel> players)
    {
       if(player.Quest.Check(player.Score, players))
        {
           return true;
       }
        return false;
    }

    public static Quest GetRandomQuest()
    {
        //get a random quest from questlist not used yet
        Quest result = QuestList[Random.Range(0, QuestList.Count)];
        QuestList.Remove(result);
        return result;
    }

    private static List<Quest> InitializeQuestList()
    {
        var QuestList = new List<Quest>();
        QuestList.AddRange(AddQuestMoreOfAKind());
        QuestList.AddRange(AddQuestLessOfAKind());
        QuestList.AddRange(AddQuestMediocrity());
        return QuestList;

    }

    private static IEnumerable<Quest> AddQuestLessOfAKind()
    {
        List<Quest> result = new List<Quest>();
        foreach (var item in GameModel.Risorse)
        {
            result.Add(new Quest()
            {
                Description = "Sii il giocatore con più " + item.Description + " tra tutti i giocatori",
                Check = (Score score, List<PlayerModel> players) =>
                {
                    foreach (var item1 in players)
                    {
                        var itemType = item.GetType();
                        var scoreValue = (int)itemType.GetProperty(item.Type).GetValue(item1.Score);
                        var currentValue = (int)itemType.GetProperty(item.Type).GetValue(score);

                        if (scoreValue <= currentValue)
                        {
                            return false;
                        }
                    }
                    return true;
                },
            });
        }
        return result;
    }

    private static List<Quest> AddQuestMoreOfAKind() {
        List<Quest> result = new List<Quest>();
        foreach (var item in GameModel.Risorse)
        {
            result.Add(new Quest()
            {
                Description = "Sii il giocatore con meno " + item.Description + " tra tutti i giocatori",
                Check = (Score score, List<PlayerModel> players) =>
                {
                    foreach (var item1 in players)
                    {
                        var itemType = item.GetType();
                        var scoreValue = (int)itemType.GetProperty(item.Type).GetValue(item1.Score);
                        var currentValue = (int)itemType.GetProperty(item.Type).GetValue(score);

                        if (scoreValue >= currentValue)
                        {
                            return false;
                        }
                    }
                    return true;
                },
            });
        }
        return result;
    }

    private static List<Quest> AddQuestMediocrity()
    {
        List<Quest> result = new List<Quest>();
        foreach (var item in GameModel.Risorse)
        {
            result.Add(new Quest()
            {
                Description = "Per ogni risorsa non devi averne di più o di meno degli altri giocatori",
                Check = (Score score, List<PlayerModel> players) =>
                {
                    int MaxHorses = players.Max(x => x.Score.Horses);
                    int MinHorses = players.Min(x => x.Score.Horses);
                    int MaxCoppers = players.Max(x => x.Score.Coppers);
                    int MinCoppers = players.Min(x => x.Score.Coppers);
                    int MaxIrons = players.Max(x => x.Score.Irons);
                    int MinIrons = players.Min(x => x.Score.Irons);
                    int MaxWheat = players.Max(x => x.Score.Wheat);
                    int MinWheat = players.Min(x => x.Score.Wheat);
                    int MaxSalt = players.Max(x => x.Score.Salt);
                    int MinSalt = players.Min(x => x.Score.Salt);

                    foreach (var item1 in players)
                    {
                       if(item1.Score.Horses == MaxHorses || item1.Score.Horses == MinHorses)
                        {
                            return false;
                        }
                        if (item1.Score.Coppers == MaxCoppers || item1.Score.Coppers == MinCoppers)
                        {
                            return false;
                        }
                        if (item1.Score.Irons == MaxIrons || item1.Score.Irons == MinIrons)
                        {
                            return false;
                        }
                        if (item1.Score.Wheat == MaxWheat || item1.Score.Wheat == MinWheat)
                        {
                            return false;
                        }
                        if (item1.Score.Salt == MaxSalt || item1.Score.Salt == MinSalt)
                        {
                            return false;
                        }
                    }
                    return true;
                },
            });
        }
        return result;
    }

}
