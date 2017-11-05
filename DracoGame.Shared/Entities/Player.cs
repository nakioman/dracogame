using System;
using System.Collections.Generic;
using System.Linq;
using DracoGame.Shared.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.AI.Pathfinding;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;

namespace DracoGame.Shared.Entities
{
    public class Player : Entity
    {
        enum Animations
        {
            WalkUp,
            WalkDown,
            WalkRight,
            WalkLeft
        }

        private readonly TiledMap _tiledMap;
        private readonly TiledTileLayer _collisionLayer;
        private Sprite<Animations> _animation;
        private float _moveSpeed = 10f;
        private AstarGridGraph _astarGraph;
        private List<Point> _astarSearchPath;
        private Mover _mover;

        public Player(TiledMap tiledMap, string collisionLayer, Vector2 initialPosition)
        {
            name = Constants.Entities.Player;
            position = initialPosition;

            _tiledMap = tiledMap;
            _collisionLayer = _tiledMap.getLayer<TiledTileLayer>(collisionLayer);
        }

        public override void onAddedToScene()
        {
            base.onAddedToScene();

            _astarGraph = new AstarGridGraph(_collisionLayer);

            // load up our character texture atlas.
            var texture = scene.content.Load<Texture2D>(Constants.Content.Characters);
            var subtextures = Subtexture.subtexturesFromAtlas(texture, 32, 48);

            _animation = addComponent(new Sprite<Animations>(subtextures[0]));
            _animation.setOrigin(new Vector2(0, 48 - 32)); //Set the origin to the left bottom

            // add a shadow that will only be rendered when our player is behind the detailss layer of the tilemap (renderLayer -1). The shadow
            // must be in a renderLayer ABOVE the details layer to be visible.
            var shadow = addComponent(new SpriteMime(_animation));
            shadow.color = new Color(10, 10, 10, 80);
            shadow.material = Material.stencilRead();
            shadow.renderLayer = -2; // ABOVE our tiledmap layer so it is visible

            // extract the animations from the atlas
            _animation.addAnimation(Animations.WalkDown, new SpriteAnimation(new List<Subtexture>
            {
                subtextures[0],
                subtextures[1],
                subtextures[2],
            }));

            _animation.addAnimation(Animations.WalkUp, new SpriteAnimation(new List<Subtexture>()
            {
                subtextures[36],
                subtextures[37],
                subtextures[38],
            }));

            _animation.addAnimation(Animations.WalkLeft, new SpriteAnimation(new List<Subtexture>
            {
                subtextures[12],
                subtextures[13],
                subtextures[14],
            }));

            _animation.addAnimation(Animations.WalkRight, new SpriteAnimation(new List<Subtexture>()
            {
                subtextures[24],
                subtextures[25],
                subtextures[26],
            }));

            _mover = addComponent<Mover>(); 
        }

        public override void update()
        {
            base.update();

            if (Input.leftMouseButtonPressed)
            {
                var mousePosition = Core.scene.camera.mouseToWorldPoint();
                var start = _tiledMap.worldToTilePosition(position);
                var end = _tiledMap.worldToTilePosition(mousePosition);
                _astarSearchPath = _astarGraph.search(start, end);
                _astarSearchPath?.Remove(start);
                _animation.stop();
            }

            if (Input.touch.currentTouches.Any())
            {
                var touchPosition = Core.scene.camera.touchToWorldPoint(Input.touch.currentTouches.First());
                var start = _tiledMap.worldToTilePosition(position);
                var end = _tiledMap.worldToTilePosition(touchPosition);
                _astarSearchPath = _astarGraph.search(start, end);
                _astarSearchPath?.Remove(start);
                _animation.stop();
            }

            if (_astarSearchPath != null && _astarSearchPath.Any())
            {
                var point = _astarSearchPath.First();
                var newPosition = _tiledMap.tileToWorldPosition(point);
                var delta = Vector2.Subtract(newPosition, position);
                var motion = delta * _moveSpeed * Time.deltaTime;

                if (Math.Abs(motion.Length()) < 1)
                {
                    _astarSearchPath.RemoveAt(0);
                }

                PlayAnimation(motion);
                _mover.move(motion, out _);

                if (!_astarSearchPath.Any())
                {
                    position = newPosition;
                    _animation.stop();
                }

            }
        }

        private void PlayAnimation(Vector2 delta)
        {
            var animation = Animations.WalkUp;
            if (Math.Abs(delta.X) > Math.Abs(delta.Y))
            {
                animation = delta.X > 0 ? Animations.WalkRight : Animations.WalkLeft;
            }
            else if (delta.Y > 0)
            {
                animation = Animations.WalkDown;

            }

            if (!_animation.isAnimationPlaying(animation))
            {
                _animation.play(animation);
            }
        }
    }
}