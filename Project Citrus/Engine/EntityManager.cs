using Project_Citrus.Engine.ContentLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Citrus.Engine.EntitySystems;
using Project_Citrus.Engine.lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine
{
    public class EntityManager
    {
        private List<Entity> entities = new List<Entity>(); // All entities in a list
        private RenderSystem renderSystem = new RenderSystem();
        private LogicSystem logicSystem = new LogicSystem();

        public EntityManager(SpriteBatch spriteBatch)
        {
            renderSystem.Initialize(spriteBatch);
        }

        public List<Entity> Entities
        {
            get { return entities; }
        }

        [LuaFunctionAttr("Add_Entity_To_World", "Add an entity to the world.", new String[] { "Entity to add." })]
        public void Add_Entity_To_World(Entity entity)
        {
            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        [LuaFunctionAttr("Entity_Exists", "Check if an entity is already created.", new String[] { "Entity to check." })]
        public Boolean Entity_Exists(Entity entity)
        {
            if (entities.Contains(entity))
                return true;
            return false;
        }

        [LuaFunctionAttr("Create_Entity", "Create an entity.", new String[] { "ID of the entity to create." })]
        public Entity Create_Entity(String id_type)
        {
            Entity entity = JSON_Loader.Get_Entity(id_type);
            return entity;
        }

        [LuaFunctionAttr("RegisterToRenderSystem", "Register the entity to the render system. Returns false if the entity did not have the valid component types", new String[] { "Entity to register." })]
        public Boolean RegisterToRenderSystem(Entity entity)
        {
            return this.renderSystem.Register_Entity(entity);
        }

        [LuaFunctionAttr("RegisterToLogicSystem", "Register the entity to the logic system. Returns false if the entity did not have the valid component types", new String[] { "Entity to register." })]
        public Boolean RegisterToLogicSystem(Entity entity)
        {
            return this.logicSystem.Register_Entity(entity);
        }

        [LuaFunctionAttr("Data_Dump", "Get a list of all entities in the world.")]
        public void Data_Dump()
        {
            System.Diagnostics.Debug.WriteLine("Entities:");
            foreach (Entity ent in entities)
            {
                System.Diagnostics.Debug.WriteLine(ent.Name);
            }
        }
        public void Update(GameTime gameTime)
        {
            logicSystem.Execute(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            renderSystem.Execute(spriteBatch, gameTime);
        }
    }
}
