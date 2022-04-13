using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//HBinaryMapLoader
namespace UKH
{
    [Serializable]
    public class SerializableGridData
    {
        public List<string[]> gridData = new List<string[]>();
    }
    public static class UBinaryMapLoder
    {
        public static SerializableGridData BinaryRead(string path)
        {
            if (File.Exists(path))
            {
                SerializableGridData SGD    = new SerializableGridData();
                BinaryFormatter bf          = new BinaryFormatter();
                FileStream file             = File.Open(path, FileMode.Open);

                SGD = bf.Deserialize(file) as SerializableGridData;
                file.Close();
                return SGD;
            }
            else
            {
                return null;
            }
        }
        public enum ETileLayerType
        {
            Instance = 0,
            Location,
            Collision,
            Trigger,
            Hill
        }
    }
}
//public static void BinaryWrite(List<List<OCell>> mapData, string path, ETileLayerType layerType)
//{
//    SerializableGridData MDB = new SerializableGridData();
//    int sizeX = mapData.Count;
//    int sizeY = mapData[0].Count;
//    for (int i = 0; i < sizeX; i++)
//    {
//        string[] row = new string[sizeY];
//        for (int j = 0; j < sizeY; j++)
//        {
//            if (layerType == ETileLayerType.Instance)
//                row[j] = mapData[i][j].GetInstanceGUID().ToString();
//            else if (layerType == ETileLayerType.Location)
//                row[j] = mapData[i][j].GetLocationType().ToString();
//            else if (layerType == ETileLayerType.Collision)
//                row[j] = mapData[i][j].GetIsCollision() ? "T" : "F";
//            else if (layerType == ETileLayerType.Trigger)
//                row[j] = mapData[i][j].GetEventTriggerIDX().ToString();
//            else if (layerType == ETileLayerType.Hill)
//                row[j] = mapData[i][j].GetIsHill() ? "T" : "F"; ;
//
//        }
//        MDB.gridData.Add(row);
//    }
//    if (File.Exists(path))
//    {
//        Debug.LogError("파일이 존재함- 덮어쓰겠음");
//        File.Delete(path);
//    }
//    BinaryFormatter bf = new BinaryFormatter();
//    FileStream file = File.Create(path);
//    bf.Serialize(file, MDB);
//    file.Close();
//}

//public static SerializableGridData Load(string pathDir, string filename, string extension)
//{
//    SerializableGridData returnData = new SerializableGridData();

//    DirectoryInfo di = new DirectoryInfo(pathDir);
//    foreach (FileInfo File in di.GetFiles())
//    {
//        if (File.Extension.ToLower().CompareTo($"{filename}.{extension}") == 0)
//        {
//            String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
//            String FullFilePath = File.FullName;
//            Debug.Log(FullFilePath + " " + FileNameOnly);

//            string encryptData = LoadFile(FullFilePath);
//            string decryptData = Decrypt(encryptData);

//            Debug.Log(decryptData);

//            SerializableETilePresetData data = JsonToData(decryptData);

//            ETPTileEntity returnOBJ = new ETPTileEntity();
//            returnOBJ._guidTOBJ = data._guidTOBJ;
//            returnOBJ._locationType = data._locationType;
//            returnOBJ._filename = data._filename;
//            returnOBJ._isBaseLocPlane = data._isBaseLocPlane;
//            returnOBJ._isUseAnim = data._isUseAnim;
//            returnOBJ._texture = Resources.Load<Texture2D>("MapData/Sprites/" + data._textureName);

//            if (data._animTextureNameList != null)
//                returnOBJ._animTextureList = new List<Texture2D>();

//            for (int i = 0; i < returnOBJ._animTextureList.Count; i++)
//                returnOBJ._animTextureList.Add(Resources.Load<Texture2D>("MapData/Sprite/" + data._animTextureNameList[i]));

//            returnOBJ._tile = Resources.Load<Tile>("MapData/Tile/" + data._tileName);
//            returnOBJ._isGatheringEvent = data._isEntityEvent;
//            returnOBJ._gatheringIdx = data._entityEventIdx;
//            if (returnOBJ._isGatheringEvent && returnOBJ._gatheringIdx > 999)
//            {
//                string idx = returnOBJ._gatheringIdx.ToString();
//                returnOBJ.gatheringObjLevel = Int32.Parse(idx[0].ToString());
//                int type = returnOBJ._gatheringIdx / 1000;
//                if (type < (int)EGatheringOBJType.max)
//                    returnOBJ.gatheringObjType = (EGatheringOBJType)(returnOBJ._gatheringIdx / 1000);
//            }
//            returnData.Add(returnOBJ);
//        }
//    }
//    return returnData;
//}