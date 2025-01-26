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

    public static Quest GetEmptyQuest()
    {
        return new Quest()
        {
            Description = "Nessuna missione",
            Check = (Score score, List<PlayerModel> players) =>
            {
                return true;
            },
        };
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
                        if (score.getResource(item.Type) <= item1.Score.getResource(item.Type))
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
                        if (score.getResource(item.Type) >= item1.Score.getResource(item.Type))
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
                    foreach(var r in GameModel.Risorse)
                    {
                        int MaxRes = players.Max(x => x.Score.getResource(r.Type));
                        int MinRes = players.Min(x => x.Score.getResource(r.Type));
                        if (score.getResource(r.Type) != MaxRes && score.getResource(r.Type) != MinRes)
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
