#if UNITY_ANDROID || UNITY_WEBGL || UNITY_EDITOR
using System;
using System.Collections;
using UnityEngine.Networking;
#endif
using System.IO;
using UnityEngine;

namespace Arcturus.AssetUtil
{
    public static class AssetUtils
    {
        #region Helper Funcs
        private static Texture2D GetBlankTexture()
        {
            return new Texture2D(2, 2);
        }
        #endregion

        /// <summary>
        /// Locates and returns a sprite assets if it exists at the given path.<br/>
        /// Handles both the Resources and StreamingAssets folders.<br/><br/>
        /// Note:<br/>
        /// - Resources do not include file extension.<br/>
        /// - StreamingAssets do include file extension.<br/>
        /// </summary>
        /// <param name="folderOrigin"></param>
        /// <param name="pathFromOrigin"></param>
        /// <param name="streamedSpritePivotX"></param>
        /// <param name="streamedSpritePivotY"></param>
        /// <returns></returns>
        public static Sprite GetAssetSprite(FolderOrigin folderOrigin, string pathFromOrigin, float streamedSpritePivotX = 0.5f, float streamedSpritePivotY = 0.5f)
        {
            switch (folderOrigin)
            {
                case FolderOrigin.Resources:
                    return Resources.Load<Sprite>(pathFromOrigin);

                case FolderOrigin.StreamingAssets:
                    var path = Path.Combine(Application.streamingAssetsPath, pathFromOrigin);
                    var bytes = File.ReadAllBytes(path);
                    Texture2D texture = GetBlankTexture();
                    texture.LoadImage(bytes);
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(streamedSpritePivotX, streamedSpritePivotY));

                default:
                    return null;
            }
        }

        /// <summary>
        /// Locates and returns text from a text asset if it exists at the given path.<br/>
        /// Handles both the Resources and StreamingAssets folders.<br/><br/>
        /// Note:<br/>
        /// - Resources do not include file extension.<br/>
        /// - StreamingAssets do include file extension.<br/>
        /// </summary>
        /// <param name="folderOrigin"></param>
        /// <param name="pathFromOrigin"></param>
        /// <returns></returns>
        public static string GetAssetText(FolderOrigin folderOrigin, string pathFromOrigin)
        {
            switch (folderOrigin)
            {
                case FolderOrigin.Resources:
                    return Resources.Load<TextAsset>(pathFromOrigin).ToString();

                case FolderOrigin.StreamingAssets:
                    var path = Path.Combine(Application.streamingAssetsPath, pathFromOrigin);
                    return File.ReadAllText(path);

                default:
                    return "";
            }
        }

        /// <summary>
        /// Locates and returns bytes from an asset if it exists at the given path.<br/>
        /// Handles both the Resources and StreamingAssets folders.<br/><br/>
        /// Note:<br/>
        /// - Resources do not include file extension.<br/>
        /// - StreamingAssets do include file extension.<br/>
        /// </summary>
        /// <param name="pathFromOrigin"></param>
        /// <returns></returns>
        public static byte[] GetAssetBytes(FolderOrigin folderOrigin, string pathFromOrigin)
        {
            switch (folderOrigin)
            {
                case FolderOrigin.Resources:
                    return Resources.Load<TextAsset>(pathFromOrigin).bytes;

                case FolderOrigin.StreamingAssets:
                    var path = Path.Combine(Application.streamingAssetsPath, pathFromOrigin);
                    return File.ReadAllBytes(path);

                default:
                    return new byte[0];
            }
        }

#if UNITY_ANDROID || UNITY_WEBGL || UNITY_EDITOR
        #region WebGL & Android Funcs
        /// <summary>
        /// This reads the StreamingAssets directory through local host madness.<br/>
        /// Requires a provided function for handling the output sprite.<br/><br/>
        /// Important:<br/>
        /// - Only use for android and webgl builds.<br/>
        /// - Include file extension in path.
        /// </summary>
        /// <param name="pathFromStreamingAssets"></param>
        /// <param name="OnSpriteIsLoaded"></param>
        /// <param name="spritePivotX"></param>
        /// <param name="spritePivotY"></param>
        /// <returns></returns>
        public static IEnumerator GetAssetSpriteAndroid(string pathFromStreamingAssets, Action<Sprite> OnSpriteIsLoaded, float spritePivotX = 0.5f, float spritePivotY = 0.5f)
        {
            var path = Path.Combine(Application.streamingAssetsPath, pathFromStreamingAssets);

            var www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError(www.error);
            else
            {
                var bytes = www.downloadHandler.data;
                Texture2D texture = GetBlankTexture();
                texture.LoadImage(bytes);
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(spritePivotX, spritePivotY));
                OnSpriteIsLoaded?.Invoke(sprite);
            }
        }

        /// <summary>
        /// This reads the StreamingAssets directory through local host madness.<br/>
        /// Requires a provided function for handling the output text.<br/><br/>
        /// Important:<br/>
        /// - Only use for android and webgl builds.<br/>
        /// - Include file extension in path.
        /// </summary>
        /// <param name="pathFromStreamingAssets"></param>
        /// <param name="OnTextIsLoaded"></param>
        /// <returns></returns>
        public static IEnumerator GetAssetTextAndroid(string pathFromStreamingAssets, Action<string> OnTextIsLoaded)
        {
            var path = Path.Combine(Application.streamingAssetsPath, pathFromStreamingAssets);

            var www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError(www.error);
            else
            {
                var bytes = www.downloadHandler.data;
                var text = Convert.ToBase64String(bytes);
                OnTextIsLoaded?.Invoke(text);
            }
        }

        /// <summary>
        /// This reads the StreamingAssets directory through local host madness.<br/>
        /// Requires a provided function for handling the output byte array.<br/><br/>
        /// Important:<br/>
        /// - Only use for android and webgl builds.<br/>
        /// - Include file extension in path.
        /// </summary>
        /// <param name="pathFromStreamingAssets"></param>
        /// <param name="OnBytesIsLoaded"></param>
        /// <returns></returns>
        public static IEnumerator GetAssetBytesAndroid(string pathFromStreamingAssets, Action<byte[]> OnBytesIsLoaded)
        {
            var path = Path.Combine(Application.streamingAssetsPath, pathFromStreamingAssets);

            var www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError(www.error);
            else
            {
                var bytes = www.downloadHandler.data;
                OnBytesIsLoaded?.Invoke(bytes);
            }
        }
        #endregion
#endif
    }
}
