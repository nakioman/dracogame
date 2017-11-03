using DracoGame.Shared.Entities;
using DracoGame.Shared.Helpers;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DracoGame.Shared.Scenes
{
    public class TestMap : Scene
    {
        public TestMap()
        {
            addRenderer(new DefaultRenderer());
        }

        public override void initialize()
        {
            // load the TiledMap and display it with a TiledMapComponent
            var tiledEntity = createEntity(Constants.Entities.TestMap);
            var tiledMap = content.Load<TiledMap>(Constants.Content.Map.Test);
            var tiledMapComponent = tiledEntity.addComponent(new TiledMapComponent(tiledMap));
            tiledMapComponent.setLayersToRender(
                Constants.MapLayers.Collision,
                Constants.MapLayers.Objects,
                Constants.MapLayers.Road,
                Constants.MapLayers.Walkable);

            // render below/behind everything else. our player is at 0.
            tiledMapComponent.renderLayer = 10;

            // render our above-details layer after the player so the player is occluded by it when walking behind things
            var tiledMapDetailsComp = tiledEntity.addComponent(new TiledMapComponent(tiledMap));
            tiledMapDetailsComp.setLayerToRender(Constants.MapLayers.WalkableBehind);
            tiledMapDetailsComp.renderLayer = -1;

            // the details layer will write to the stencil buffer so we can draw a shadow when the player is behind it. we need an AlphaTestEffect here as well
            tiledMapDetailsComp.material = Material.stencilWrite();
            tiledMapDetailsComp.material.effect = content.loadNezEffect<SpriteAlphaTestEffect>();

            // setup our camera bounds with a 1 tile border around the edges (for the outside collision tiles)
            var maxBound = new Vector2(tiledMap.tileHeight * tiledMap.width, tiledMap.tileWidth * tiledMap.height);
            tiledEntity.addComponent(new CameraBounds(Vector2.Zero, maxBound));

            // Get spawn points from tiledmap
            var spawnPointsLayer = tiledMap.getObjectGroup(Constants.MapLayers.SpawnPoints);
            var randomSpanPointIndex = Random.nextInt(spawnPointsLayer.objects.Length);
            var playerSpawnPoint = spawnPointsLayer.objects[randomSpanPointIndex].position;

            //Create our player
            var playerEntity = addEntity(new Player(tiledMap, Constants.MapLayers.Collision, playerSpawnPoint));

            // add a component to have the Camera follow the player
            var followCamera = camera.entity.addComponent(new FollowCamera(playerEntity));
            followCamera.mapLockEnabled = true;
            followCamera.mapSize = new Vector2(tiledMap.width * tiledMap.tileWidth, tiledMap.height * tiledMap.tileHeight);
        }
    }
}