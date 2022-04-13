//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames.BasicApi.SavedGame;
//using GooglePlayGames.BasicApi.Events;


//public class GPGSBinder
//{
//    static GPGSBinder inst = new GPGSBinder();
//    public static GPGSBinder Inst => inst;



//    ISavedGameClient SavedGame =>
//        PlayGamesPlatform.Instance.SavedGame;

//    IEventsClient Events =>
//        PlayGamesPlatform.Instance.Events;



//    void Init()
//    {
//        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
//        PlayGamesPlatform.InitializeInstance(config);
//        PlayGamesPlatform.DebugLogEnabled = true;
//        PlayGamesPlatform.Activate();
//    }


//    public void Login(Action<bool, UnityEngine.SocialPlatforms.ILocalUser> onLoginSuccess = null)
//    {
//        Init();
//        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
//        {
//            onLoginSuccess?.Invoke(success == SignInStatus.Success, Social.localUser);
//        });
//    }

//    public void Logout()
//    {
//        PlayGamesPlatform.Instance.SignOut();
//    }


//    public void SaveCloud(string fileName, string saveData, Action<bool> onCloudSaved = null)
//    {
//        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
//            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
//            {
//                if (status == SavedGameRequestStatus.Success)
//                {
//                    var update = new SavedGameMetadataUpdate.Builder().Build();
//                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(saveData);
//                    SavedGame.CommitUpdate(game, update, bytes, (status2, game2) =>
//                    {
//                        onCloudSaved?.Invoke(status2 == SavedGameRequestStatus.Success);
//                    });
//                }
//            });
//    }

//    public void LoadCloud(string fileName, Action<bool, string> onCloudLoaded = null)
//    {
//        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
//            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
//            {
//                if (status == SavedGameRequestStatus.Success)
//                {
//                    SavedGame.ReadBinaryData(game, (status2, loadedData) =>
//                    {
//                        if (status2 == SavedGameRequestStatus.Success)
//                        {
//                            string data = System.Text.Encoding.UTF8.GetString(loadedData);
//                            onCloudLoaded?.Invoke(true, data);
//                        }
//                        else
//                            onCloudLoaded?.Invoke(false, null);
//                    });
//                }
//            });
//    }

//    public void DeleteCloud(string fileName, Action<bool> onCloudDeleted = null)
//    {
//        SavedGame.OpenWithAutomaticConflictResolution(fileName,
//            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) =>
//            {
//                if (status == SavedGameRequestStatus.Success)
//                {
//                    SavedGame.Delete(game);
//                    onCloudDeleted?.Invoke(true);
//                }
//                else
//                    onCloudDeleted?.Invoke(false);
//            });
//    }


//    public void ShowAchievementUI() =>
//        Social.ShowAchievementsUI();

//    public void UnlockAchievement(string gpgsId, Action<bool> onUnlocked = null) =>
//        Social.ReportProgress(gpgsId, 100, success => onUnlocked?.Invoke(success));

//    public void IncrementAchievement(string gpgsId, int steps, Action<bool> onUnlocked = null) =>
//        PlayGamesPlatform.Instance.IncrementAchievement(gpgsId, steps, success => onUnlocked?.Invoke(success));


//    public void ShowAllLeaderboardUI() =>
//        Social.ShowLeaderboardUI();

//    public void ShowTargetLeaderboardUI(string gpgsId) =>
//        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(gpgsId);

//    public void ReportLeaderboard(string gpgsId, long score, Action<bool> onReported = null) =>
//        Social.ReportScore(score, gpgsId, success => onReported?.Invoke(success));

//    public void LoadAllLeaderboardArray(string gpgsId, Action<UnityEngine.SocialPlatforms.IScore[]> onloaded = null) =>
//        Social.LoadScores(gpgsId, onloaded);

//    public void LoadCustomLeaderboardArray(string gpgsId, int rowCount, LeaderboardStart leaderboardStart,
//        LeaderboardTimeSpan leaderboardTimeSpan, Action<bool, LeaderboardScoreData> onloaded = null)
//    {
//        PlayGamesPlatform.Instance.LoadScores(gpgsId, leaderboardStart, rowCount, LeaderboardCollection.Public, leaderboardTimeSpan, data =>
//        {
//            onloaded?.Invoke(data.Status == ResponseStatus.Success, data);
//        });
//    }


//    public void IncrementEvent(string gpgsId, uint steps)
//    {
//        Events.IncrementEvent(gpgsId, steps);
//    }

//    public void LoadEvent(string gpgsId, Action<bool, IEvent> onEventLoaded = null)
//    {
//        Events.FetchEvent(DataSource.ReadCacheOrNetwork, gpgsId, (status, iEvent) =>
//        {
//            onEventLoaded?.Invoke(status == ResponseStatus.Success, iEvent);
//        });
//    }

//    public void LoadAllEvent(Action<bool, List<IEvent>> onEventsLoaded = null)
//    {
//        Events.FetchAllEvents(DataSource.ReadCacheOrNetwork, (status, events) =>
//        {
//            onEventsLoaded?.Invoke(status == ResponseStatus.Success, events);
//        });
//    }

//}








//Test.cs 소스입니다



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Test : MonoBehaviour
//{
//    string log;


//    void OnGUI()
//    {
//        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 3);


//        if (GUILayout.Button("ClearLog"))
//            log = "";

//        if (GUILayout.Button("Login"))
//            GPGSBinder.Inst.Login((success, localUser) =>
//            log = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");

//        if (GUILayout.Button("Logout"))
//            GPGSBinder.Inst.Logout();

//        if (GUILayout.Button("SaveCloud"))
//            GPGSBinder.Inst.SaveCloud("mysave", "want data", success => log = $"{success}");

//        if (GUILayout.Button("LoadCloud"))
//            GPGSBinder.Inst.LoadCloud("mysave", (success, data) => log = $"{success}, {data}");

//        if (GUILayout.Button("DeleteCloud"))
//            GPGSBinder.Inst.DeleteCloud("mysave", success => log = $"{success}");

//        if (GUILayout.Button("ShowAchievementUI"))
//            GPGSBinder.Inst.ShowAchievementUI();

//        if (GUILayout.Button("UnlockAchievement_one"))
//            GPGSBinder.Inst.UnlockAchievement(GPGSIds.achievement_one, success => log = $"{success}");

//        if (GUILayout.Button("UnlockAchievement_two"))
//            GPGSBinder.Inst.UnlockAchievement(GPGSIds.achievement_two, success => log = $"{success}");

//        if (GUILayout.Button("IncrementAchievement_three"))
//            GPGSBinder.Inst.IncrementAchievement(GPGSIds.achievement_three, 1, success => log = $"{success}");

//        if (GUILayout.Button("ShowAllLeaderboardUI"))
//            GPGSBinder.Inst.ShowAllLeaderboardUI();

//        if (GUILayout.Button("ShowTargetLeaderboardUI_num"))
//            GPGSBinder.Inst.ShowTargetLeaderboardUI(GPGSIds.leaderboard_num);

//        if (GUILayout.Button("ReportLeaderboard_num"))
//            GPGSBinder.Inst.ReportLeaderboard(GPGSIds.leaderboard_num, 1000, success => log = $"{success}");

//        if (GUILayout.Button("LoadAllLeaderboardArray_num"))
//            GPGSBinder.Inst.LoadAllLeaderboardArray(GPGSIds.leaderboard_num, scores =>
//            {
//                log = "";
//                for (int i = 0; i < scores.Length; i++)
//                    log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
//            });

//        if (GUILayout.Button("LoadCustomLeaderboardArray_num"))
//            GPGSBinder.Inst.LoadCustomLeaderboardArray(GPGSIds.leaderboard_num, 10,
//                GooglePlayGames.BasicApi.LeaderboardStart.PlayerCentered, GooglePlayGames.BasicApi.LeaderboardTimeSpan.Daily, (success, scoreData) =>
//                {
//                    log = $"{success}\n";
//                    var scores = scoreData.Scores;
//                    for (int i = 0; i < scores.Length; i++)
//                        log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
//                });

//        if (GUILayout.Button("IncrementEvent_event"))
//            GPGSBinder.Inst.IncrementEvent(GPGSIds.event_event, 1);

//        if (GUILayout.Button("LoadEvent_event"))
//            GPGSBinder.Inst.LoadEvent(GPGSIds.event_event, (success, iEvent) =>
//            {
//                log = $"{success}, {iEvent.Name}, {iEvent.CurrentCount}";
//            });

//        if (GUILayout.Button("LoadAllEvent"))
//            GPGSBinder.Inst.LoadAllEvent((success, iEvents) =>
//            {
//                log = $"{success}\n";
//                foreach (var iEvent in iEvents)
//                    log += $"{iEvent.Name}, {iEvent.CurrentCount}\n";
//            });

//        GUILayout.Label(log);
//    }
//}

