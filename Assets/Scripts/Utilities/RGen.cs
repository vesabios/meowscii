using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class RGen {


	//this is the weight of this RGen when it's in a table.
	//when it's not in a table or is a top-level RGen, this has no meaning.
	public double weight = 1.0f; 

	public int id = 0; // general purpose identifier

	//if this is not a leaf, it will have a table of weighted branches.
	public List<RGen> table = new List<RGen>();


	//------------------------------------------------------------------------
	public int Count {
		get {
			return table.Count;
		}
	}

	//------------------------------------------------------------------------
	public RGen(double weight = 1.0) {
		this.weight = weight;
	}

	//------------------------------------------------------------------------
	public double TotalWeight() {
		if ((table==null) || (table.Count==0) ) {
			return weight;
		}
		double total = 0;
		foreach (RGen rg in table) {
			total += rg.TotalWeight();
		}
		return total;
	}

	//------------------------------------------------------------------------
	public void ClearTable() {
		table.Clear();
	}

	//------------------------------------------------------------------------
	public void AddToTable(RGen rg) {
		table.Add(rg);
	}

	//------------------------------------------------------------------------
	public RGen Result() {

		if ((table==null) || (table.Count==0) ) {
            //Debug.Log("RGen::Result "+this + " " + weight);
            

            //Console.WriteLine(this);

            return this;
		}
		return GetResult();
	}

	//------------------------------------------------------------------------
	public RGen GetResult() {
        //Debug.Log("RGen::GetResult "+this);
		double totalWeight = 0.0f;
		for (int i=0; i<table.Count; i++) {
            //Debug.Log("adding table: " + i + ", weight: " + table[i].weight);
			totalWeight += table[i].weight;	
		}

		double roll = Random.value * totalWeight;

		double tally = 0;
		for (int i=0; i<table.Count; i++) {
			tally += table[i].weight;
			if (tally>=roll) {
				//Debug.Log(tally+">"+roll+"   :"+table.Count+"/"+totalWeight);
				return table[i].Result();
			}
		}

		return null;
	}


	public static void PrintTable(RGen a, RGen b) {

		for (int x=0; x<a.table.Count; x++) {
			for (int y=0; y<b.table.Count; y++) {
				List<RGen> t = a.table[x].table.Intersect(b.table[y].table).ToList();

				double weight = 0;

				foreach (RGen rg in t) {
					weight += rg.TotalWeight();
				}

				weight *= a.table[x].weight * b.table[y].weight;

				int w = (int)Mathf.Round((float)weight); 

				if (w==0) {
					//Console.Write("  . ");
 
				} else {
					//Console.Write(w.ToString().PadLeft(3)+" ");
				}
			}

			//Console.Write("\r\n");
		}

		//Console.WriteLine(" ");


	}


	//------------------------------------------------------------------------
	public static Tuple3<int, int, RGen> Combine(RGen a, RGen b) {

		double[,] temp = new double[a.table.Count, b.table.Count];

		double totalWeight = 0;
		for (int x=0; x<a.table.Count; x++) {
			for (int y=0; y<b.table.Count; y++) {


				List<RGen> t = a.table[x].table.Intersect(b.table[y].table).ToList();

				double weight = 0;

				foreach (RGen rg in t) {
					weight += rg.TotalWeight();
				}

				weight *= a.table[x].weight * b.table[y].weight;
				

				temp[x,y] = weight;
				totalWeight += weight;
			}
		}


		double roll = Random.value * totalWeight;

		double tally = 0;
		for (int x=0; x<a.table.Count; x++) {
			for (int y=0; y<b.table.Count; y++) {

				tally += temp[x,y];

				if (tally>=roll) {

					RGen union = new RGen(temp[x,y]);

					union.table = a.table[x].table.Intersect(b.table[y].table).ToList();
					//Console.WriteLine("total union: "+union.table.Count);	 

					return new Tuple3<int, int, RGen>(a.table[x].id, b.table[y].id, union);
				}
			}
		}

		return null;

	}

	//------------------------------------------------------------------------
	public List<RGen> GenerateResults(float mean) {

		//Console.WriteLine("gen results for: "+mean);

		int q = GetQuantity(mean);

		List<RGen> results = new List<RGen>();

		for (int i=0; i<q; i++) {
			RGen rg = GetResult();
			if (rg!=null) results.Add(rg);
		}

		return results;

	}	

	//------------------------------------------------------------------------
	private int GetQuantity(float mean) {
		return (int)mean;
	}


	//------------------------------------------------------------------------
	public override string ToString() {
		return "RGen";

	}

}
