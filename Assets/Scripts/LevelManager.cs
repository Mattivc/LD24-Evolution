using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	public Transform playerTransform;
	
	public GameObject levelEndPrefab;
	public List<GameObject> levelBlockPrefabs;
	
	private static int levelSize = 32;
	
	private byte[,] levelData = new byte[levelSize,levelSize];
	private List<LevelBlock> levelBlockList = new List<LevelBlock>();
	
	private struct LevelBlock
	{
		public LevelBlock(int x, int y, byte blockID, GameObject block)
		{
			xPos = x;
			yPos = y;
			this.block = blockID;
			go = block;
		}
		
		public int xPos;
		public int yPos;
		
		public byte block;
		
		public GameObject go;
	}
	
	void Start()
	{
		// build level
		
		for (int xi = 0; xi < levelSize; xi++)
		{
			for (int yi = 0; yi < levelSize; yi++)
			{
				levelData[xi,yi] = (byte)Random.Range(0, levelBlockPrefabs.Count);
			}
		}
		
	}
	
	void Update()
	{
		Vector2 playerWorldPos = new Vector2(playerTransform.position.x, playerTransform.position.z);
		Vector2I playerLevelPos = WorldToLevelPos(playerWorldPos);
		
		
		Vector2I[] spawnPosArray = new Vector2I[]
		{
			playerLevelPos, // 0
			playerLevelPos - Vector2I.Rigth, // -x
			playerLevelPos + Vector2I.Rigth, // +x
			playerLevelPos - Vector2I.Up, // -y
			playerLevelPos + Vector2I.Up, // +y
			playerLevelPos + Vector2I.Rigth + Vector2I.Up, // +x +y
			playerLevelPos + Vector2I.Rigth - Vector2I.Up, // +x -y
			playerLevelPos - Vector2I.Rigth + Vector2I.Up, // -x +y
			playerLevelPos - Vector2I.Rigth - Vector2I.Up, // -x -y
		};
		
		
		for (int i = 0; i < 9; i++)
		{
			Vector2I spawnPos = spawnPosArray[i];
			
			if (spawnPos.x >= 0 &&
				spawnPos.x < levelSize-1 &&
				spawnPos.y >= 0 &&
				spawnPos.y < levelSize -1)
			{
				if (!ContainsBlock(spawnPos))
					SpawnBlock(spawnPos);
			}
			else
			{
				if (!ContainsBlock(spawnPos))
					SpawnEndBlock(spawnPos);
			}
		}
		
	}
	
	private void SpawnBlock(Vector2I spawnPos)
	{
		int spawnBlockID = (int)levelData[spawnPos.x, spawnPos.y];
		
		GameObject go = (GameObject)GameObject.Instantiate(levelBlockPrefabs[spawnBlockID]);
		
		Vector2 wolrdSpawnPos = LevelToWorldPos(spawnPos);
		
		go.transform.position = new Vector3(wolrdSpawnPos.x, 0f, wolrdSpawnPos.y);
		go.transform.localEulerAngles = new Vector3(0f, 90f * Random.Range(0, 4), 0f);
		
		go.transform.parent = transform;
		levelBlockList.Add(new LevelBlock(spawnPos.x, spawnPos.y, (byte)spawnBlockID, go));
	
	}
	
	private void SpawnEndBlock(Vector2I spawnPos)
	{
		GameObject go = (GameObject)GameObject.Instantiate(levelEndPrefab);
		
		Vector2 wolrdSpawnPos = LevelToWorldPos(spawnPos);
		
		go.transform.position = new Vector3(wolrdSpawnPos.x, 0f, wolrdSpawnPos.y);
		go.transform.localEulerAngles = new Vector3(0f, 90f * Random.Range(0, 4), 0f);
		
		go.transform.parent = transform;
		levelBlockList.Add(new LevelBlock(spawnPos.x, spawnPos.y, 255, go));
	}
	
	private bool ContainsBlock(Vector2I blockPos)
	{
		foreach (LevelBlock levelBlock in levelBlockList)
		{
			if(levelBlock.xPos == blockPos.x && levelBlock.yPos == blockPos.y)
				return true;
		}
		
		return false;
	}
	
	private Vector2I WorldToLevelPos(Vector2 worldPos)
	{
		return new Vector2I(Mathf.RoundToInt(worldPos.x / 32f), Mathf.RoundToInt(worldPos.y / 32f));
	}
	
	private Vector2 LevelToWorldPos(Vector2I levelPos)
	{
		return new Vector2((float)(levelPos.x * 32), (float)(levelPos.y * 32));
	}
}
