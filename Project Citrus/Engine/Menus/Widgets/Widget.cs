using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Project_Citrus.ContentLoading;
using Project_Citrus.Engine.ContentLoading;
using Project_Citrus.Engine.Menus.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.Menus.Widgets
{
    public class Widget
    {
        protected String name;
        protected Vector2 position;
        protected Vector2 size;
        protected String image_name;
        protected List<Widget> widgets;
        protected ImageInfo image_info;
        protected Widget parent;
        protected Format WidgetFormat;
        protected Boolean formated;
        protected Vector2 formated_position;
        protected Vector2 formated_size;
        public Widget() : this(null, null, Vector2.Zero, new Vector2(1, 1), null) { }
        public Widget(String name) : this(name, null, Vector2.Zero, new Vector2(1, 1), null) { }
        public Widget(String name, Widget parent) : this(name, parent, Vector2.Zero, new Vector2(1, 1), null) { }
        public Widget(String name, Vector2 position) : this(name, null, position, new Vector2(1, 1), null) { }
        public Widget(String name, Vector2 position, Vector2 size) : this(name, null, position, size, null) { }
        public Widget(String name, Vector2 position, Vector2 size, params Widget[] new_widgets) : this(name, null, position, size, null) { }
        public Widget(String name, Widget parent, Vector2 position, Vector2 size, params Widget[] new_widgets)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            this.parent = parent;
            widgets = new List<Widget>();
            if (new_widgets != null)
                foreach (Widget widget in new_widgets)
                    this.Add_Widget(widget);
            this.formated = false;
            this.Initialize();
        }
        public String Name { get { return name; } }
        public String Image_Name { get { return image_name; } set { image_name = value; } }
        public virtual Vector2 Position { get { return position; } set { position = value; } }
        public virtual Vector2 Size { get { return size; } set { size = value; } }
        public List<Widget> Widgets { get { return widgets; } }
        public virtual Vector2 Formated_Position { get { return formated_position; } set { formated_position = value; } }
        public virtual Vector2 Formated_Size { get { return formated_size; } set { formated_size = value; } }
        public virtual Vector2 Real_Position
        {
            get
            {
                if (parent != null)
                    return parent.Real_Position + Formated_Position;
                else
                    return Formated_Position;
            }
        }
        public virtual Vector2 Real_Size
        {
            get
            {
                if (Image_Info != null)
                    return Size / Image_Info.Size;
                return new Vector2(1, 1);
            }
        }
        public ImageInfo Image_Info
        {
            get
            {
                if (image_info != null)
                    return image_info;
                else
                {
                    image_info = ContentLoader.Get_ImageInfo(image_name);
                    return image_info;
                }
            }
        }
        public virtual void Initialize()
        {
            this.WidgetFormat = new Format(10, 10);
        }

        public Boolean Reformat()
        {
            this.WidgetFormat.FormatWidget(this, this.parent);
            formated = true;
            return true;
        }
        public Boolean Add_Widget(Widget widget)
        {
            if (widgets.Contains(widget))
                return false;
            widgets.Add(widget);
            widget.parent = this;
            widget.formated = false;
            this.formated = false;
            return true;
        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!formated)
                Reformat();
            if (Image_Info != null)
            {
                spriteBatch.Draw(Image_Info.Tex, Real_Position, null, Color.White, 0, Vector2.Zero, Real_Size, SpriteEffects.None, 0);
            }
            if (widgets != null)
                foreach (Widget widget in widgets)
                    widget.Draw(spriteBatch, gameTime);
        }
    }
}
