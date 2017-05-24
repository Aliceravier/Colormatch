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
//	static Vector3 lastPlaced;
//	static string lastNamed;  
	static GameObject tiles;
	static string folderName = "Tiles";
	static string objtag = "Room";
	bool isComplete;
	//static float storedgridh;
	//static float storedgridw;

	public void OnEnable()
	{

		grid = (Grid)target;

		//storedgridh = grid.height;
		//storedgridw = grid.width;
//		lastPlaced = new Vector3 (0, 0, 2000);
//		lastNamed = "this is my soul leaving my body and going straight to hell im coming 2pac";

		SceneView.onSceneGUIDelegate = GridUpdate;
		tiles = GameObject.Find (folderName); //Sets up a nice little folder for everything to go into :)
		if (tiles == null) {
			tiles = new GameObject ();
			tiles.name = folderName;
			tiles.transform.position = Vector3.zero;
			//only relevant if placing a tile in a room
		}
	} 


	void PlaceThing(Vector3 aligned)
	{
		
		GameObject obj;
		Undo.IncrementCurrentGroup ();

		obj = (GameObject)PrefabUtility.InstantiatePrefab (currentObj);
		obj.transform.position = aligned;
		
		obj.transform.SetParent (tiles.transform, true);

//		lastPlaced = aligned;
//		lastNamed = obj.name;

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

		GUILayout.BeginHorizontal (); //Allows user to set which gameObject they want to put things into
		isComplete = GUILayout.Button(" Complete Room ");
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
				grid.width = ExtendedBehaviour.getTileSize (currentObj).x;
				grid.height = ExtendedBehaviour.getTileSize (currentObj).y;
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

		if (isComplete)
			completeRoom ();



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

	void completeRoom(){
		tiles.AddComponent<RoomManager> ();
		tiles.tag = "Room";

		RoomManager rm = tiles.GetComponent<RoomManager> ();
		Vector2 dist = rm.diffBetweenCentre(rm.getClosestCentreTile ());

		foreach (Transform tile in tiles.transform) {
			tile.position += (Vector3) dist;
		}
		placeOverlays ();	
		isComplete = false;

	}

	void placeOverlays(){
		//loadan shit
		Object overlay, posOverlay; 
		string overlayS = "Overlay.prefab", posOverlayS = "PositionOverlay.prefab";
		overlay = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+overlayS, typeof(GameObject));
		posOverlay = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/"+ posOverlayS, typeof(GameObject));
	
		//instantiate objects
		GameObject overlayObj = (GameObject)PrefabUtility.InstantiatePrefab (overlay);
		GameObject posOverlayObj = (GameObject)PrefabUtility.InstantiatePrefab (posOverlay);

		//get sizes
		Vector2 size = ExtendedBehaviour.getTileSize (overlayObj);
		Vector2 roomSize = tiles.GetComponent<RoomManager> ().getSize ();
		Vector2 sizeOfOneTile = ExtendedBehaviour.getTileSize (tiles.transform.GetChild (1).gameObject);
		float scalar = 1;

		Debug.Log (roomSize);
		Debug.Log (size);
		Debug.Log (sizeOfOneTile);


		//% magic to calculate true size
		Vector3 trueSize = new Vector3((roomSize.x/size.x),
			(roomSize.y/size.y),
			1);
		Debug.Log (trueSize);
		//place them in the correct place

		Vector3 dist = tiles.GetComponent<RoomManager>().getClosestCentreTile().position;
		overlayObj.transform.position = dist;
		posOverlayObj.transform.position = dist;

		posOverlayObj.transform.position += new Vector3 (0, -sizeOfOneTile.y);
		overlayObj.transform.position += new Vector3 (0, -sizeOfOneTile.y);

		overlayObj.transform.SetParent (tiles.transform, true);
		posOverlayObj.transform.SetParent (tiles.transform, true);



		overlayObj.transform.localScale = trueSize;
		posOverlayObj.transform.localScale = trueSize;






	}
		
}
