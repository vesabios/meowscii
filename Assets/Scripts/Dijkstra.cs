using UnityEngine;
using System.Collections;


public class Dijkstra : ScriptableObject {


	public static int OMIT_VALUE = 10000;
	public static int PASS_VALUE = 1255;

	[HideInInspector] public int [,] graph;
	[HideInInspector] public static int [,] baseGraph;


	private bool m_IsDone = false;
	private object m_Handle = new object();
	private System.Threading.Thread m_Thread = null;


	private int queryCount = 0;

	private bool flip = false;

	public static Vector2 dims = Landscape.dims;

	public Vector2 origin;
	public Vector2 target;


	//------------------------------------------------------------------------
	public Dijkstra() {

		InitBaseGraph ();

		dims = Landscape.dims;

		graph = new int [(int)dims.x, (int)dims.y];


	}

	static void InitBaseGraph() {
		if (baseGraph==null) baseGraph = new int [(int)dims.x, (int)dims.y];

	}

	public bool IsDone {

		get {
			bool tmp;
			lock (m_Handle) {
				tmp = m_IsDone;
			}

			return tmp;
		}

		set {
			lock (m_Handle) {
				m_IsDone = value;
			}
		}
	}



	//------------------------------------------------------------------------

	private void RunIterate() {
		ThreadIterate();
		IsDone = true;

	}


	public void Iterate() {

		Engine.instance.StartCoroutine(IIterate());
	}

	public IEnumerator IIterate() {

		IsDone = false;
		m_Thread = new System.Threading.Thread(RunIterate);
		m_Thread.Start();		

		while (!IsDone) {
			yield return null;
		}

	}

	public int IntAbs(int x) {
		return (x > 0) ? x : -x;
	}

	public int IntMin(int a, int b) {
		return (a < b) ? a : b;
	}

	 

	public void ThreadIterate() {

		bool changed = false;

		int lvn = 0;
		int v = 0;

		int iter = 0;


		int y0 = (int)target.y -1;
		int y1 = (int)target.y +1;
		int x0 = (int)target.x -1;
		int x1 = (int)target.x +1;

		int minx = 256;
		int miny = 256;
		int maxx = -1;
		int maxy = -1;

		while (iter<64) {

			changed = false;
			iter++;

			x0 = Mathf.Max (1, x0);
			x1 = Mathf.Min ((int)dims.x-1, x1);

			y0 = Mathf.Max (1, y0);
			y1 = Mathf.Min ((int)dims.y-1, y1);

			for (int y = y0; y<y1; y++) {
				for (int x = x0; x<x1; x++) {

					v =  graph[x,y];

					if ( v < OMIT_VALUE) {

						lvn = IntMin(v, graph[x-1,y]);
						lvn = IntMin(lvn, graph[x,y-1]);
						lvn = IntMin(lvn, graph[x+1,y]);
						lvn = IntMin(lvn, graph[x,y+1]);


						if ((v-lvn)>=2) {
							graph[x,y] = lvn+1;


							minx = Mathf.Min (x - 2, minx);
							miny = Mathf.Min (y - 2, miny);
							maxx = Mathf.Max (x + 2, maxx);
							maxy = Mathf.Max (y + 2, maxy);

							if (new Vector2 (x, y) == origin) {
								return;
							}

							changed = true;
						}
					}

				}
			}

			if (!changed) {
				return;
			}

			x0 = minx;
			x1 = maxx;
			y0 = miny;
			y1 = maxy;

		}
	}

	public void DecreaseLiklihoodOfLocation(Vector2 l) { // add one to this cell to reduce its chance of being chosen

		int x = (int)l.x;
		int y = (int)l.y;


		int v =  graph[x, y];
		if ( v < OMIT_VALUE) {    	
			graph[x, y] += 1;
		}
	}


	//------------------------------------------------------------------------
	private void RunInit() {
		ThreadInit();
		IsDone = true;

	}

	public void Init() {
		Engine.instance.StartCoroutine(IInit());
	}

	public IEnumerator IInit() {

		IsDone = false;

		m_Thread = new System.Threading.Thread(RunInit);
		m_Thread.Start();		

		while (!IsDone) {
			yield return null;
		}


	}

	public static void PrepareBaseGraph() {
		InitBaseGraph ();

		for (int y = 0; y<dims.y; y++) {
			for (int x = 0; x<dims.x; x++) {
				baseGraph[x,y] = Game.CanActorOccupyLocation (Engine.player, new Vector2 (x, y)) ? 128 : OMIT_VALUE;
			}
		}
	}

	public void ThreadInit() {

		for (int y = 0; y<dims.y; y++) {
			for (int x = 0; x<dims.x; x++) {
				graph[x,y] = baseGraph [x, y];
			}
		}

	}


	//------------------------------------------------------------------------

	public IEnumerator IRetreat() {

		for (int y = 0; y<dims.y; y++) {
			for (int x = 0; x<dims.x; x++) {
				if ((y<2)||(y>dims.y-2)||(x<2)||(x>dims.x-2)) {
					graph[x,y] = OMIT_VALUE;
				} else {
					int v = graph[x,y];
					if ( v < OMIT_VALUE-1) {
						graph[x,y] = ((v*-6)/5);
					}
				}
			}
		}

		yield return null;

	}


	//------------------------------------------------------------------------
	public void UpdateTiles() {
		for (int y = 1; y<dims.y-1; y++) {
			for (int x = 1; x<dims.x-1; x++) {
				if (graph[x,y] < OMIT_VALUE) {
					//Tile.SetMovementValueForTile(x, y, graph[x,y]);
				}
			}
		}
	}

	//------------------------------------------------------------------------
	public void SetObstacle(Vector3 l) {

		int x = (int)l.x;
		int y = (int)l.y;

		graph[x, y] = Dijkstra.OMIT_VALUE;
	}






	//------------------------------------------------------------------------
	public void SetTarget(Vector3 l) {

		target = (Vector2)l;

		int x = (int)l.x;
		int y = (int)l.y;

		graph[x, y] = 0;
	}


	public bool IsTraversable(Vector3 l) {

		int x = (int)l.x;
		int y = (int)l.y;


		return graph[x, y] < OMIT_VALUE;

	}



	//------------------------------------------------------------------------
	public void SetOrigin(Vector3 l) {

		origin = (Vector2)l;

	}



	public int GetMoveValue(Vector3 l) {

		int x = (int)l.x;
		int y = (int)l.y;

		return graph[x, y];
	}

	//------------------------------------------------------------------------
	public Vector2 GetMoveVector(Vector3 l) {

		int x = (int)l.x;
		int y = (int)l.y;

		int v = graph[x,y];

		int lvn = OMIT_VALUE-1;
		Vector2 moveVector = Vector2.zero;

		queryCount++;
		if (queryCount>3) {
			queryCount = 0;

			flip = !flip;
		}


		if (flip) {

			if (lvn > graph[x+1,y]) {
				moveVector = new Vector2(1,0);
				lvn = graph[x+1,y];
			}	

			if (lvn > graph[x-1,y]) {
				moveVector = new Vector2(-1,0);
				lvn = graph[x-1,y];
			}	

			if (lvn > graph[x,y+1]) {
				moveVector = new Vector2(0,1);
				lvn = graph[x,y+1];
			}

			if (lvn > graph[x,y-1]) {
				moveVector = new Vector2(0,-1);
				lvn = graph[x,y-1];
			}   

		} else {

			if (lvn > graph[x,y+1]) {
				moveVector = new Vector2(0,1);
				lvn = graph[x,y+1];
			}

			if (lvn > graph[x,y-1]) {
				moveVector = new Vector2(0,-1);
				lvn = graph[x,y-1];
			}   

			if (lvn > graph[x+1,y]) {
				moveVector = new Vector2(1,0);
				lvn = graph[x+1,y];
			}	

			if (lvn > graph[x-1,y]) {
				moveVector = new Vector2(-1,0);
				lvn = graph[x-1,y];
			}			    

		}



		return moveVector;

	}

	//------------------------------------------------------------------------
	public Vector2 GetFlankVector(Vector2 l) {
		int x = (int)l.x;
		int y = (int)l.y;

		int v = graph[x, y];

		Vector2 moveVector = Vector2.zero;

		queryCount++;
		if (queryCount > 3) {
			queryCount = 0;
			flip = !flip;
		}


		if (flip) {

			if (v == graph[x + 1, y]) {
				moveVector = new Vector2(1, 0);
			} else if (v == graph[x - 1, y]) {
				moveVector = new Vector2(-1, 0);
			} else if (v == graph[x, y + 1]) {
				moveVector = new Vector2(0, 1);
			} else if (v == graph[x, y - 1]) {
				moveVector = new Vector2(0, -1);
			}

		} else {

			if (v == graph[x, y + 1]) {
				moveVector = new Vector2(0, 1);
			} else if (v == graph[x, y - 1]) {
				moveVector = new Vector2(0, -1);
			} else if (v == graph[x + 1, y]) {
				moveVector = new Vector2(1, 0);
			} else if (v == graph[x - 1, y]) {
				moveVector = new Vector2(-1, 0);
			}

		}

		return moveVector;

	}


	//------------------------------------------------------------------------
	public int GetActiveArea() {
		int area = 0;
		for (int y = 1; y<dims.y-1; y++) {
			for (int x = 1; x<dims.x-1; x++) {
				if (graph[x,y] < PASS_VALUE-1) {
					area++;
				}
			}
		}

		return area;

	}


}