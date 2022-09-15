using System.IO;
using UnityEngine;

namespace Arcturus.AssetUtil
{
    public static class AssetUtils
    {
        /// <summary>
        /// Locates and returns a sprite assets if it exists at the given path.<br/>
        /// Handles both the Resources and StreamingAssets folders.<br/><br/>
        /// Note:<br/>
        /// - Resources do not include file extension.<br/>
        /// - StreamingAssets do include file extension.<br/>
        /// </summary>
        /// <param name="folderOrigin"></param>
        /// <param name="pathFromOrigin"></param>
        /// <returns></returns>
        public static Sprite GetAssetSprite(FolderOrigin folderOrigin, string pathFromOrigin)
        {
            switch (folderOrigin)
            {
                case FolderOrigin.Resources:
                    return Resources.Load<Sprite>(pathFromOrigin);

                case FolderOrigin.StreamingAssets:
                    var bytes = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, pathFromOrigin));

                    Texture2D texture = new Texture2D(1, 1);
                    texture.LoadImage(bytes);

                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                default:
                    return null;
            }
        }


    }
}
