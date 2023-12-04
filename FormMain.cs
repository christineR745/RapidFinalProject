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

        //method to position buttons (should only be called onces per map section)
        //had to hard code the button locations because setting them on the designer wasn't working for me
        public void PositionButtons(MapSection map)
        {
            foreach (Area area in map.AreaList)
            {
                area.Button.Location = new Point(area.X, area.Y);
                area.Button.FlatStyle = FlatStyle.Flat;
                area.Button.BackColor = Color.Transparent;
                area.Button.FlatAppearance.MouseDownBackColor = Color.Transparent;
                area.Button.FlatAppearance.MouseOverBackColor = Color.Transparent;
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
            MapSection map1 = new MapSection("Map 1", new Bitmap(Properties.Resources.mapOutline1));
            MapSection map2 = new MapSection("Map 2", new Bitmap(Properties.Resources.placeholder));
            MapSection map3 = new MapSection("Map 3", new Bitmap(Properties.Resources.placeholder));
            //areas within map1
            Area area1A = new Area("1-A", btn_1A, 195, 591);
            Area area1B = new Area("1-B", btn_1B, 390, 591);
            Area area2A = new Area("2-A", btn_2A, 205, 498);
            Area area2B = new Area("2-B", btn_2B, 390, 508);
            Area area3A = new Area("3-A", btn_3A, 163, 411);
            Area area3B = new Area("3-B", btn_3B, 227, 379);
            Area area3C = new Area("3-C", btn_3C, 336, 446);
            Area area3D = new Area("3-D", btn_3D, 442, 459);
            Area area4A = new Area("4-A", btn_4A, 57, 335);
            Area area4B = new Area("4-B", btn_4B, 148, 306);
            Area area4C = new Area("4-C", btn_4C, 246, 256);
            Area area4D = new Area("4-D", btn_4D, 355, 335);
            Area area4E = new Area("4-E", btn_4E, 442, 357);
            Area area4F = new Area("4-F", btn_4F, 516, 379);
            Area area5A = new Area("5-A", btn_5A, 81, 177);
            Area area5B = new Area("5-B", btn_5B, 166, 189);
            Area area5C = new Area("5-C", btn_5C, 336, 247);
            Area area5D = new Area("5-D", btn_5D, 392, 218);
            Area area5E = new Area("5-E", btn_5E, 507, 290);
            Area area6A = new Area("6-A", btn_6A, 111, 92);
            Area area6B = new Area("6-B", btn_6B, 219, 92);
            Area area6C = new Area("6-C", btn_6C, 268, 148);
            Area area6D = new Area("6-D", btn_6D, 495, 166);
            Area area7A = new Area("7-A", btn_7A, 406, 92);
            //create accessible area lists
            //there's probably a better way to do this but I can't figure it out
            area1A.AccessibleAreas = new List<Area> { area2A };
            area1B.AccessibleAreas = new List<Area> { area2B };
            area2A.AccessibleAreas = new List<Area> { area3A, area3B };
            area2B.AccessibleAreas = new List<Area> { area3C, area3D };
            area3A.AccessibleAreas = new List<Area> { area4A, area4B };
            area3B.AccessibleAreas = new List<Area> { area4C };
            area3C.AccessibleAreas = new List<Area> { area4D };
            area3D.AccessibleAreas = new List<Area> { area4E, area4F};
            area4A.AccessibleAreas = new List<Area> { area5A };
            area4B.AccessibleAreas = new List<Area> { area5B };
            area4C.AccessibleAreas = new List<Area> { area6C };
            area4D.AccessibleAreas = new List<Area> { area5C, area5D };
            area4E.AccessibleAreas = new List<Area> { area5E };
            area4F.AccessibleAreas = new List<Area> { area5E };
            area5A.AccessibleAreas = new List<Area> { area6A };
            area5B.AccessibleAreas = new List<Area> { area6A, area6B };
            area5C.AccessibleAreas = new List<Area> { area6C };
            area5D.AccessibleAreas = new List<Area> { area7A };
            area5E.AccessibleAreas = new List<Area> { area6D };
            area6D.AccessibleAreas = new List<Area> { area7A };
            //create list of areas in map
            map1.AreaList = new List<Area>() {
                area1A, area1B, 
                area2A, area2B, 
                area3A, area3B, area3C, area3D,
                area4A, area4B, area4C, area4D, area4E, area4F,
                area5A, area5B, area5C, area5D, area5E,
                area6A, area6B, area6C, area6D,
                area7A
            };
            
            //unlock first areas
            area1A.unlocked = true;
            area1B.unlocked = true;

            PositionButtons(map1);
            LoadSection(map1);
        }

        private void areaButton_Click(object sender, EventArgs e)
        {
            //find area with corresponding button
            Area area = currentMap.AreaList.First(item => item.Button == sender);
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
    }
}
