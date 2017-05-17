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

	public void OnEnable()
	{
		grid = (Grid)target;
		lastPlaced = new Vector3 (0, 0, 2000);
		lastNamed = "this is my soul leaving my body and going straight to hell im coming 2pac";
		SceneView.onSceneGUIDelegate = GridUpdate;

		tiles = GameObject.Find ("Tiles"); //Sets up a nice little folder for everything to go into :)
		if (tiles == null) {
			tiles = new GameObject ();
			tiles.name = "Tiles";
			tiles.transform.position = Vector3.zero;
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
		GUILayout.BeginHorizontal (); //This allows the user to change grid width
		GUILayout.Label (" Grid Width ");
		grid.width = EditorGUILayout.FloatField (grid.width, GUILayout.Width (50));
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal (); //Allows user to change grid height
		GUILayout.Label (" Grid Height ");
		grid.height = EditorGUILayout.FloatField (grid.height, GUILayout.Width (50));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal ();
		GUILayout.Label (" Tile "); //Allows user to select an object from a list
		currentObj = (GameObject) EditorGUILayout.ObjectField(currentObj, typeof(GameObject), false);
		GUILayout.EndHorizontal ();

		SceneView.RepaintAll();

	}

	void GridUpdate (SceneView sceneview)
	{
		Event e = Event.current;

		Vector3 mousePos = getMousePoint (e);

		Vector3 aligned = new Vector3 (
			Mathf.Floor (mousePos.x / grid.width) * grid.width + grid.width / 2.0f,
			Mathf.Floor (mousePos.y / grid.height) * grid.height + grid.height / 2.0f);
		//Aligns the tiles to the centre of the closest grid tile, taking width and height

		if (e.isKey && e.character == (char)'a') {

			if (currentObj != null) {
			
				replace (aligned);

			}
		} else if (e.isKey && e.character == (char)'d') 
		{
			
			foreach (Transform tile in tiles.transform) 
			{
				if (aligned == tile.transform.position) 
				{
					Undo.DestroyObjectImmediate (tile.gameObject);
				}
			}
				
		} else if (e.isKey && e.character == (char)'r') {
			
			if (Selection.activeObject) 
			{
				Undo.IncrementCurrentGroup ();


				GameObject obj = Selection.activeGameObject;
				Undo.RecordObject (obj.transform, "Rotate " + obj.name);
				obj.transform.Rotate (new Vector3 (0, 0, 90));


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
