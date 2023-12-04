using RapidFinalProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RapidFinalProject
{
    public partial class FormMain : Form
    {
        MapSection currentMap;

        //default colours
        static Color areaColor = Color.Gold;
        static Color areaHoverColor = Color.Yellow;
        static Color areaDoneColor = Color.Green;
        static Color areaDoneHoverColor = Color.Lime;


        //method to position buttons (should only be called onces per map section)
        //had to hard code the button locations because setting them on the designer wasn't working for me
        public void PositionButtons(MapSection map)
        {
            foreach (Area area in map.AreaList)
            {
                area.Button.Location = new Point(area.X, area.Y);
                area.Button.BackColor = areaColor;
            }
        }

        //method to load map sections
        public void LoadSection(MapSection map)
        {
            currentMap = map;
            lbl_AreaName.Text = map.Name;

            this.BackgroundImage = map.Image;

            //unhide available area buttons and put them in the right spot
            foreach(Area area in map.AreaList)
            {
                if(area.unlocked == true)
                {
                    area.Button.Visible = true;
                }
            }
        }

        public void LoadArea(Area area)
        {
            lbl_AreaName.Text = area.AreaName;

            this.BackgroundImage = area.Image;

            //this line will probably be moved later
            btn_Next.Visible = true;
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(660, 700);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //create map objects
            MapSection map1 = new MapSection("Map 1", new Bitmap(Properties.Resources.map1));
            //MapSection map2 = new MapSection("Map 2", new Bitmap(Properties.Resources.placeholder));
            //MapSection map3 = new MapSection("Map 3", new Bitmap(Properties.Resources.placeholder));

            //Dynamically adds all the areas to the map's list
            foreach (var con in this.Controls)
            {
                if(con.GetType().ToString() == "RapidFinalProject.RoundButton")
                {
                    RoundButton rb = (RoundButton)con;
                    map1.AreaList.Add(new Area(rb.Text, rb, rb.Location.X, rb.Location.Y));
                }
            }

            //populates the accesible list via the tag property
            List<string> parents;
            foreach (Area a in map1.AreaList)
            {
                if (a.Button.Tag == null) continue;
                parents = new List<string>();
                parents.AddRange(a.Button.Tag.ToString().Split('=')[1].Split('-'));
                parents.ForEach(p =>
                    map1.AreaList.FindAll(area => area.Button.Name.Split('_').Last() == p)
                        .ForEach(x => x.AccessibleAreas.Add(a)));
            }

            //sets the initial areas to unlocked
            map1.AreaList.Where(x => x.Button.Tag.ToString().Split('=')[1] =="null")
                .ToList().ForEach(x => x.unlocked = true);

            PositionButtons(map1);
            LoadSection(map1);
        }

        private void areaButton_Click(object sender, EventArgs e)
        {
            //find area with corresponding button
            Area area = currentMap.AreaList.First(item => item.Button == sender);
            area.Button.BackColor = areaDoneColor;
            //in the future this will need to be changed with a win condition
            area.completed = true;
            //hide buttons
            foreach(Area visibleArea in currentMap.AreaList)
            {
                if(visibleArea.Button.Visible == true)
                {
                    visibleArea.Button.Visible = false;
                }
            }
            //unlock accessible areas
            if(area.AccessibleAreas != null)
            {
                foreach(Area accessibleArea in area.AccessibleAreas)
                {
                    accessibleArea.unlocked = true;
                }
            }
            else
            {
                //no accessablie paths - leaf node
            }

            LoadArea(area);
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            //hide next button
            btn_Next.Visible = false;

            LoadSection(currentMap);
        }

        private void HoverArea(object sender, EventArgs e)
        {
            RoundButton button = (RoundButton)sender;
            
            if(currentMap.AreaList.Find(area => area.Button == button).completed) button.BackColor = areaDoneHoverColor;
            else button.BackColor = areaHoverColor;
        }

        private void HoverLeave(object sender, EventArgs e)
        {
            RoundButton button = (RoundButton)sender;
            if (currentMap.AreaList.Find(area => area.Button == button).completed) button.BackColor = areaDoneColor;
            else button.BackColor = areaColor;
        }
    }
}
