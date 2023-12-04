using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapidFinalProject
{
    //main map
    //this might not even need to be a class
    public class Map
    {
    }

    //sections of the map (to display on screen)
    //let's say 3 different sections, so need 3 different map images
    public class MapSection
    {
        public string Name;
        public Bitmap Image;
        public List<Area> AreaList;

        public MapSection(string name, Bitmap image) 
        {
            Name = name;
            Image = image;
            AreaList = new List<Area>();
        }
    }

    //areas (where the player encounters enemies and finds items)
    public class Area
    {
        public string AreaName;
        public Bitmap Image = new Bitmap(Properties.Resources.placeholder);
        public bool unlocked = false;
        public bool completed { get; set; } = false;

        //Button info
        public RoundButton Button;
        public int X;
        public int Y;

        public List<Area> AccessibleAreas = new List<Area>();
        public List<Enemy> Enemies = new List<Enemy>();

        public Area(string name, RoundButton button, int x, int y)
        {
            AreaName = name;
            Button = button;
            X = x;
            Y = y;
        }
    }

    //empty enemy class just so the code works
    public class Enemy
    {

    }
}
