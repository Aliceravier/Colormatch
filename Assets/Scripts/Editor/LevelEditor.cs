using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

/*
 * Shit to do:
 * Have some kind of tile replacement thing going ish
 * Need to look at stored layer
 * If anything is there that is on the same layer as the thing being placed
 * replace it with the thing currently there
 * else
 * layer it on top
 * 
 * Aside from that we good
 * Maybe having an editor variant which pulls random tile replacements would be good too
 * Thus need to have some kind of array thing
 * this will be tricky
 * 
 * 
 * */
[CustomEditor (typeof(Grid))]
public class LevelEditor : Editor 
{
	Grid grid;
	static GameObject currentObj;
	static Vector3 lastPlaced;
	static string lastNamed;  
	static GameObject tiles;
	static string folderName = "Tiles";
	static string objtag = "Room";
	//static float storedgridh;
	//static float storedgridw;

	public void OnEnable()
	{
		

		grid = (Grid)target;

		//storedgridh = grid.height;
		//storedgridw = grid.width;
		lastPlaced = new Vector3 (0, 0, 2000);
		lastNamed = "this is my soul leaving my body and going straight to hell im coming 2pac";

		SceneView.onSceneGUIDelegate = GridUpdate;
		tiles = GameObject.Find (folderName); //Sets up a nice little folder for everything to go into :)
		if (tiles == null) {
			tiles = new GameObject ();
			tiles.name = folderName;
			tiles.transform.position = Vector3.zero;
			//only relevant if placing a tile in a room

			if (currentObj != null && !currentObj.CompareTag(objtag)) {
				tiles.AddComponent<RoomManager> ();
				tiles.tag = "Room";
			}
		}
	} 


	void PlaceThing(Vector3 aligned)
	{
		
		GameObject obj;
		Undo.IncrementCurrentGroup ();

		obj = (GameObject)PrefabUtility.InstantiatePrefab (currentObj);
		obj.transform.position = aligned;
		obj.transform.SetParent (tiles.transform, true);

		lastPlaced = aligned;
		lastNamed = obj.name;

		Undo.RegisterCreatedObjectUndo (obj, "Create " + obj.name);

	}

	void replace(Vector3 aligned)
	{
		string currentLayer = currentObj.GetComponent<SpriteRenderer> ().sortingLayerName; //Get layer of current gameObject being placed
		foreach (Transform tile in tiles.transform) 
		{
			if (aligned == tile.transform.position) 
			{
				string tileLayer = tile.gameObject.GetComponent<SpriteRenderer> ().sortingLayerName; //Get layer of current tile being iterated through
				if (tileLayer == currentLayer) 
				{
					Undo.DestroyObjectImmediate (tile.gameObject);

				}
			}
		}

		PlaceThing (aligned);

		

	}


	public override void OnInspectorGUI ()
	{
		/*
		GUILayout.BeginHorizontal (); //This allows the user to change grid width
		GUILayout.Label (" Grid Width ");
		storedgridw = EditorGUILayout.FloatField (storedgridw, GUILayout.Width (50));
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal (); //Allows user to change grid height
		GUILayout.Label (" Grid Height ");
		storedgridh= EditorGUILayout.FloatField (storedgridh, GUILayout.Width (50));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal ();
		GUILayout.Label (" Type of tile being placed "); //Allows user to select if tile is room or standard tile
		toPlace = (Thing) EditorGUILayout.EnumPopup(toPlace, GUILayout.Width(50));
		GUILayout.EndHorizontal ();*/

		GUILayout.BeginHorizontal ();
		GUILayout.Label (" Tile "); //Allows user to select an object from a list
		currentObj = (GameObject) EditorGUILayout.ObjectField(currentObj, typeof(GameObject), false);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label (" Folder Name "); //Allows user to set which gameObject they want to put things into
		folderName = EditorGUILayout.TextField(folderName, GUILayout.Width(100));
		GUILayout.EndHorizontal ();

		SceneView.RepaintAll();

	}

	void GridUpdate (SceneView sceneview)
	{
		tiles = GameObject.Find (folderName);
		Event e = Event.current;

		//sets size to be big if object is room. Sets size to be tilesize if object is not
		if (currentObj != null) {
			if (currentObj.CompareTag (objtag)) {
				grid.width = currentObj.GetComponent<RoomManager> ().getSize ().x;
				grid.height = currentObj.GetComponent<RoomManager> ().getSize ().y;
			} else {
				grid.width = currentObj.GetComponent<SpriteRenderer> ().bounds.size.x;
				grid.height = currentObj.GetComponent<SpriteRenderer>().bounds.size.y;
			}
		}

		Vector3 mousePos = getMousePoint (e);

		Vector3 aligned = new Vector3 (
			Mathf.Floor (mousePos.x / grid.width) * grid.width + grid.width / 2.0f,
			Mathf.Floor (mousePos.y / grid.height) * grid.height + grid.height / 2.0f);

		Bounds alignedBounds = new Bounds (aligned, new Vector3 (grid.width, grid.height));
		//Aligns the tiles to the centre of the closest grid tile, taking width and height

		if (e.isKey && e.character == (char)'a') { //APPENDING/REPLACING

			if (currentObj != null) {
				if (!currentObj.CompareTag (objtag))
					replace (aligned);
				else
					PlaceThing (aligned);
			}

		} else if (e.isKey && e.character == (char)'d') //DELETING
		{
			
			foreach (Transform tile in tiles.transform) 
			{
				if (alignedBounds.Contains(tile.transform.position)) 
				{
					Undo.DestroyObjectImmediate (tile.gameObject);
				}
			}
				
		} else if (e.isKey && e.character == (char)'r') { //ROTATING
			
			foreach (Transform tile in tiles.transform)
			{
				if (alignedBounds.Contains(tile.transform.position)) {
					Undo.IncrementCurrentGroup ();


					GameObject obj = tile.gameObject;
					Undo.RecordObject (obj.transform, "Rotate " + obj.name);
					obj.transform.Rotate (new Vector3 (0, 0, 90));
				}


			} 
		}



	}

	Vector3 getMousePoint(Event e)
	{
		/*This rad code uses a little known technique known as RAYCASTING to CAST
		a FUCKING RAY from where the mouse is (looking specifically at it within the camera bounds)
		then it returns that ray */
		Ray r;

		r = Camera.current.ScreenPointToRay (new Vector3 (
			e.mousePosition.x,
			-e.mousePosition.y + Camera.current.pixelHeight));

		return r.origin;
	}
}
