using System;
using game_project.ECS;
using game_project.ECS.Components;
using game_project.GameObjects.Enemy;
using game_project.GameObjects.Link;
using Microsoft.Xna.Framework;

namespace game_project.CollisionResponse
{
    public class KeeseCollisionResponse : EnemyCollisionResponse
    {

        public KeeseCollisionResponse(Entity e, int damage) : base(e, damage)
        {

        }

        public override void Visit(ECS.Components.CollisionResponse other)
        {
            other.ResolveCollision(this);
        }

        public override void ResolveCollision(RigidCollisionResponse other)
        {

            // do nothing
        }



    }
}
