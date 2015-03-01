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
        protected Anchor origin;
        protected Anchor anchor;
        public Widget() : this(null, null, Vector2.Zero, new Vector2(1, 1), Anchor.TOP_LEFT, null) { }
        public Widget(String name) : this(name, null, Vector2.Zero, new Vector2(1, 1), Anchor.TOP_LEFT, null) { }
        public Widget(String name, Widget parent) : this(name, parent, Vector2.Zero, new Vector2(1, 1), Anchor.TOP_LEFT, null) { }
        public Widget(String name, Vector2 position, Anchor anchor) : this(name, null, position, new Vector2(1, 1), anchor, null) { }
        public Widget(String name, Vector2 position, Vector2 size, Anchor anchor) : this(name, null, position, size, anchor, null) { }
        public Widget(String name, Vector2 position, Vector2 size, Anchor anchor, params Widget[] new_widgets) : this(name, null, position, size, anchor, new_widgets) { }
        public Widget(String name, Widget parent, Vector2 position, Vector2 size, Anchor anchor, params Widget[] new_widgets)
            : this(name, parent, position, size, Anchor.TOP_LEFT, anchor, new_widgets) { }
        public Widget(String name, Widget parent, Vector2 position, Vector2 size, Anchor origin, Anchor anchor, params Widget[] new_widgets)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            this.origin = origin;
            this.anchor = anchor;
            this.parent = parent;
            widgets = new List<Widget>();
            if (new_widgets != null)
                foreach (Widget widget in new_widgets)
                    this.Add_Widget(widget);
        }
        public String Name { get { return name; } }
        public String Image_Name { get { return image_name; } set { image_name = value; } }
        public List<Widget> Widgets { get { return widgets; } }
        public virtual Vector2 Position { get { return position; } set { position = value; } }
        public virtual Vector2 Size { get { return size; } set { size = value; } }
        public Anchor Origin { get { return origin; } }
        protected virtual Vector2 Real_Position
        {
            get
            {
                if (parent != null)
                {
                    Vector2 real_pos = TransformOriginToTopLeft();
                    switch (anchor)
                    {
                        case Anchor.TOP_LEFT:
                            real_pos.X = parent.Real_Position.X + real_pos.X;
                            real_pos.Y = parent.Real_Position.Y + real_pos.Y;
                            break;
                        case Anchor.TOP_RIGHT:
                            real_pos.X = parent.Real_Position.X + parent.Size.X + real_pos.X;
                            real_pos.Y = parent.Real_Position.Y + real_pos.Y;
                            break;
                        case Anchor.BOTTOM_LEFT:
                            real_pos.X = parent.Real_Position.X + real_pos.X;
                            real_pos.Y = parent.Real_Position.Y + parent.Size.Y + real_pos.Y;
                            break;
                        case Anchor.BOTTOM_RIGHT:
                            real_pos.X = parent.Real_Position.X + parent.Size.X + real_pos.X;
                            real_pos.Y = parent.Real_Position.Y + parent.Size.Y + real_pos.Y;
                            break;
                        case Anchor.CENTER:
                            real_pos.X = parent.Real_Position.X + parent.Size.X / 2 + real_pos.X;
                            real_pos.Y = parent.Real_Position.Y + parent.Size.Y / 2 + real_pos.Y;
                            break;
                    }
                    return real_pos;
                }
                else
                    return position;
            }
        }
        protected virtual Vector2 Scale
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

        /*
         * Convert coordinates to be relative to Top_Left
         */
        protected Vector2 TransformOriginToTopLeft()
        {
            Vector2 transformed = new Vector2(Position.X, Position.Y);
            switch (origin)
            {
                case Anchor.TOP_LEFT:
                    // Do nothing, already relative to top left
                    break;
                case Anchor.TOP_RIGHT:
                    transformed.X = Position.X - Size.X;
                    break;
                case Anchor.BOTTOM_LEFT:
                    transformed.Y = Position.Y - Size.Y;
                    break;
                case Anchor.BOTTOM_RIGHT:
                    transformed.X = Position.X - Size.X;
                    transformed.Y = Position.Y - Size.Y;
                    break;
                case Anchor.CENTER:
                    transformed.X = Position.X - Size.X/2;
                    transformed.Y = Position.Y - Size.Y/2;
                    break;
            }
            return transformed;
        }
        public Boolean Add_Widget(Widget widget)
        {
            if (widgets.Contains(widget))
                return false;
            widgets.Add(widget);
            widget.parent = this;
            return true;
        }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Image_Info != null)
            {
                spriteBatch.Draw(Image_Info.Tex, Real_Position, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
            }
            if (widgets != null)
                foreach (Widget widget in widgets)
                    widget.Draw(spriteBatch, gameTime);
        }
    }
}
