using System;
using UnityEngine;

 namespace RedStone
{
	public static class ColorConfig 
	{
		/// <summary>
		/// None
		public static Color None { get { return TableManager.instance.GetData<TableTextColor>(1).value; } }
		/// <summary>
		/// 白1
		public static Color white2 { get { return TableManager.instance.GetData<TableTextColor>(21).value; } }
		/// <summary>
		/// 白2
		public static Color white3 { get { return TableManager.instance.GetData<TableTextColor>(22).value; } }
		/// <summary>
		/// 白3
		public static Color white4 { get { return TableManager.instance.GetData<TableTextColor>(23).value; } }
		/// <summary>
		/// 白4
		public static Color white5 { get { return TableManager.instance.GetData<TableTextColor>(24).value; } }
		/// <summary>
		/// 黄1
		public static Color yellow1 { get { return TableManager.instance.GetData<TableTextColor>(31).value; } }
		/// <summary>
		/// 黄2
		public static Color yellow2 { get { return TableManager.instance.GetData<TableTextColor>(32).value; } }
		/// <summary>
		/// 黄3
		public static Color yellow3 { get { return TableManager.instance.GetData<TableTextColor>(33).value; } }
		/// <summary>
		/// 黄4
		public static Color yellow4 { get { return TableManager.instance.GetData<TableTextColor>(34).value; } }
		/// <summary>
		/// 黄5
		public static Color yellow5 { get { return TableManager.instance.GetData<TableTextColor>(35).value; } }
		/// <summary>
		/// 黄6
		public static Color yellow6 { get { return TableManager.instance.GetData<TableTextColor>(36).value; } }
		/// <summary>
		/// 白1
		public static Color white1 { get { return TableManager.instance.GetData<TableTextColor>(37).value; } }
		/// <summary>
		/// 蓝1
		public static Color blue1 { get { return TableManager.instance.GetData<TableTextColor>(41).value; } }
		/// <summary>
		/// 蓝2
		public static Color blue2 { get { return TableManager.instance.GetData<TableTextColor>(42).value; } }
		/// <summary>
		/// 蓝3
		public static Color blue3 { get { return TableManager.instance.GetData<TableTextColor>(43).value; } }
		/// <summary>
		/// 蓝4
		public static Color blue4 { get { return TableManager.instance.GetData<TableTextColor>(44).value; } }
		/// <summary>
		/// 蓝5
		public static Color blue5 { get { return TableManager.instance.GetData<TableTextColor>(45).value; } }
		/// <summary>
		/// 蓝6
		public static Color blue6 { get { return TableManager.instance.GetData<TableTextColor>(46).value; } }
		/// <summary>
		/// 红1
		public static Color red1 { get { return TableManager.instance.GetData<TableTextColor>(51).value; } }
		/// <summary>
		/// 红2
		public static Color red2 { get { return TableManager.instance.GetData<TableTextColor>(52).value; } }
		/// <summary>
		/// 红3
		public static Color red3 { get { return TableManager.instance.GetData<TableTextColor>(53).value; } }
		/// <summary>
		/// 红4
		public static Color red4 { get { return TableManager.instance.GetData<TableTextColor>(54).value; } }
		/// <summary>
		/// 红5
		public static Color red5 { get { return TableManager.instance.GetData<TableTextColor>(55).value; } }
		/// <summary>
		/// 红6
		public static Color red6 { get { return TableManager.instance.GetData<TableTextColor>(56).value; } }
		/// <summary>
		/// 红7
		public static Color red7 { get { return TableManager.instance.GetData<TableTextColor>(57).value; } }
		/// <summary>
		/// 绿1
		public static Color green1 { get { return TableManager.instance.GetData<TableTextColor>(61).value; } }
		/// <summary>
		/// 绿2
		public static Color green2 { get { return TableManager.instance.GetData<TableTextColor>(62).value; } }
		/// <summary>
		/// 绿3
		public static Color green3 { get { return TableManager.instance.GetData<TableTextColor>(63).value; } }


		public static string GetColorCode(string name) 
		{
			switch(name){
				 case "None" : return "#FFFFFF";
				 case "white2" : return "#e9fdff";
				 case "white3" : return "#bbc7c9";
				 case "white4" : return "#929b9d";
				 case "white5" : return "#6f7274";
				 case "yellow1" : return "#f6e2c1";
				 case "yellow2" : return "#fde384";
				 case "yellow3" : return "#f2cd0d";
				 case "yellow4" : return "#ffba00";
				 case "yellow5" : return "#ffa21d";
				 case "yellow6" : return "#90611d";
				 case "white1" : return "#fffaed";
				 case "blue1" : return "#bce2e7";
				 case "blue2" : return "#94b0bd";
				 case "blue3" : return "#93dfff";
				 case "blue4" : return "#6fc4e6";
				 case "blue5" : return "#0ddaff";
				 case "blue6" : return "#4f7ea2";
				 case "red1" : return "#ffedec";
				 case "red2" : return "#f0cdc9";
				 case "red3" : return "#f37474";
				 case "red4" : return "#fb4925";
				 case "red5" : return "#c63647";
				 case "red6" : return "#8f473e";
				 case "red7" : return "#9f7676";
				 case "green1" : return "#0ef9f1";
				 case "green2" : return "#2aacaa";
				 case "green3" : return "#327974";
			}
			 return "#ffffff";
		}
	}
}
