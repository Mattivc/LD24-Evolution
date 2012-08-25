using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
	public Transform playerTransform;
	
	public List<GameObject> levelBlockPrefabs;
	
	private static int levelSize = 8;
	
	private byte[,] levelData = new byte[levelSize,levelSize];
	private List<LevelBlock> levelBlockList = new List<LevelBlock>();
	
	private struct LevelBlock
	{
		public LevelBlock(int x, int y, GameObject block)
		{
			xPos = x;
			yPos = y;
			go = block;
		}
		
		public int xPos;
		public int yPos;
		
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
		
		if (!ContainsBlock(playerLevelPos))
		{
			SpawnBlock(playerLevelPos);
		}
		
		if (playerLevelPos.x > 0) // -X
		{
			Vector2I newPos = new Vector2I(playerLevelPos.x-1, playerLevelPos.y);
			
			if (!ContainsBlock(newPos))
				SpawnBlock(newPos);
		}
		
		if (playerLevelPos.x < levelSize) // +X
		{
			Vector2I newPos = new Vector2I(playerLevelPos.x+1, playerLevelPos.y);
			
			if (!ContainsBlock(newPos))
				SpawnBlock(newPos);
		}
		
		if (playerLevelPos.y > 0) // -Y
		{
			Vector2I newPos = new Vector2I(playerLevelPos.x, playerLevelPos.y-1);
			
			if (!ContainsBlock(newPos))
				SpawnBlock(newPos);
		}
		
		if (playerLevelPos.y < levelSize) // +Y
		{
			Vector2I newPos = new Vector2I(playerLevelPos.x, playerLevelPos.y+1);
			
			if (!ContainsBlock(newPos))
				SpawnBlock(newPos);
		}
	}
	
	private void SpawnBlock(Vector2I spawnPos)
	{
		int spawnBlockID = (int)levelData[spawnPos.x, spawnPos.y];
		GameObject go = (GameObject)GameObject.Instantiate(levelBlockPrefabs[spawnBlockID]);
		
		Vector2 wolrdSpawnPos = LevelToWorldPos(spawnPos);
		
		go.transform.position = new Vector3(wolrdSpawnPos.x, 0f, wolrdSpawnPos.y);
		go.transform.parent = transform;
		levelBlockList.Add(new LevelBlock(spawnPos.x, spawnPos.y, go));
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
		return new Vector2I(Mathf.RoundToInt(worldPos.x / 16f), Mathf.RoundToInt(worldPos.y / 16f));
	}
	
	private Vector2 LevelToWorldPos(Vector2I levelPos)
	{
		return new Vector2((float)(levelPos.x * 16), (float)(levelPos.y * 16));
	}
}
